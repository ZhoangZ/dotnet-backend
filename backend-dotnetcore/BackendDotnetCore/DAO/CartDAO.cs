using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackendDotnetCore.DAO
{
    public class CartDAO
    {

        private BackendDotnetDbContext dbContext;
        public CartDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }

        

        public CartEntity getCart(int UserId)

        {

            var tmp = dbContext.Carts.Where(s => s.UserId == UserId)
                .Include(x => x.User)
                .Include(x=>x.Items)
                .ThenInclude(X=>X.Product)
                //.Any(x=>x.)
                ;

            return tmp.FirstOrDefault();

        }

        public CartItemEntity SaveCart(CartItemEntity cartItemEntity)

        {
            dbContext.Entry(cartItemEntity).Reference(x => x.Product).IsModified = false;
            dbContext.Entry(cartItemEntity).Reference(x => x.Cart).IsModified = false;
            dbContext.Entry(cartItemEntity).Property(x => x.TotalPrice).IsModified = false;
           // dbContext.CartItems.Add(cartItemEntity);
            dbContext.SaveChanges();

            return cartItemEntity;

        }
      
    }


}
