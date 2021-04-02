using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
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
        public void UpdateImageProduct(ImageProduct imageProduct, int productID)
        {

            if (imageProduct.Product.Id == productID)
            {
                dbContext.Entry(imageProduct.Image).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
