﻿using BackendDotnetCore.DAO;
using BackendDotnetCore.DTO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if(e.InnerException!=null)
                Console.WriteLine(e.InnerException.Message);
                return BadRequest(e.Message);
            }

        }

        [HttpPost]
        [Authorize]

        public ActionResult postOrder([FromBody] FormPutOrder formOrder)
        {
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("User: " + user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;

            //Xóa hết tất cả sản phẩm trong giỏ hàng
            orderDAO.deleteAllItemCart(user.Id);

            OrderEntity c = new OrderEntity();
            c.AddressDelivery = formOrder.Address;
            c.Email = formOrder.Email;
            c.Phone = formOrder.Phone;
            c.Fullname = formOrder.Fullname;
            c.Note = formOrder.Note;
            c.Status = 1;
            c.CreatedDate = DateTime.Now;
            c.Cod = false;


            c.UserId = user.Id;
            c=orderDAO.SaveOrder(c);
           // c = orderDAO.getOrder(c);
            if(c==null) return BadRequest("Not save orderEntity");
            Console.WriteLine("Order: {0}", c.Id);
           // if(c!=null) return Ok(c); ;

           // return Ok(c);
           
            foreach (OrderItem ci in formOrder.CartItems)
            {

                Console.WriteLine("productId: {0}, amount: {1}", ci.Idp, ci.Quantity);
                if (ci.Idp == 0)
                {
                    return BadRequest("Thiếu tham số idp.");
                }

                Console.WriteLine("productSpecificId {0}", ci.Idp);
                Product2 p = product2DAO.getProduct(ci.Idp); if (p == null) return BadRequest();

                try
                {
                    OrderItemEntity cartItemEntity = null;
                    if (c.Items != null)
                        cartItemEntity = c.Items.Find(X => X.ProductId.CompareTo(ci.Idp) == 0);
                    else c.Items = new List<OrderItemEntity>();

                    Console.WriteLine("orderItemEntity: {0}" , cartItemEntity);
                    if (cartItemEntity == null)
                    {
                        cartItemEntity = new OrderItemEntity();
                        cartItemEntity.Quantity = ci.Quantity;
                        cartItemEntity.ProductId = ci.Idp;
                        cartItemEntity.OrderId = c.Id;
                        cartItemEntity.Product = p;
                        //
                        if (cartItemEntity.Deleted == false)
                            c.Items.Add(cartItemEntity);
                    }
                    else
                    {
                        Console.WriteLine("Increase amount");
                        cartItemEntity.Quantity += ci.Quantity;
                        //cartItemEntity.Quantity = ci.Quantity;
                        if (cartItemEntity.Quantity < 0) return BadRequest("Số lượng item trong giỏ hàng nhỏ hơn 0, hãy xóa item này khỏi giỏ hàng");
                    }

                    cartItemEntity.Actived = ci.Actived;
                    cartItemEntity.Deleted = ci.Deleted;
                    cartItemEntity = orderDAO.SaveOrder(cartItemEntity);



                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        Console.WriteLine(e.InnerException.Message);
                    return BadRequest(e.Message);
                }
            }



            //Payment
            c = orderDAO.getOrder(c);

            PaymentEntity paymentEntity = new PaymentEntity();
            paymentEntity.userId = user.Id;
            paymentEntity.Amount = c.TotalPrice*100;
            paymentEntity.CurrCode = "VND";
            //paymentEntity.UrlReturn = "htpp://localhost:3000/accept";
            paymentEntity.UrlReturn = formOrder.UrlReturn;
            paymentEntity.CreateTime = DateTime.Now;
            paymentEntity.IpAddress = "119.17.249.22";
            paymentEntity.TransactionStatus = Models.EPaymentStatus.PENDING;
            paymentEntity = paymentDAO.AddPayment(paymentEntity);
            paymentEntity.gender("https://localhost:25002/payment/redirect");
            paymentEntity = paymentDAO.UpdatePayment(paymentEntity);
            c.Payment = paymentEntity;
            c.PaymentId = paymentEntity.Id;
            Console.WriteLine("REST-Payment Id: {0}", c.PaymentId);
            orderDAO.UpdateOrder(c);

            c = orderDAO.getOrder(c);


            // return Ok((OrderDTO)c);

           
            return Ok(new OrderDTO(c));
            //return Ok(c);

        }

        [HttpPost("cod")]
        [Authorize]

        public ActionResult postOrderCod([FromBody] FormPutOrder formOrder)
        {
            Console.WriteLine("POST-Cod------------");
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;

            //Xóa hết tất cả sản phẩm trong giỏ hàng
            orderDAO.deleteAllItemCart(user.Id);

            OrderEntity c = new OrderEntity();
            c.Cod = true;
            c.AddressDelivery = formOrder.Address;
            c.Email = formOrder.Email;
            c.Phone = formOrder.Phone;
            c.Fullname = formOrder.Fullname;
            c.Note = formOrder.Note;
            c.Status = 1;
            c.CreatedDate = DateTime.Now;


            c.UserId = user.Id;
            c = orderDAO.SaveOrder(c);
            // c = orderDAO.getOrder(c);
            if (c == null) return BadRequest("Not save orderEntity");
            Console.WriteLine("Order: {0}", c.Id);
            // if(c!=null) return Ok(c); ;

            // return Ok(c);
            if (formOrder.CartItems != null) //return BadRequest();
                foreach (OrderItem ci in formOrder.CartItems)
            {

                Console.WriteLine("productId: {0}, amount: {1}", ci.Idp, ci.Quantity);
                if (ci.Idp == 0)
                {
                    return BadRequest("Thiếu tham số idp.");
                }

                Console.WriteLine("productSpecificId {0}", ci.Idp);
                Product2 p = product2DAO.getProduct(ci.Idp); if (p == null) return BadRequest();

                try
                {
                    OrderItemEntity cartItemEntity = null;
                    if (c.Items != null)
                        cartItemEntity = c.Items.Find(X => X.ProductId.CompareTo(ci.Idp) == 0);
                    else c.Items = new List<OrderItemEntity>();

                    Console.WriteLine("orderItemEntity: {0}", cartItemEntity);
                    if (cartItemEntity == null)
                    {
                        cartItemEntity = new OrderItemEntity();
                        cartItemEntity.Quantity = ci.Quantity;
                        cartItemEntity.ProductId = ci.Idp;
                        cartItemEntity.OrderId = c.Id;
                        cartItemEntity.Product = p;
                        //
                        if (cartItemEntity.Deleted == false)
                            c.Items.Add(cartItemEntity);
                    }
                    else
                    {
                        Console.WriteLine("Increase amount");
                        cartItemEntity.Quantity += ci.Quantity;
                        //cartItemEntity.Quantity = ci.Quantity;
                        if (cartItemEntity.Quantity < 0) return BadRequest("Số lượng item trong giỏ hàng nhỏ hơn 0, hãy xóa item này khỏi giỏ hàng");
                    }

                    cartItemEntity.Actived = ci.Actived;
                    cartItemEntity.Deleted = ci.Deleted;
                    cartItemEntity = orderDAO.SaveOrder(cartItemEntity);
                     


                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        Console.WriteLine(e.InnerException.Message);
                    return BadRequest(e.Message);
                }
            }



            

            c = orderDAO.getOrder(c);
            StringBuilder sb = new StringBuilder("Hệ thống nhận được đơn hàng mới. Vào kiểm tra ngay!\n");
            sb.Append("<a href=\"http://localhost:3000/Admin/orders-manager\"> NHẤN VÀO ĐÂY ĐỂ KIỂM TRA.</a>");
            SendMailService.SendEmail("ĐƠN HÀNG MỚI", UserEntity.EmailAdminFinal, sb.ToString());
            // return Ok((OrderDTO)c);
            //return Ok(new OrderDTO(c));
            return Ok(c);

        }

        //get list orders status
        [HttpGet("status")]
        public IActionResult GetListStatusOrders()
        {
            return Ok(OrderEntity.GetListStatusOrders());
        }
    }

    public class FormPutOrder
    {
        public List<OrderItem> CartItems { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string UrlReturn { get; set; }


       

    }
    public class OrderItem
    {
       
        public int Quantity { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
        public int Idp { get; set; }
      

        public OrderItem()
        {
            Quantity = 1;
            Actived = true;
            Deleted = false;
            Idp = 0;


        }
    }
   
    public class FormDeleteOrderItem
    {
        public long CartItemId { get; set; }

    }

    
}
