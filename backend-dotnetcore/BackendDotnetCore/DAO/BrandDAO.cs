using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class BrandDAO
    {
        private BackendDotnetDbContext dbContext;




        public BrandDAO()
        {
            dbContext = new BackendDotnetDbContext();

        }

        public Brand getEntityById(int Id)

        {

            var tmp = dbContext.Brands.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }

        public Brand AddEntity(Brand Product)
        {
            dbContext.Brands.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public Brand UpdateRAM(Brand Product)
        {
            dbContext.Brands.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public Brand DeletedEntity(Brand deleted)
        {
            dbContext.Brands.Remove(deleted);
            dbContext.SaveChanges();
            return deleted;
        }

    }
}
