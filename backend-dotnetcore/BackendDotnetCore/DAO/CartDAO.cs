﻿using BackendDotnetCore.EF;
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


        public CartEntity getCart(CartEntity cartEntity)
        {
            dbContext.Entry(cartEntity).State = EntityState.Detached;

            return getCart(cartEntity.UserId);

        }
        public CartEntity getCart(int UserId)

        {

            var tmp = dbContext.Carts.Where(s => s.UserId == UserId)
                .Include(x => x.User)
                .Include(x => x.Items)
                
                    .ThenInclude(X => X.Product)
                          .ThenInclude(X=>X.Images)
                .Include(x => x.Items)
                
                    .ThenInclude(X => X.Product)
                        .ThenInclude(X => X.Brand)

                .Include(x => x.Items)
                .ThenInclude(X => X.Product)
                    .ThenInclude(X => X.Ram)

                .Include(x => x.Items)
                 .ThenInclude(X => X.Product)
                    .ThenInclude(X => X.Rom)



                ;
            
            return tmp.FirstOrDefault();

        }
        public void deleteAllItemCart(int UserId)
        {
            var tmp=dbContext.Carts.Where(s => s.UserId == UserId).Include(x => x.Items);
            CartEntity c=tmp.FirstOrDefault();

            foreach (var ci in c.Items)
            {
                dbContext.Remove(ci);

            }
            dbContext.SaveChanges();
        }
        public CartEntity getCart(int UserId, long cartItemId)

        {
            //Cart cartItem ower User
            var tmp = dbContext.Carts.Where(s => s.UserId == UserId)
                .Include(x => x.Items)
                .Where(y => y.Items.Any(U=> U.Id.CompareTo( cartItemId)==0))
                ;

            return tmp.FirstOrDefault();

        }

        public CartItemEntity SaveCart(CartItemEntity cartItemEntity)

        {
            dbContext.Entry(cartItemEntity).Reference(x => x.Product).IsModified = false;
            dbContext.Entry(cartItemEntity).Reference(x => x.Cart).IsModified = false;
           
           // dbContext.CartItems.Add(cartItemEntity);
            dbContext.SaveChanges();
            if (cartItemEntity.Deleted == true)
            {
                dbContext.Remove(cartItemEntity);
            }
            return cartItemEntity;

        }

        public IEnumerable<CartItemEntity> RemoveCart(IEnumerable<CartItemEntity> cartItemEntity)

        {
            foreach(var c in cartItemEntity)
            {
            dbContext.Remove(c);

            }
            dbContext.SaveChanges();

            return cartItemEntity;

        }

    }


}
