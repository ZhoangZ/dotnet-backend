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
        public List<Product> getList(int _page, int _limit, string sort)

        {
            _page=(_page<=0)?1:_page;
            var tmp = dbContext.Products.Include("Images");
            string [] strs=sort.Split(",");
            /*if (strs.Length == 0) 
                strs[0] = sort ;
                //strs = new string[] { sort };*/
            foreach (var str in strs)
            {
                if (str.CompareTo("idaz")==0)
                {
                    //Console.WriteLine("asc");
                    tmp=tmp.OrderBy(x => x.Id);

                }
                else if (str.CompareTo("idza")==0)
                {
                    //Console.WriteLine("desc");
                    tmp=tmp.OrderByDescending(x => x.Id);

                }
            }

            List < Product > rs= tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<Product>();
            return rs;

        }
    }
}
