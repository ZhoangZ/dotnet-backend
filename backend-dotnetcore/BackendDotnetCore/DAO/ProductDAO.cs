using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BackendDotnetCore.DAO
{
    public class ProductDAO
    {
        private BackendDotnetDbContext dbContext;
        public ProductDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }
        public Product getAccount(int Id)

        {
           
          var tmp = from accounts in dbContext.Products
                      where accounts.Id == Id
                      select new Product
                      {
                          Id = accounts.Id,
                          Name = accounts.Name,
                          
                          
                        

                      } ;
            return tmp.ToList()[0];
        }
        public Product getProduct(int Id)

        {

            var tmp = dbContext.Products.Where(s=> s.Id== Id).Include("Images")
                        .FirstOrDefault();
            return tmp;

        }
    }
}
