using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace BackendDotnetCore.DAO
{
    public class Product2DAO
    {
        private BackendDotnetDbContext dbContext;
        

       

        public Product2DAO()
        {
            dbContext = new BackendDotnetDbContext();
           
        }
       
        public Product2 getProduct(int Id)

        {

            var tmp = dbContext.Products.Where(s => s.Id == Id)
                .Where(X => X.deleted == false)
                .Include("Images")
                .Include(x => x.Ram)
                .Include(x => x.Rom)
                .Include(x => x.Informations)
                .Include(x => x.Brand)
                .Include(x => x.commentOrders);//new 070902019                ;
         
            return tmp.SingleOrDefault(); 

        }
        //lay san pham trong order kem theo comment theo order do
        public Product2 getProductByOrderID(int Id, long orderID)

        {

            var tmp = dbContext.Products.Where(s => s.Id == Id)
                .Where(X => X.deleted == false)
                .Include("Images")
                .Include(x => x.Ram)
                .Include(x => x.Rom)
                .Include(x => x.Informations)
                .Include(x => x.Brand)
                .Include(x => x.commentOrders).SingleOrDefault();//new 070902019            

            List<CommentEntity> lsComments = new List<CommentEntity>();
            foreach(CommentEntity ce in tmp.commentOrders)
            {
                if(ce.orderID == orderID)
                {
                    lsComments.Add(ce);

                }
            }
            tmp.commentOrders = lsComments;

            return tmp;

        }

        // public Product2 getProductBySpecificID(int specificID)
        // {
        //     var specific = dbContext.product2Specifics.Where(s => s.Id == specificID).Include(x => x.Product).FirstOrDefault();
        //     return getProduct(specific.Product.Id);
        // }

        // public Product2Specific getSpecific(long Id)
        // {
        //     var tmp = dbContext.product2Specifics.Where(s => s.Id == Id);
        //     return tmp.FirstOrDefault();
        // }



        public List<Product2> getList(int _page, int _limit, string _sort, int salePrice_lte, int salePrice_gte, int brand_id, int rom_id, int ram_id,int isHot, string title_like, int deleted)

        {
            _page=(_page<=0)?1:_page;
            IQueryable<Product2> tmp1;
            if (deleted != -1)
            {
                tmp1 = dbContext.Products.Where(X => X.deleted == (deleted == 1));
            }
                else
                tmp1 = dbContext.Products;


            var tmp=tmp1.Include(x => x.Brand)
               .Include(x => x.Ram)
               .Include(x => x.Rom)
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
                tmp = tmp.Where(x => x.RomId == rom_id);
            }
            if (ram_id > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.RamId == ram_id);
            }
          
            if (isHot > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.IsHot==true);
            }
            if (title_like !=null && title_like.Length>0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.Name.Contains(title_like));
            }

            string [] strs=_sort.Split(",");
            /*if (strs.Length == 0) 
                strs[0] = sort ;
                //strs = new string[] { sort };*/
            foreach (var str in strs)
            {
                string key = str.ToLower();
              
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
                // sort CreatedAt
                else if (key.CompareTo("createdat:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.CreatedAt);

                }
                else if (key.CompareTo("createdat:asc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderBy(x => x.CreatedAt);

                }

                // sort UpdatedAt
                else if (key.CompareTo("updatedat:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.UpdatedAt);

                }
                else if (key.CompareTo("updatedat:asc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderBy(x => x.UpdatedAt);

                }
                // sort AmoutSold
                else if (key.CompareTo("amountsold:desc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderByDescending(x => x.AmountSold);

                }
                else if (key.CompareTo("amountsold:asc") == 0)
                {
                    //Console.WriteLine("desc");
                    tmp = tmp.OrderBy(x => x.AmountSold);

                }
            }

            List < Product2 > rs= tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<Product2>();
            return rs;

        }
        public int getCount(int salePrice_lte, int salePrice_gte, int brand_id, int rom_id, int ram_id,int isHot, string title_like, int deleted)

        {
            IQueryable<Product2> tmp1;
            if (deleted != -1)
            {
                tmp1 = dbContext.Products.Where(X => X.deleted == (deleted == 1));
            }
            else
                tmp1 = dbContext.Products;
            var tmp = tmp1

             
             
               .Include(x => x.Brand)
               .Include(x => x.Ram)
               .Include(x => x.Rom)
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
                tmp = tmp.Where(x => x.Rom.Id == rom_id);
            }
            if (ram_id > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.RamId==ram_id);
            }
            if (isHot > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.IsHot==true);
            }


            if (title_like != null && title_like.Length > 0)
            {
                //Console.WriteLine("rom_id {0}", rom_id);               
                tmp = tmp.Where(x => x.Name.Contains(title_like));
            }


            int rs = tmp.Count();
            return rs;

        }


        //phương thức insert into table product
        public Product2 AddProduct(Product2 Product)
        {
            dbContext.Entry(Product).Reference(x => x.Ram).IsModified = false;
            dbContext.Entry(Product).Reference(x => x.Rom).IsModified = false;
            dbContext.Entry(Product).Reference(x => x.Brand).IsModified = false;
            dbContext.Entry(Product).Collection(x => x.Images).IsModified = false;
            dbContext.Entry(Product).Collection(x => x.Informations).IsModified = false;
            dbContext.Entry(Product).Collection(x => x.commentOrders).IsModified = false;
            dbContext.Products.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }

        

        //phương thức cập nhật product by id
        public Product2 Save(Product2 Product)
        {
            dbContext.Entry(Product).Reference(x => x.Ram).IsModified = false;
            dbContext.Entry(Product).Reference(x => x.Rom).IsModified = false;
            dbContext.Entry(Product).Reference(x => x.Brand).IsModified = false;
            dbContext.Entry(Product).Collection(x => x.Images).IsModified = false;
            dbContext.Entry(Product).Collection(x => x.Informations).IsModified = false;
            dbContext.Entry(Product).Collection(x => x.commentOrders).IsModified = false;

            dbContext.Update<Product2>(Product);
            dbContext.SaveChanges();
            return Product;


           
        }
   
        //phương thức xóa từng phần tử product bằng id khi nhận từ request
        public Product2 RemoveProductById(int Id)
        {
            Product2 product = getProduct(Id);
           
            if (product != null)
            {
                product.UpdatedAt = DateTime.Now;
                int rs = 0;
                product.deleted = true;
                dbContext.Update<Product2>(product);
                rs=dbContext.SaveChanges();
                if (rs != 0) return product;
            }
            return null;
        }

        

        public int Total()
        {
           int rs= dbContext.Products.Where(X => X.deleted == false).Count();
            return rs;
        }
        public List<Brand> GetBrands(int deleted)
        {
            if(deleted!=-1)return dbContext.Brands.Where(e=>e.Deleted==(deleted==1)).ToList();
            return dbContext.Brands.ToList();
        }
        public List<Brand> GetActivedBrands()
        {

            return dbContext.Brands.Where(e => e.Actived).Where(e=>!e.Deleted).ToList();
        }
        public List<RomEntity> GetRoms(int deleted)
        {

            if (deleted != -1)  return dbContext.Roms.Where(e => e.Deleted == (deleted == 1)).ToList();
            return dbContext.Roms.Where(e => !e.Deleted).ToList();
        }
        public List<RamEntity> GetRams(int deleted)
        {

            if (deleted != -1)  return dbContext.Rams.Where(e => e.Deleted == (deleted == 1)).ToList();
            return dbContext.Rams.Where(e => !e.Deleted).ToList();
        }


    }
}
