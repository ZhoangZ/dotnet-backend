using BackendDotnetCore.DAO;
using BackendDotnetCore.Enitities;
using BackendDotnetCore.Models;
using BackendDotnetCore.Response;
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
        public ActionResult GetAllProducts(int _limit=10, int _page=1, string _sort = "id:asc", int salePrice_lte = -1, int salePrice_gte = -1)
        {
            try
            {
                List<Product2> lst = ProductDAO.getList(_page, _limit, _sort, salePrice_lte, salePrice_gte);
                int toltal = ProductDAO.Total();
                lst.setRequset(Request);
                PageResponse pageResponse = new PageResponse();
                pageResponse.Data = lst;
                pageResponse.Pagination = new Pagination(_limit, _page, toltal);
                return Ok(pageResponse);
            }
            catch(Exception e)
            {
                return BadRequest(new MessageResponse("Không lấy được danh sách sản phẩm.", "None product take"));
            }
          
        }

        [HttpGet]
        [HttpGet("{_id}")]
        //lấy ra một sản phẩm theo id dùng cho trang chi tiết sản phẩm,...
        public ActionResult GetOneProductById(int _id)
        {
            //Console.WriteLine(_id);
            try
            {
                Product2 product = ProductDAO.getProduct(_id);
                if (product == null) return BadRequest();
                product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(new MessageResponse("Không lấy được sản phẩm.", "None product take"));
            }
           
        }

        [HttpPost]
        //truyền vào tham số [FromBody] Product Product
        public ActionResult CreateNewProduct([FromBody] Product2 Product)
        {
            try
            {
                Product.CreatedAt = DateTime.UtcNow;
                Product2 product = ProductDAO.AddProduct(Product);
                if (product == null) return BadRequest();
                product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });
                return Ok(product);
            }
            catch (Exception e)
            {
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }
           
        }

        [HttpPut("{id}")]
        //phương thức cập nhật một sản phẩm theo id
        //tham số truyền vào [FromBody] Product Product và id
        public ActionResult UpdateProductById([FromBody] Product2 Product)
        {
            try
            {
                int rs = ProductDAO.Save(Product);
                if (rs != 0)
                    return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }
           
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
            try
            {

                int rs = ProductDAO.RemoveProductById(id);
                if (rs == 1)
                    return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }

        
            
        }


    }

}
