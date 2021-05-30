using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class CartItemDAO
    {

        private BackendDotnetDbContext dbContext;
        public CartItemDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }

       /* public int save(CartItemEntity cartItem)
        {
            if (cartItem.id != 0)
            {
                Console.WriteLine("cartItem after update = " + cartItem.id);
                dbContext.CartItems.Update(cartItem);
                dbContext.SaveChanges();
               
            }
            else
            {
                var ct = new CartItemEntity()
                {
                    active = 1,
                    product = cartItem.product,
                    user = cartItem.user
                };
                dbContext.CartItems.AddAsync(ct);
                dbContext.SaveChangesAsync();
                return ct.id;
            }
            return cartItem.id;
        }*/
    }
}
