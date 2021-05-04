using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}
