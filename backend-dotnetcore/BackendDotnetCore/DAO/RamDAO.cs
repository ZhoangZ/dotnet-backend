using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class RamDAO
    {
        private BackendDotnetDbContext dbContext;




        public RamDAO()
        {
            dbContext = new BackendDotnetDbContext();

        }

        public RamEntity getRamById(int Id)

        {

            var tmp = dbContext.Rams.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }

        public RamEntity AddEntity(RamEntity Product)
        {
            dbContext.Rams.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public RamEntity UpdateRAM(RamEntity Product)
        {
            dbContext.Rams.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public RamEntity DeletedEntity(RamEntity deleted)
        {
            dbContext.Rams.Remove(deleted);
            dbContext.SaveChanges();
            return deleted;
        }


    }
}
