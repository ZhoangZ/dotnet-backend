using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BackendDotnetCore.DAO
{
    public class OrderDAO
    {

        private BackendDotnetDbContext dbContext;
        public OrderDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }


        public OrderEntity getCart(OrderEntity cartEntity)
        {
            dbContext.Entry(cartEntity).State = EntityState.Detached;

            return getOrder(cartEntity.UserId, cartEntity.Id);

        }
        public OrderEntity getCart(int UserId)

        {

            var tmp = dbContext.Orders.Where(s => s.UserId == UserId)
                .Include(x => x.User)
                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Product)
                          .ThenInclude(X=>X.Images)
                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Product)
                        .ThenInclude(X => X.Brand)

                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Ram)

                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
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
        public OrderEntity getOrder(int UserId, long cartItemId)

        {
            //Cart cartItem ower User
            var tmp = dbContext.Orders.Where(s => s.UserId == UserId && s.Id.CompareTo(cartItemId) == 0)
                .Include(x => x.User)
                .Include(x => x.Items)

                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Product)
                          .ThenInclude(X => X.Images)
                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Product)
                        .ThenInclude(X => X.Brand)

                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Ram)

                .Include(x => x.Items)
                .ThenInclude(X => X.ProductSpecific)
                    .ThenInclude(X => X.Rom)
                ;

            return tmp.FirstOrDefault();

        }

        public OrderItemEntity SaveOrder(OrderItemEntity cartItemEntity)

        {
            dbContext.Entry(cartItemEntity).Reference(x => x.ProductSpecific).IsModified = false;
            //dbContext.Entry(cartItemEntity).Reference(x => x.ProductSpecific.Product).IsModified = false;
            dbContext.Entry(cartItemEntity).Reference(x => x.Order).IsModified = false;
           
           // dbContext.CartItems.Add(cartItemEntity);
            dbContext.SaveChanges();
            if (cartItemEntity.Deleted == true)
            {
                dbContext.Remove(cartItemEntity);
            }
            return cartItemEntity;

        }
        public OrderEntity SaveOrder(OrderEntity cartItemEntity)

        {
            dbContext.Entry(cartItemEntity).Collection(x => x.Items).IsModified = false;
            dbContext.Entry(cartItemEntity).Property(x => x.TotalItem).IsModified = false;
            dbContext.Entry(cartItemEntity).Property(x => x.TotalPrice).IsModified = false;

            dbContext.Orders.Add(cartItemEntity);
            dbContext.SaveChanges();
            
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
