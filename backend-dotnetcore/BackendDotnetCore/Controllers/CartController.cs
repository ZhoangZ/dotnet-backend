using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Forms;
using BackendDotnetCore.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
   // [Route("api/cart")]
    public class CartController: ControllerBase
    {
        private Product2DAO product2DAO = new Product2DAO();
        private CartItemDAO cartItemDAO = new CartItemDAO();
        private IUserService userService = new UserService();
        static ICollection<CartItemDTO> collection = new HashSet<CartItemDTO>();

      /*  [HttpPost("add")]
        public ICollection<CartItemDTO> addToCart([FromBody] CartItemDTO cartItemInfo)
        {
            int idUser = 0;
            if (null == HttpContext.Session.GetInt32("idUserSession"))
            {
                Console.WriteLine("has not user on session");
                idUser = 0;
            }
            else
            {
                idUser = (int) HttpContext.Session.GetInt32("idUserSession");
                Console.WriteLine("has user on session " + idUser);
            }
            if (collection.Count > 0)
            {
                foreach (CartItemDTO c in collection)
                {
                    if (c.productID == cartItemInfo.productID)
                    {
                        c.amount++;
                        return appendListCart(idUser);
                    }
                }
                collection.Add(cartItemInfo);
                return appendListCart(idUser);
            }
            else{
                collection.Add(cartItemInfo);
            }
           
            return appendListCart(idUser);
        }*/

        /*public ICollection<CartItemDTO> appendListCart(int idUser)
        {
            if (idUser != 0)
            {
                Console.WriteLine("checkUserSession is true"); idUser = (int)HttpContext.Session.GetInt32("idUserSession");
                UserEntity ue = userService.getUserById(idUser);
                Console.WriteLine(ue.Fullname + ", email " + ue.Email);
                foreach (CartItemDTO c in collection)
                {
                    CartItemEntity ce = new CartItemEntity();
                    ce.product = product2DAO.getProduct(c.productID);
                    ce.user = ue;
                    ue.CartItemEntities.Add(ce);
                    int saved = cartItemDAO.save(ce);
                    if (saved != 0) Console.WriteLine(saved);
                }
            }
            return collection;
        }*/
    }
}
