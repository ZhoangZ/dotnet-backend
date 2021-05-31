using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
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
    //[Route("products")]
    public class ProductControlller : ControllerBase
    {
        //auto wired productDAO
        private Product2DAO ProductDAO;
        private CommentDAO commentDAO;

        public ProductControlller(Product2DAO ProductDAO, CommentDAO commentDAO)
        {
            /*this.ProductDAO = ProductDAO;
            this.commentDAO = commentDAO;*/
            this.ProductDAO = new Product2DAO();
            this.commentDAO = new CommentDAO();

        }

        //lấy danh sách dữ liệu sản phẩm theo tiêu chí
        [HttpGet("list")]
        [HttpGet("/products")]
        public ActionResult GetAllProducts(int _limit = 10, int _page = 1, string _sort = "id:asc", int salePrice_lte = -1, int salePrice_gte = -1
            , int brand_id = 0, int rom_id = 0, int ram_id = 0, int isHot =0)
        {
            try
            {
                List<Product2> lst = ProductDAO.getList(_page, _limit, _sort, salePrice_lte, salePrice_gte, brand_id, rom_id, ram_id, isHot);
                int toltal = ProductDAO.getCount(salePrice_lte, salePrice_gte, brand_id, rom_id, ram_id, isHot);
                lst.setRequset(Request);
                PageResponse pageResponse = new PageResponse();
                pageResponse.Data = lst;
                pageResponse.Pagination = new Pagination(_limit, _page, toltal);
                return Ok(pageResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Không lấy được danh sách sản phẩm.", "None product take"));
            }

        }

        [HttpGet]
        [HttpGet("{_id}")]
        //lấy ra một sản phẩm theo id dùng cho trang chi tiết sản phẩm,...
        public ActionResult GetOneProductById(int _id, int id)
        {
            //Console.WriteLine(_id);
            try
            {
                int tmp = _id;
                if (id != 0) tmp = id;
                Product2 product = ProductDAO.getProduct(tmp);
                if (product == null) return BadRequest(tmp);
                product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });

                CommentResponse commentResponse = new CommentResponse();
                product.commentResponse = commentResponse;
                ICollection<CommentEntity> listResult = commentDAO.getAllByProductID(product.Id);
                if (listResult.Count == 0)
                {
                    commentResponse.tbcRate = 0.0;
                    commentResponse.tongCmt = 0;
                    commentResponse.listCommentByProduct = new List<CommentEntity>();
                    return Ok(product);
                }
                commentResponse.listCommentByProduct = listResult;
                commentResponse.computeSumOfList();
                commentResponse.computeTbcRate();
               // return Ok(product);
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
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
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }

        }


        [HttpDelete]
        //phương thức delete danh sách sản phẩm theo id
        //tham số truyền vào là một mảng id
        public void deleteProducts(int[] ids)
        {
            foreach (int id in ids)
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
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }



        }

        [HttpGet("brands")]

        public ActionResult GetBrand()
        {

            try
            {
                List<Brand> product = ProductDAO.GetBrands();
                if (product == null) return BadRequest(new MessageResponse("Lỗi.", "Error"));
                /*product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });*/
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Lỗi.", "Error"));
            }

        }
        [HttpGet("brands-actived")]

        public ActionResult GetBrandActived()
        {

            try
            {
                List<Brand> product = ProductDAO.GetActivedBrands();
                if (product == null) return BadRequest(new MessageResponse("Lỗi.", "Error"));
                /*product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });*/
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Lỗi.", "Error"));
            }

        }
        [HttpGet("rams")]

        public ActionResult GetRam()
        {

            try
            {
                List<RamEntity> product = ProductDAO.GetRams();
                if (product == null) return BadRequest(new MessageResponse("Lỗi.", "Error"));
                /*product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });*/
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Lỗi.", "Error"));
            }

        }

        [HttpGet("roms")]

        public ActionResult GetRom()
        {

            try
            {
                List<RomEntity> product = ProductDAO.GetRoms();
                if (product == null) return BadRequest(new MessageResponse("Lỗi.", "Error"));
                /*product.Images.ForEach(delegate (ImageProduct ip) {
                    ip.setRequest(Request);
                });*/
                return Ok(product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Lỗi.", "Error"));
            }

        }




    }

}
