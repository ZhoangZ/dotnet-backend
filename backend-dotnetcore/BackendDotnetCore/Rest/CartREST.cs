﻿using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
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
        Product2DAO product2DAO;
        public CartREST(PaymentDAO paymentDAO, Product2DAO product2DAO, CartDAO cartDAO)
        {
            this.paymentDAO = paymentDAO;
            this.cartDAO = cartDAO;
            this.product2DAO = product2DAO;
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
            Console.WriteLine("productId: {0}, amount: {1}", formAddCart.ProductId, formAddCart.Amount);
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            Console.WriteLine("User: " + user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;
            Product2 p=product2DAO.getProduct(formAddCart.ProductId);
            if (p == null) return BadRequest();

            try
            {
                CartEntity c = cartDAO.getCart(user.Id);
                CartItemEntity cartItemEntity = null;
                cartItemEntity = c.Items.Find(X => X.ProductSpecificId.CompareTo( formAddCart.ProductId) ==0);
               
                   
                Console.WriteLine("cartItemEntity" + cartItemEntity);
                if (cartItemEntity == null)
                {
                    cartItemEntity = new CartItemEntity();
                    cartItemEntity.Amount = formAddCart.Amount;
                    cartItemEntity.ProductSpecificId = formAddCart.ProductId;
                    cartItemEntity.CartId = c.Id;

                     c.Items.Add(cartItemEntity);
                }
                else
                {
                    Console.WriteLine("Increase amount");
                    cartItemEntity.Amount += formAddCart.Amount;
                }
                
                cartItemEntity.Actived = formAddCart.Actived;
                cartItemEntity = cartDAO.SaveCart(cartItemEntity);

                c= cartDAO.getCart(c);
                return Ok(new CartDTO(c));

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
        public bool Actived { get; set; }
        public FormAddCart()
        {
            Amount = 1;
            Actived = true;
        }
    }

    public class FormDeleteCart
    {
        public long CartItemId { get; set; }
       
    }
}