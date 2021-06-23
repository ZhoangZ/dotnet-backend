using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class RomDAO
    {
        private BackendDotnetDbContext dbContext;




        public RomDAO()
        {
            dbContext = new BackendDotnetDbContext();

        }

        public RomEntity getRamById(int Id)

        {

            var tmp = dbContext.Roms.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }

        public RomEntity AddEntity(RomEntity Product)
        {
            dbContext.Roms.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public RomEntity UpdateRAM(RomEntity Product)
        {
            dbContext.Roms.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }


    }
}
