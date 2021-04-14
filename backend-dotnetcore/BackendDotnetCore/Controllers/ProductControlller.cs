using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using BackendDotnetCore.Models;
using BackendDotnetCore.Ultis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("product")]
    public class ProductControlller : ControllerBase
    {
        //auto wired productDAO
        private Product2DAO ProductDAO = new Product2DAO();


        //lấy danh sách dữ liệu sản phẩm theo tiêu chí
        [HttpGet("list")]
        [HttpGet("/products")]
        public ActionResult GetAllProducts(int _limit=10, int _page=1, string sort = "id:asc", int lte = -1, int gte = -1)
        {
            List<Product2> lst = ProductDAO.getList(_page, _limit, sort, lte, gte);
            int toltal = ProductDAO.Total();
            lst.setRequset(Request);

            PageResponse pageResponse = new PageResponse();
            pageResponse.Data = lst;
            pageResponse.Pagination = new Pagination(_limit, _page, toltal);


            return Ok(pageResponse);
        }

        [HttpGet]
        //lấy ra một sản phẩm theo id dùng cho trang chi tiết sản phẩm,...
        public Product2 GetOneProductById(int _id)
        {
            Product2 product = ProductDAO.getProduct(_id);
            product.Images.ForEach(delegate (ImageProduct ip) {
                ip.setRequest(Request);
            });
            return product;
        }

        [HttpPost]
        //truyền vào tham số [FromBody] Product Product
        public Product2 CreateNewProduct([FromBody] Product2 Product)
        {
            Product.CreatedAt = DateTime.UtcNow;
            Product2 product = ProductDAO.AddProduct(Product);
            product.Images.ForEach(delegate (ImageProduct ip) {
                ip.setRequest(Request);
            });
            return product;
        }

        [HttpPut("{id}")]
        //phương thức cập nhật một sản phẩm theo id
        //tham số truyền vào [FromBody] Product Product và id
        public ActionResult UpdateProductById([FromBody] Product2 Product)
        {
            int rs=ProductDAO.Save(Product);
            if(rs!=0)
            return Ok();
            return BadRequest();
        }

       
        [HttpDelete]
        //phương thức delete danh sách sản phẩm theo id
        //tham số truyền vào là một mảng id
        public void deleteProducts(int[] ids)
        {
            foreach(int id in ids)
            {
                    ProductDAO.RemoveProductById(id);
                    Console.WriteLine("Remove productID={0}", id);
            }
        }

        [HttpDelete("{id}")]
        //phương thức delete danh sách sản phẩm theo id
        //tham số truyền vào là một mảng id
        public ActionResult deleteProduct(int id)
        {
          
            int rs= ProductDAO.RemoveProductById(id);
            if(rs==1)
            return Ok();
            return BadRequest();
        
            
        }


    }

}
