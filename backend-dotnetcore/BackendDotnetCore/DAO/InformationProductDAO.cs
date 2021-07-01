using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class InformationProductDAO
    {
        private BackendDotnetDbContext dbContext;




        public InformationProductDAO()
        {
            dbContext = new BackendDotnetDbContext();

        }

        public InformationProduct getEntityById(int Id)

        {

            var tmp = dbContext.InformationProducts.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }
        public ICollection< InformationProduct> getEntitysByForeId(int Id)

        {

            var tmp = dbContext.InformationProducts.Where(s => s.ProductId == Id);

            return tmp.ToList() ;

        }

        public InformationProduct AddEntity(InformationProduct Product)
        {
            dbContext.InformationProducts.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public InformationProduct UpdateEntity(InformationProduct Product)
        {
            dbContext.InformationProducts.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }

        public InformationProduct DeletedEntity(InformationProduct deleted)
        {
            dbContext.InformationProducts.Remove(deleted);
            dbContext.SaveChanges();          
            return deleted;
        }

    }
}
