using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BackendDotnetCore.DAO
{
    public class OrderDAO
    {

        private BackendDotnetDbContext dbContext;
        public Product2DAO productDAO;
        public OrderDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
            productDAO = new Product2DAO();
        }


        public OrderEntity getOrder(OrderEntity cartEntity)
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
                

                .Include(x => x.Payment)
                
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

      /*  public OrderEntity getCart2(OrderEntity cartEntity)
        {
            dbContext.Entry(cartEntity).State = EntityState.Detached;

            return getCart(cartEntity.Id);

        }*/
        public OrderEntity SaveOrder(OrderEntity orderEntity)

        {
            dbContext.Entry(orderEntity).Collection(x => x.Items).IsModified = false;
            dbContext.Entry(orderEntity).Property(x => x.TotalItem).IsModified = false;
            dbContext.Entry(orderEntity).Property(x => x.TotalPrice).IsModified = false;
            dbContext.Entry(orderEntity).Reference(x => x.Payment).IsModified = false;
            //dbContext.Entry(orderEntity).Property(x => x.PaymentId).IsModified = false;
            dbContext.Orders.Add(orderEntity);
            dbContext.SaveChanges();
            Console.WriteLine("idOrder: {0}", orderEntity.Id);
           // dbContext.Entry(cartItemEntity).Reload();
            return orderEntity;
           // return getOrder(orderEntity);

        }

        public OrderEntity UpdateOrder(OrderEntity orderEntity)

        {
            Console.WriteLine("PaymentId1: {0}", orderEntity.PaymentId);
            dbContext.Entry(orderEntity).Collection(x => x.Items).IsModified = false;
            dbContext.Entry(orderEntity).Property(x => x.TotalItem).IsModified = false;
            dbContext.Entry(orderEntity).Property(x => x.TotalPrice).IsModified = false;
            //dbContext.Entry(orderEntity).Reference(x => x.Payment).IsModified = false;
            //dbContext.Entry(orderEntity).Property(x => x.PaymentId).IsModified = true;

            
            Console.WriteLine("PaymentId2: {0}", orderEntity.PaymentId);
            dbContext.SaveChanges();
            // dbContext.Entry(cartItemEntity).Reload();
            return orderEntity;
            // return getOrder(orderEntity);

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


        //user action with orders
        public ArrayList GetOrdersByUserID(int userID)
        {
            ArrayList listOrderUser = new ArrayList();
            var list = dbContext.Orders.Where(x => x.UserId == userID).ToList();
            foreach(OrderEntity o in list)
            {
                var orderItem = dbContext.OrderItems.Where(x => x.OrderId == o.Id).ToList();
                foreach (OrderItemEntity oi in orderItem) {
                    var productSpecifics = dbContext.product2Specifics.Where(x => x.Id == oi.ProductSpecificId).SingleOrDefault();
                    var product = productDAO.getProductBySpecificID((int)productSpecifics.Id);
                    productSpecifics.Product = product;
                    oi.ProductSpecific = productSpecifics;
                }
                o.Items = orderItem;
                listOrderUser.Add(o);
            }
            return listOrderUser;
        }

    }


}
