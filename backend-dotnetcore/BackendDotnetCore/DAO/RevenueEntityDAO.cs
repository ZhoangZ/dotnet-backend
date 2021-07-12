using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class RevenueEntityDAO
    {
        private BackendDotnetDbContext dbContext;




        public RevenueEntityDAO()
        {
            dbContext = new BackendDotnetDbContext();

        }

        public RevenueEntity getEntityById(int Id)

        {

            var tmp = dbContext.Revenues.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }

        public RevenueEntity getEntity(int year,int month)

        {

            var tmp = dbContext.Revenues.Where(s => (s.Year == year && s.Month==month));

            return tmp.FirstOrDefault();

        }

        public ICollection<RevenueEntity> getEntitys(int year)

        {

            var tmp = dbContext.Revenues.Where(s => s.Year == year );

            return tmp.ToList();

        }

        public decimal sumYearMoney(int year)

        {

            var tmp = dbContext.Revenues.Where(s => s.Year == year).Sum(i=>i.Money);

            return tmp;

        }

        public RevenueEntity AddEntity(RevenueEntity Product)
        {
            dbContext.Revenues.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public RevenueEntity UpdateRAM(RevenueEntity Product)
        {
            dbContext.Revenues.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public RevenueEntity DeletedEntity(RevenueEntity deleted)
        {
            dbContext.Revenues.Remove(deleted);
            dbContext.SaveChanges();
            return deleted;
        }

    }
}
