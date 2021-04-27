using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace BackendDotnetCore.DAO
{
    public class Product2DAO
    {
        private BackendDotnetDbContext dbContext;
        private ImageProductDAO imageProductDAO;
        public Product2DAO()
        {
            this.dbContext = new BackendDotnetDbContext();
            this.imageProductDAO = new ImageProductDAO();
        }
        public Product2 getAccount(int Id)

        {
           
          var tmp = from accounts in dbContext.Products
                      where accounts.Id == Id
                      select new Product2
                      {
                          Id = accounts.Id,
                          Name = accounts.Name,
                          
                          
                        

                      } ;
            return tmp.ToList()[0];
        }
        public Product2 getProduct(int Id)

        {

            var tmp = dbContext.Products.Where(s => s.Id == Id).Where(X => X.deleted == false)

                .Include(x => x.Specifics).ThenInclude(x => x.Ram).Include(x => x.Specifics)
                .ThenInclude(x => x.Rom).Include("Images").Include(x => x.Informations).Include(x => x.Brand)
                      ;
         
            return tmp.FirstOrDefault(); 

        }
        public List<Product2> getList(int _page, int _limit, string _sort, int salePrice_lte, int salePrice_gte, int brand_id, int rom_id, int ram_id)

        {
            _page=(_page<=0)?1:_page;
            var tmp = dbContext.Products.Where(X => X.deleted == false)

              .Include(x => x.Specifics).ThenInclude(x => x.Ram)
              .Include(x => x.Specifics).ThenInclude(x => x.Rom)
              .Include(x => x.Brand)
              .Include("Images")
              
              //.Include("Informations")
                ;
            if (salePrice_lte != -1)
            {
               // Console.WriteLine(lgt);
                tmp = tmp.Where(x => (x.SalePrice  <= salePrice_lte ));

            }
            if (salePrice_gte != -1)
            {
               // Console.WriteLine(gte);
                tmp = tmp.Where(x => (x.SalePrice  >= salePrice_gte));
            }
            if (salePrice_gte != -1)
            {
                // Console.WriteLine(gte);
                tmp = tmp.Where(x => (x.SalePrice >= salePrice_gte));
            }

            if (brand_id > 0)
            {
                //Console.WriteLine(brand_id);
                tmp = tmp.Where(x => x.Brand.Id == brand_id);
            }

            if (rom_id > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.Specifics.Any(y => y.Rom.Id == rom_id));
            }
            if (ram_id > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.Specifics.Any(y => y.Ram.Id == ram_id));
            }

            string [] strs=_sort.Split(",");
            /*if (strs.Length == 0) 
                strs[0] = sort ;
                //strs = new string[] { sort };*/
            foreach (var str in strs)
            {
                string key = str.ToLower();
               /* if (key.CompareTo("idasc")==0)
                {
                    //Console.WriteLine("asc");
                    tmp=tmp.OrderBy(x => x.Id);

                }
                else if (key.CompareTo("iddesc")==0)
                {
                    //Console.WriteLine("desc");
                    tmp=tmp.OrderByDescending(x => x.Id);

                }
                else if (key.CompareTo("salepricedesc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.OriginalPrice * (100 - x.promotionPercents));

                }
                else if (key.CompareTo("salepriceasc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderBy(x => x.OriginalPrice * (100 - x.promotionPercents));

                }else*/
                // format :asc
                if (key.CompareTo("id:asc") == 0)
                {
                    //Console.WriteLine("asc");
                    tmp = tmp.OrderBy(x => x.Id);

                }
                else if (key.CompareTo("id:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.Id);

                }
                else if (key.CompareTo("saleprice:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.SalePrice);

                }
                else if (key.CompareTo("saleprice:asc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderBy(x => x.SalePrice);

                }
            }

            List < Product2 > rs= tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<Product2>();
            return rs;

        }
        public int getCount(string _sort, int salePrice_lte, int salePrice_gte, int brand_id, int rom_id, int ram_id)

        {
            
            var tmp = dbContext.Products.Where(X => X.deleted == false)

              .Include(x => x.Specifics).ThenInclude(x => x.Ram)
              .Include(x => x.Specifics).ThenInclude(x => x.Rom)
              .Include(x => x.Brand)
              .Include("Images")
                //.Include("Informations")
               ;
           
          


            if (salePrice_lte != -1)
            {
                // Console.WriteLine(lgt);
                tmp = tmp.Where(x => (x.SalePrice <= salePrice_lte));

            }
            if (salePrice_gte != -1)
            {
                // Console.WriteLine(gte);
                tmp = tmp.Where(x => (x.SalePrice >= salePrice_gte));
            }
            if (salePrice_gte != -1)
            {
                // Console.WriteLine(gte);
                tmp = tmp.Where(x => (x.SalePrice >= salePrice_gte));
            }

            if (brand_id > 0)
            {
                //Console.WriteLine(brand_id);
                tmp = tmp.Where(x => x.Brand.Id == brand_id);
            }
            if (rom_id > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.Specifics.Any(y => y.Rom.Id == rom_id));
            }
            if (ram_id > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.Specifics.Any(y => y.Ram.Id == ram_id));
            }



            string[] strs = _sort.Split(",");
            /*if (strs.Length == 0) 
                strs[0] = sort ;
                //strs = new string[] { sort };*/
            foreach (var str in strs)
            {
                string key = str.ToLower();                
                if (key.CompareTo("id:asc") == 0)
                {
                    //Console.WriteLine("asc");
                    tmp = tmp.OrderBy(x => x.Id);

                }
                else if (key.CompareTo("id:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.Id);

                }
                else if (key.CompareTo("saleprice:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.SalePrice);

                }
                else if (key.CompareTo("saleprice:asc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderBy(x => x.SalePrice);

                }
            }

            int rs = tmp.Count();
            return rs;

        }

        //phương thức insert into table product
        public Product2 AddProduct(Product2 Product)
        {
            dbContext.Products.AddAsync(Product);
            dbContext.SaveChangesAsync();
            return Product;
        }
        
        //phương thức cập nhật product by id
        public int Save(Product2 Product)
        {
            int rs = 0;
            if (Product != null)
            {
                dbContext.Update<Product2>(Product);
                rs = dbContext.SaveChanges();
            }
               
            return rs;
        }
   
        //phương thức xóa từng phần tử product bằng id khi nhận từ request
        public int RemoveProductById(int Id)
        {
            Product2 product = getProduct(Id);
            int rs = 0;
            if (product != null)
            {
                product.deleted = true;
                dbContext.Update<Product2>(product);
                rs=dbContext.SaveChanges();
            }
            return rs;
        }

        

        public int Total()
        {
           int rs= dbContext.Products.Where(X => X.deleted == false).Count();
            return rs;
        }
        public List<Brand> GetBrands()
        {

            return dbContext.Brands.ToList();
        }
        public List<Brand> GetActivedBrands()
        {

            return dbContext.Brands.Where(e => e.Actived).ToList();
        }
        public List<RomEntity> GetRoms()
        {

            return dbContext.Roms.ToList();
        }
        public List<RamEntity> GetRams()
        {

            return dbContext.Rams.ToList();
        }


    }
}
