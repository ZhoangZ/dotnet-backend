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

        public InformationProduct getEntityById(long Id)

        {

            var tmp = dbContext.InformationProducts.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }
        public ICollection<InformationProduct> getEntitysByForeId(int Id)

        {

            var tmp = dbContext.InformationProducts.Where(s => s.ProductId == Id);

            return tmp.ToList();

        }

        public InformationProduct AddEntity(InformationProduct Product)
        {
            dbContext.InformationProducts.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public ICollection<InformationProduct> AddEntitys(ICollection<InformationProduct> entitys, int productId)
        {

            ICollection<InformationProduct> rs = new List<InformationProduct>();
            if (entitys != null)
            {
                foreach (var e in entitys)
                {          
                    if (e.Id == 0)
                    {
                            if (!e.Deleted)                          
                            {
                                  e.ProductId = productId;
                                dbContext.InformationProducts.Add(e);
                                dbContext.SaveChanges();
                                rs.Add(e);
                            }


                    }
                    else
                    {
                        var tmp2=getEntityById(e.Id);
                        if (tmp2 == null) continue;
                        if (!e.Deleted)
                        {
                            e.ProductId = productId;
                            tmp2.content = e.content;
                            tmp2.name = e.name;
                            dbContext.InformationProducts.Update(tmp2);
                            dbContext.SaveChanges();
                            rs.Add(tmp2);
                        }
                        else
                        {
                            dbContext.InformationProducts.Remove(tmp2);
                            dbContext.SaveChanges();
                        }
                    }
                
                    
                }
            }
            return rs;
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

        public void DeleteAllByProductId(int productId) {
        var tmp = dbContext.InformationProducts.Where(s => s.ProductId == productId)
                ;
          var array=  tmp.ToList();

            foreach (var ci in array)
            {
                dbContext.InformationProducts.Remove(ci);

            }
          dbContext.SaveChanges();}

    }
}
