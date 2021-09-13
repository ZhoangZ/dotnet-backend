using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class ImageProductDAO
    {
        private BackendDotnetDbContext dbContext;

        public ImageProductDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }



        //phương thức cập nhật hình ảnh sản phẩm
        public void UpdateImageProduct(ImageProduct imageProduct, Product2 product, long id)
        {
           
            Console.WriteLine("imageId={0},productId={1},id={2}",imageProduct.Id, product.Id, id);
            int I = product.Images.Count;
            
                if (imageProduct.Product.Id == product.Id && imageProduct.Id == id)
                {
                    dbContext.Entry(imageProduct).State = EntityState.Modified;
                    dbContext.SaveChanges();
                }
            
        }

        public ImageProduct DeletedEntity(ImageProduct deleted)
        {
            dbContext.Images.Remove(deleted);
            dbContext.SaveChanges();
           
            var a=FileProcess.FileProcess.getFullPath("product\\" + deleted._image); //we are using Temp file name just for the example. Add your own file path.
            if (File.Exists(a))
            {
                // If file found, delete it    
                File.Delete(a);
               
                Console.WriteLine("File deleted.");
            }
            return deleted;
        }
        public ImageProduct DeletedEntity2(ImageProduct deleted)
        {
            dbContext.Images.Remove(deleted);
            dbContext.SaveChanges();
            if (Count(deleted._image) != 1) return deleted;
            var a = FileProcess.FileProcess.getFullPath("product\\" + deleted._image); //we are using Temp file name just for the example. Add your own file path.
            if (File.Exists(a))
            {
                // If file found, delete it    
                File.Delete(a);

                Console.WriteLine("File deleted.");
            }
            return deleted;
        }

        public ImageProduct getEntityById(int Id)

        {

            var tmp = dbContext.Images.Where(s => s.Id == Id);

            return tmp.FirstOrDefault();

        }

        public int Count(string image)

        {

            var tmp = dbContext.Images.Where(s => s._image == image);
            int rs=tmp.Count();
            return rs;

        }

        public ImageProduct AddEntity(ImageProduct Product)
        {
            dbContext.Images.Add(Product);
            dbContext.SaveChanges();
            return Product;
        }
        public ImageProduct UpdateEntity(ImageProduct Product)
        {
            dbContext.Images.Update(Product);
            dbContext.SaveChanges();
            return Product;
        }

        public List<ImageProduct> getList(int _page, int _limit, string _sort)

        {
            _page = (_page <= 0) ? 1 : _page;
            IQueryable<ImageProduct> tmp;
           
            tmp = dbContext.Images;


           
            string[] strs = _sort.Split(",");
           
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
               
            }
            
            List<ImageProduct> rs = tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<ImageProduct>();
            return rs;

        }
        public int getCount()

        {
            IQueryable<ImageProduct> tmp;            
            tmp = dbContext.Images;          
            int rs = tmp.Count();
            return rs;

        }

        public List<ImageProduct> getList(int _page, int _limit, string _sort, int productId)

        {
            _page = (_page <= 0) ? 1 : _page;
            IQueryable<ImageProduct> tmp;

            tmp = dbContext.Images.Where(X=>X.ProductId==productId);



            string[] strs = _sort.Split(",");

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

            }

            List<ImageProduct> rs = tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<ImageProduct>();
            return rs;

        }
        public int getCount( int productId)

        {
            IQueryable<ImageProduct> tmp;
            tmp = dbContext.Images.Where(X => X.ProductId == productId);
            int rs = tmp.Count();
            return rs;

        }
    }
}
