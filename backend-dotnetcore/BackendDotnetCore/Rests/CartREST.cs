using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rests
{
    [ApiController]
    [Route("api/cart")]
    public class CartREST : ControllerBase
    {
        private PaymentDAO paymentDAO;
        private CartDAO cartDAO;
        Product2DAO product2DAO;
        public CartREST()
        {


            this.paymentDAO = new PaymentDAO();
            this.cartDAO = new CartDAO();
            this.product2DAO = new Product2DAO();
        }
        [HttpGet]
        [Authorize]

        public ActionResult getCart()
        {


            try
            {

                // Lấy UserEntity đang đăng nhập từ jwt
                UserEntity user = (UserEntity)HttpContext.Items["User"];
                //Console.WriteLine("User: " + user);
                // Xóa bộ nhớ đệm chứa userentity
                HttpContext.Items["User"] = null;
                CartEntity c = cartDAO.getCart(user.Id);
                // return Ok(c);
                return Ok(new CartDTO(c));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return BadRequest();

        }

        [HttpDelete]
        [Authorize]

        public ActionResult removeRowCart([FromBody] FormDeleteCart formDeleteCart)
        {


            try
            {
                // Lấy UserEntity đang đăng nhập từ jwt
                UserEntity user = (UserEntity)HttpContext.Items["User"];
                Console.WriteLine("User: " + user);
                // Xóa bộ nhớ đệm chứa userentity
                HttpContext.Items["User"] = null;
                CartEntity c = cartDAO.getCart(user.Id, formDeleteCart.CartItemId);
                if (c == null) return BadRequest();
                cartDAO.RemoveCart(c.Items);

                c = cartDAO.getCart(c);
                return Ok(new CartDTO(c));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return BadRequest();

        }

        /* [HttpPost("test/mail")]

         public ActionResult test([FromBody] string mail)
         {
             return Ok(mail);
         }*/
        [HttpPost]
        [Authorize]

        public ActionResult postCart([FromBody] FormAddCart formAddCart)
        {
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("User: " + user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;

            //Xóa hết tất cả sản phẩm trong giỏ hàng
            cartDAO.deleteAllItemCart(user.Id);
           
            CartEntity c = cartDAO.getCart(user.Id);
            foreach (CartItem ci in formAddCart.CartItems)
            {

                int productId = 0;
                if(ci.Product!=null )
                {
                  Console.WriteLine("productId: {0}, amount: {1}", ci.Product.Id, ci.Quantity);
                    if (productId==0 && ci.Product.Id != 0)
                    {
                        productId = ci.Product.Id;
                    }
                       

                }
               
                Console.WriteLine("productSpecificId {0}", productId);
                Product2 p = product2DAO.getProduct(productId);
                if (p == null) return BadRequest();

                try
                {
                    CartItemEntity cartItemEntity = null;
                    if (c.Items != null)
                        cartItemEntity = c.Items.Find(X => X.Product.Id.CompareTo(productId) == 0);
                    else
                    {
                        c.Items = new List<CartItemEntity>();
                    }

                    Console.WriteLine("cartItemEntity" + cartItemEntity);
                    if (cartItemEntity == null)
                    {
                        cartItemEntity = new CartItemEntity();
                        cartItemEntity.Quantity = ci.Quantity;
                        cartItemEntity.ProductId = productId;
                        cartItemEntity.CartId = c.Id;
                        //
                        if (cartItemEntity.Deleted == false)
                            c.Items.Add(cartItemEntity);
                    }
                    else
                    {
                        Console.WriteLine("Increase amount");
                        //cartItemEntity.Amount += ci.Amount;
                        cartItemEntity.Quantity = ci.Quantity;
                        if (cartItemEntity.Quantity < 0) return BadRequest("Số lượng item trong giỏ hàng nhỏ hơn 0, hãy xóa item này khỏi giỏ hàng");
                    }

                    cartItemEntity.Actived = ci.Actived;
                    cartItemEntity.Deleted = ci.Deleted;
                    cartItemEntity = cartDAO.SaveCart(cartItemEntity);



                }
                catch (Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                    return BadRequest(e.Message);
                }
            }

            c = cartDAO.getCart(c);
            return Ok(c);
            //return Ok(new CartDTO(c));

        }
    }

    public class FormAddCart
    {
        public List<CartItem> CartItems { get; set; }

    }
    public class CartItem
    {
       
        public int Quantity { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }

        public Product3 Product { get; set; }
        public CartItem()
        {
            Quantity = 1;
            Actived = true;
            Deleted = false;
            Product = new Product3() { Id = 0};
        }
    }

    public class FormDeleteCart
    {
        public long CartItemId { get; set; }

    }
    public class Product3
    {
        public int Id { get; set; }
      
    }
  
}

