using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
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
        public List<Product2> GetAllProducts(int _limit, int _page, string sort = "idaz", int lte = -1, int gte = -1)
        {
            return ProductDAO.getList(_page, _limit, sort, lte, gte);
        }

        [HttpGet]
        //lấy ra một sản phẩm theo id dùng cho trang chi tiết sản phẩm,...
        public Product2 GetOneProductById(int _id)
        {
            return ProductDAO.getProduct(_id);
        }

        [HttpPost("new")]
        //truyền vào tham số [FromBody] Product Product
        public Product2 CreateNewProduct([FromBody] Product2 Product)
        {
            return ProductDAO.AddProduct(Product);
        }

        [HttpPut("new/{id}")]
        //phương thức cập nhật một sản phẩm theo id
        //tham số truyền vào [FromBody] Product Product và id
        public Product2 UpdateProductById(int id,[FromBody] Product2 Product)
        {
            //setID cho product đang giao tiếp
            Product.Id = id;
            return ProductDAO.Save(Product);
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


    }

}
