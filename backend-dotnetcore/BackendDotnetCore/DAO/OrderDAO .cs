using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using BackendDotnetCore.DTO;

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
        public OrderEntity getOrder(int UserId, long cartItemId)

        {
            //Cart cartItem ower User
            var tmp = dbContext.Orders.Where(s => s.UserId == UserId && s.Id.CompareTo(cartItemId) == 0)
                .Include(x => x.User)
                .Include(x => x.Items)

                    .ThenInclude(X => X.Product)
                          .ThenInclude(X => X.Images)
                .Include(x => x.Items)
                    .ThenInclude(X => X.Product)
                        .ThenInclude(X => X.Brand)

                .Include(x => x.Items)
                .ThenInclude(X => X.Product)
                    .ThenInclude(X => X.Ram)

                .Include(x => x.Items)
                .ThenInclude(X => X.Product)
                    .ThenInclude(X => X.Rom)
                

                .Include(x => x.Payment)
                
                ;

            return tmp.FirstOrDefault();

        }

        public OrderItemEntity SaveOrder(OrderItemEntity cartItemEntity)

        {
            dbContext.Entry(cartItemEntity).Reference(x => x.Product).IsModified = false;
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


        /*
         * USER ACTION WITH ORDER (DENY, GET BY STATUS, GET ONE BY ID)
         */
        public ICollection GetOrdersByUserID(int userID)
        {
            
            var list = dbContext.Orders
                .Include(x=>x.Payment)
                .Include(x=>x.Items)                
                .Where(x => x.UserId == userID)
                .ToList();
            
            return list;
        }
        public OrderEntity GetOrderByID(int id)
        {
            var rs = dbContext.Orders
                .Where(x => x.Id == id)
                .Include(x=>x.Items)
                .Include(x=>x.Payment)
                .SingleOrDefault();
           
            return rs;
        }
        public bool DenyOrderByID(int id)
        {
            var rs = dbContext.Orders.Where(x => x.Id == id)
                /* .Include(x => x.Items)
                .Include(x => x.Payment)*/
                .SingleOrDefault();
            //check this order has status is 1 or difference 1;
            if (rs.Status != 1) return false;
            return true;
        }
        //checkComment before
        public bool checkCommentOrder(int productID, int userID, long ido)
        {
            foreach(OrderEntity order in GetOrdersByUserID(userID))
            {
                foreach(OrderItemEntity oi in order.Items)
                if (oi.ProductId == productID && oi.OrderId == ido) return true;
            }
            return false;
        }
        public ICollection<OrderEntity> GetOrdersByUserIDAndStatus(int userID, int status)
        {
            ICollection<OrderEntity> listOrderUser = new List<OrderEntity>();
            var list = dbContext.Orders
                .Where(x => x.UserId == userID && x.Status == status)
                .Include(x=>x.Payment)
                .Include(x=>x.Items)
                .ToList();
           
            return listOrderUser;
        }


        /*
         * ORDER MANAGEMENT VERSION 2
         */
        public int GetCountOrdersByUserID(int userID)
        {
            var list = dbContext.Orders
                .Include(x => x.Payment)
                .Include(x => x.Items)
                .Where(x => x.UserId == userID).ToList();
            return list.Count;
        }
        public List<OrderEntity> GetOrdersByUserID(int userID, int limit, int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = dbContext.Orders
                .Include(x => x.Payment)
                .Include(x => x.Items)
                .Where(x => x.UserId == userID).OrderByDescending(x=>x.Id);
            List<OrderEntity> rs = list.Skip(limit * (page - 1)).Take(limit)
                        .ToList();
            return rs;
        }

        public List<OrderEntity> GetOrdersByUserIDAndStatus(int userID, int status, int limit, int page)
        {
            page = (page <= 0) ? 1 : page;
            var list = dbContext.Orders
                .Where(x => x.UserId == userID && x.Status == status)
                .Include(x => x.Payment)
                .Include(x => x.Items).OrderByDescending(x => x.Id);
            List<OrderEntity> rs = list.Skip(limit * (page - 1)).Take(limit)
                        .ToList();

            return rs;
        }

        public OrderEntity getOrderByIDAndUserID(int orderID, int userID)
        {
            var rs = dbContext.Orders
               .Where(x => x.UserId == userID && x.Id == orderID)
               .Include(x => x.Payment)
               .Include(x => x.Items).SingleOrDefault();
            return rs;
        }



        /*
         * ADMIN ACTION WITH ORDER (GET ALL, GET BY STATUS, ACCEPT ORDERS PENDING)
         */
        public List<OrderEntity> GetAllOrders()
        {
            var list = dbContext.Orders
                 .Include(x => x.Items)
                .Include(x => x.Payment)
                .ToList();
            return list;
        }

        public List<OrderEntity> GetAllOrdersByStatus(int status)
        {
            var list = dbContext.Orders.Where(x=>x.Status == status)
                 .Include(x => x.Items)
                .Include(x => x.Payment)
                .ToList();
            return list;
        }

        public bool AcceptOrderPending(int orderID)
        {
            OrderEntity oe = GetOrderByID(orderID);
            if (null == oe || oe.Status != 1) return false;
            oe.Status = 2;//giao cho bo phan giao hang
            UpdateOrder(oe);
            return oe.Status == 2?true:false;
        }

        //add version 2
        public List<OrderEntity> GetListOrdersPage(int limit, int page, int status)
        {
           
            if (status != 0)
            {
               var list =  dbContext.Orders.Where(x => x.Status == status)
                                .Include(x => x.Items)
                                .Include(x => x.Payment).OrderByDescending(x => x.Id);
                List<OrderEntity> rs = list.Skip(limit * (page - 1)).Take(limit)
                      .ToList();
                return rs;
            }
            else
            {
                var list = dbContext.Orders.Include(x => x.Items)
                                .Include(x => x.Payment).OrderByDescending(x => x.Id);
                List<OrderEntity> rs = list.Skip(limit * (page - 1)).Take(limit)
                      .ToList();
                return rs;
            }
            
        }

        public int GetCountOrdersByStatus(int status)
        {
            if (status == 0)
            {
                var list = dbContext.Orders.ToList();
                return list.Count;
            }
            else
            {
              var list = dbContext.Orders
                                  .Include(x => x.Payment)
                                  .Include(x => x.Items)
                                  .Where(x => x.Status == status).ToList();
                 return list.Count;
            }
        }

    }



}
