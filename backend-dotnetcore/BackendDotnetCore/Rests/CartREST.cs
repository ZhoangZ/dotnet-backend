﻿using BackendDotnetCore.DAO;
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
    public class CartREST:ControllerBase
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
            }catch(Exception e)
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
            foreach(CartItem ci in formAddCart.CartItems)
            {

                Console.WriteLine("productId: {0}, amount: {1}", ci.ProductSpecificId, ci.Quantity);
                Product2Specific p=product2DAO.getSpecific(ci.ProductSpecificId);
                if (p == null) return BadRequest();

                try
                {
                    CartItemEntity cartItemEntity = null;
                    cartItemEntity = c.Items.Find(X => X.ProductSpecificId.CompareTo( ci.ProductSpecificId) ==0);
               
                   
                    Console.WriteLine("cartItemEntity" + cartItemEntity);
                    if (cartItemEntity == null)
                    {
                        cartItemEntity = new CartItemEntity();
                        cartItemEntity.Amount = ci.Quantity;
                        cartItemEntity.ProductSpecificId = ci.ProductSpecificId;
                        cartItemEntity.CartId = c.Id;
                        //
                        if(cartItemEntity.Deleted==false)
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
                    cartItemEntity = cartDAO.SaveCart(cartItemEntity);

                    

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return BadRequest(e.Message);
                }
            }

            c = cartDAO.getCart(c);
            return Ok(new CartDTO(c));

        }
    }

    public class FormAddCart
    {
        public List<CartItem> CartItems { get; set; }

    }
        public class CartItem
    {
        public long ProductSpecificId { get; set; }
        public int Quantity { get; set; }
        public bool Actived { get; set; }
        public bool Deleted { get; set; }
        public CartItem()
        {
            Quantity = 1;
            Actived = true;
            Deleted = false;
        }
    }

    public class FormDeleteCart
    {
        public long CartItemId { get; set; }
       
    }
}
