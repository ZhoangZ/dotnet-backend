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
        private ProductDAO ProductDAO = new ProductDAO();

        //lấy danh sách dữ liệu sản phẩm theo tiêu chí
        [HttpGet("list")]
        public List<Product> GetAllProducts(int _page, int _limit, string sort)
        {
            return ProductDAO.getList(_page, _limit, sort);
        }

        [HttpGet]
        //lấy ra một sản phẩm theo id dùng cho trang chi tiết sản phẩm,...
        public Product GetOneProductById(int _id)
        {
            return ProductDAO.getProduct(_id);
        }

        [HttpPost("new")]
        //truyền vào tham số [FromBody] Product Product
        public Product CreateNewProduct([FromBody] Product Product)
        {
            return ProductDAO.AddProduct(Product);
        }

        [HttpPut("new/{id}")]
        //phương thức cập nhật một sản phẩm theo id
        //tham số truyền vào [FromBody] Product Product và id
        public Product UpdateProductById(int id,[FromBody] Product Product)
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
