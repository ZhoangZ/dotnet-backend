using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rest
{
    [ApiController]
    [Route("api/cart")]
    public class CartREST:ControllerBase
    {
        private PaymentDAO paymentDAO;
        private CartDAO cartDAO;
        public CartREST(PaymentDAO paymentDAO, CartDAO cartDAO)
        {
            this.paymentDAO = paymentDAO;
            this.cartDAO = cartDAO;
        }
        [HttpGet]
        [Authorize]

        public ActionResult getCart()
        {

         
            try
            {
                // Lấy UserEntity đang đăng nhập từ jwt
                UserEntity user = (UserEntity)HttpContext.Items["User"];
                Console.WriteLine("User: " + user);
                // Xóa bộ nhớ đệm chứa userentity
                HttpContext.Items["User"] = null;
                CartEntity c = cartDAO.getCart(user.Id);
                return Ok(c);

            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return BadRequest();
           
        }

        [HttpPost]
        [Authorize]

        public ActionResult postCart([FromBody] FormAddCart formAddCart)
        {
            Console.WriteLine("productId: {0}, amount: {1}", formAddCart.productId, formAddCart.amount);
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("User: " + user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            try
            {
                CartEntity c = cartDAO.getCart(user.Id);
                CartItemEntity cartItemEntity = null;
                cartItemEntity = c.Items.Find(X => X.ProductId.CompareTo( formAddCart.productId) ==0);
               
                   
                Console.WriteLine("cartItemEntity" + cartItemEntity);
                if (cartItemEntity == null)
                {
                    cartItemEntity = new CartItemEntity();
                    cartItemEntity.Amount = formAddCart.amount;
                    cartItemEntity.ProductId = formAddCart.productId;
                    cartItemEntity.CartId = c.Id;

                     c.Items.Add(cartItemEntity);
                }
                else
                {
                    Console.WriteLine("Increase amount");
                    cartItemEntity.Amount += formAddCart.amount;
                }
                cartItemEntity = cartDAO.SaveCart(cartItemEntity);

                c= cartDAO.getCart(user.Id); 
                return Ok(c);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                 return BadRequest(e.Message);
            }

        }
    }
    public class FormAddCart
    {
        public int productId { get; set; }
        public int amount { get; set; }
        public FormAddCart()
        {
            amount = 1;
        }
    }
}
