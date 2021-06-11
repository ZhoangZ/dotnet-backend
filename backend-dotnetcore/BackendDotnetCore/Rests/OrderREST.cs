using BackendDotnetCore.DAO;
using BackendDotnetCore.DTO;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rests
{
    [ApiController]
    [Route("api/order")]
    public class OrderREST: ControllerBase
    {

        private PaymentDAO paymentDAO;
        private OrderDAO orderDAO;
        Product2DAO product2DAO;
        public OrderREST()
        {


            this.paymentDAO = new PaymentDAO();
            this.orderDAO = new OrderDAO();
            this.product2DAO = new Product2DAO();
        }

        [HttpGet("{id}")]
        [Authorize]

        public ActionResult getOrder(long id)
        {


            try
            {

                // Lấy UserEntity đang đăng nhập từ jwt
                UserEntity user = (UserEntity)HttpContext.Items["User"];
                //Console.WriteLine("User: " + user);
                // Xóa bộ nhớ đệm chứa userentity
                HttpContext.Items["User"] = null;
                OrderEntity c = orderDAO.getOrder(user.Id, id);
                // return Ok(c);
                return Ok(c);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return BadRequest();

        }

        [HttpPost]
        [Authorize]

        public ActionResult postOrder([FromBody] FormPutOrder formAddCart)
        {
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("User: " + user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;

            //Xóa hết tất cả sản phẩm trong giỏ hàng
            orderDAO.deleteAllItemCart(user.Id);

            OrderEntity c = new OrderEntity();
            c.AddressDelivery = formAddCart.AddressDelivery;
            c.UserId = user.Id;
            c=orderDAO.SaveOrder(c);
           // c = orderDAO.getOrder(c);
            if(c==null) return BadRequest("Not save orderEntity");
            Console.WriteLine("Order: {0}", c.Id);
           // if(c!=null) return Ok(c); ;

           // return Ok(c);
           
            foreach (OrderItem ci in formAddCart.CartItems)
            {

                Console.WriteLine("productId: {0}, amount: {1}", ci.ProductSpecificId, ci.Quantity);
                Product2Specific p = product2DAO.getSpecific(ci.ProductSpecificId);
                if (p == null) return BadRequest();

                try
                {
                    OrderItemEntity cartItemEntity = null;
                    if (c.Items != null)
                        cartItemEntity = c.Items.Find(X => X.ProductSpecificId.CompareTo(ci.ProductSpecificId) == 0);
                    else c.Items = new List<OrderItemEntity>();

                    Console.WriteLine("orderItemEntity: {0}" , cartItemEntity);
                    if (cartItemEntity == null)
                    {
                        cartItemEntity = new OrderItemEntity();
                        cartItemEntity.Amount = ci.Quantity;
                        cartItemEntity.ProductSpecificId = ci.ProductSpecificId;
                        cartItemEntity.OrderId = c.Id;
                        //
                        if (cartItemEntity.Deleted == false)
                            c.Items.Add(cartItemEntity);
                    }
                    else
                    {
                        Console.WriteLine("Increase amount");
                        //cartItemEntity.Amount += ci.Amount;
                        cartItemEntity.Amount = ci.Quantity;
                        if (cartItemEntity.Amount < 0) return BadRequest("Số lượng item trong giỏ hàng nhỏ hơn 0, hãy xóa item này khỏi giỏ hàng");
                    }

                    cartItemEntity.Actived = ci.Actived;
                    cartItemEntity.Deleted = ci.Deleted;
                    cartItemEntity = orderDAO.SaveOrder(cartItemEntity);



                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest(e.Message);
                }
            }



            //Payment
            c = orderDAO.getOrder(c);

            PaymentEntity paymentEntity = new PaymentEntity();
            paymentEntity.userId = user.Id;
            paymentEntity.Amount = c.TotalPrice*100;
            paymentEntity.CurrCode = "VND";
            paymentEntity.UrlReturn = "htpp://localhost:3000";
            paymentEntity.CreateTime = DateTime.Now;
            paymentEntity.IpAddress = "119.17.249.22";
            paymentEntity = paymentDAO.AddPayment(paymentEntity);
            paymentEntity.gender("https://localhost:25002/payment/donate");
            paymentEntity = paymentDAO.UpdatePayment(paymentEntity);
            c.Payment = paymentEntity;
            c.PaymentId = paymentEntity.Id;
            Console.WriteLine("REST-Payment Id: {0}", c.PaymentId);
            orderDAO.UpdateOrder(c);

            c = orderDAO.getOrder(c);
            //  return Ok(new OrderDTO(c));
            return Ok(c);

        }
    }

    public class FormPutOrder
    {
        public List<OrderItem> CartItems { get; set; }
        public string AddressDelivery { get; set; }

    }
    public class OrderItem
    {
        public long ProductSpecificId { get; set; }
        public int Quantity { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
        public OrderItem()
        {
            Quantity = 1;
            Actived = true;
            Deleted = false;
        }
    }

    public class FormDeleteOrderItem
    {
        public long CartItemId { get; set; }

    }
}
