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
        private ImageProductDAO imageProductDAO;
        public ProductDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
            this.imageProductDAO = new ImageProductDAO();
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
                    Console.WriteLine("asc");
                    tmp=tmp.OrderBy(x => x.Id);

                }
                else if (str.CompareTo("idza")==0)
                {
                    Console.WriteLine("desc");
                    tmp=tmp.OrderByDescending(x => x.Id);

                }
            }

            List < Product > rs= tmp.Skip(_limit * (_page - 1)).Take(_limit)
                        .ToList<Product>();
            return rs;

        }
    
        //phương thức insert into table product
        public Product AddProduct(Product Product)
        {
            dbContext.Products.AddAsync(Product);
            dbContext.SaveChangesAsync();
            return Product;
        }
        
        //phương thức cập nhật product by id
        public Product Save(Product Product)
        {
            if(Product.Id != 0)
            {
                Console.WriteLine("Icập nhật product id={0}",Product.Id);
                //cập nhật
                Product OldProduct =  getProduct(Product.Id);
                //lấy dữ liệu thay đổi
                int promotionPercents = Product.promotionPercents;
                string name = Product.Name;
                string brand = Product.Brand;
                int memory = Product.Memory;
                int ram = Product.RAM;
                double originalPrice = Product.OriginalPrice;
                string description = Product.DESCRIPTION;
                DateTime createdAt = Product.CreatedAt;
                int amount_SOLD = Product.AMOUNT_SOLD;
                string os = Product.OS;
                double goalPrice = Product.GoalPrice;
                List<ImageProduct> Images = Product.Images;

                /*
                 * source reference:https://www.learnentityframeworkcore.com/dbcontext/modifying-data
                */
                //phải kêu thêm cập nhật thuộc tính hình ảnh với ImageProductDAO
               foreach(ImageProduct image in Product.Images)
                {
                    imageProductDAO.UpdateImageProduct(image);
                }
                dbContext.Entry(Product).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            else
            {
                //thêm mới
                Console.WriteLine("Không có id, thêm mới product");
            }
            return Product;
        }
   
        //phương thức xóa từng phần tử product bằng id khi nhận từ request
        public void RemoveProductById(int[] ArrId)
        {
            foreach(int id in ArrId)
            {
                //var author = dbContext.Products.Single(a => a.Id == id);
                //var books = dbContext.ImageProduct.Where(b => EF.Property<int>(b, "AuthorId") == 1);
                //foreach (var book in books)
                //{
                //    author.Books.Remove(book);
                //}
                //context.Remove(author);
                //context.SaveChanges();
                Product product = getProduct(id);
                dbContext.Remove(product);
                dbContext.SaveChanges();
            }
        }
    
    }
}
