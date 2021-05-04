using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BackendDotnetCore.DAO
{
    public class PaymentDAO
    {
        private BackendDotnetDbContext dbContext;
      
        public PaymentDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }
       
        public PaymentEntity getPayment(int Id)

        {

            var tmp = dbContext.Payments.Where(s => s.Id == Id) ;
         
            return tmp.FirstOrDefault(); 

        }
        public List<PaymentEntity> getList(int _page, int _limit, string _sort)

        {
            _page=(_page<=0)?1:_page;
            var tmp = dbContext.Payments.Where(x=>x.Id!=1)

            
                ;
           

            string [] strs=_sort.Split(",");
         
            foreach (var str in strs)
            {
                string key = str.ToLower();
              
                if (key.CompareTo("id:asc") == 0)
                {
                  
                    tmp = tmp.OrderBy(x => x.Id);

                }
                else if (key.CompareTo("id:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.Id);

                }
               
            }

            List <PaymentEntity> rs= tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<PaymentEntity>();
            return rs;

        }
       
        public PaymentEntity AddPayment(PaymentEntity Product)
        {
            dbContext.Payments.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public PaymentEntity UpdatePayment(PaymentEntity Product)
        {
            dbContext.Payments.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }

        public int Save(PaymentEntity Product)
        {
            int rs = 0;
            if (Product != null)
            {
                dbContext.Update<PaymentEntity>(Product);
                rs = dbContext.SaveChanges();
            }
               
            return rs;
        }
   
        

        

       

    }
}
