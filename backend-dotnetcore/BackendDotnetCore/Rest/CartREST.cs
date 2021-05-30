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
            Console.WriteLine("productId: {0}, amount: {1}", formAddCart.ProductId, formAddCart.Amount);
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("User: " + user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            try
            {
                CartEntity c = cartDAO.getCart(user.Id);
                CartItemEntity cartItemEntity = null;
                cartItemEntity = c.Items.Find(X => X.ProductId.CompareTo( formAddCart.ProductId) ==0);
               
                   
                Console.WriteLine("cartItemEntity" + cartItemEntity);
                if (cartItemEntity == null)
                {
                    cartItemEntity = new CartItemEntity();
                    cartItemEntity.Amount = formAddCart.Amount;
                    cartItemEntity.ProductId = formAddCart.ProductId;
                    cartItemEntity.CartId = c.Id;

                     c.Items.Add(cartItemEntity);
                }
                else
                {
                    Console.WriteLine("Increase amount");
                    cartItemEntity.Amount += formAddCart.Amount;
                }
                if(formAddCart.Actived!=-1)
                cartItemEntity.Actived = formAddCart.Actived==1?true:false;
                cartItemEntity = cartDAO.SaveCart(cartItemEntity);

                c= cartDAO.getCart(c); 
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
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public int Actived { get; set; }
        public FormAddCart()
        {
            Amount = 1;
            Actived = 1;
        }
    }
}
