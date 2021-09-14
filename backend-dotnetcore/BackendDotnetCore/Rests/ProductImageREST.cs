using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Models;
using BackendDotnetCore.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rests
{
    [ApiController]
    [Route("api/image")]
    public class ProductImageREST:ControllerBase
    {

        ImageProductDAO entityDAO;
        public ProductImageREST()
        {

            entityDAO = new ImageProductDAO();
        }

        [HttpDelete("one/{productId}/{imageId}")]
        public IActionResult Index3(int productId, int imageId)
        {

            ImageProduct image = entityDAO.getEntityById(imageId);
            if (image == null) return BadRequest("Image not exist."); 
            if (image.ProductId != productId) return BadRequest("productId không chính xác");
            ImageProduct deleted =entityDAO.DeletedEntity(image);
            deleted.setRequest(Request);
            Console.WriteLine(deleted.Image);
            if (deleted!=null)
            {
                return Ok(deleted);
            }


            return BadRequest();
        }

        //lấy danh sách dữ liệu sản phẩm theo tiêu chí
     
        [HttpGet("{productId}")]
        public ActionResult GetAllImageProduct(
            int _limit = 10,
            int _page = 1,
            string _sort = "id:asc",
            int productId=-1
            )
        {
            try
            {
                if (productId == -1) return BadRequest("Product Id không thích hợp");
                List<ImageProduct> lst = entityDAO.getList(_page, _limit, _sort,productId);
                int toltal = entityDAO.getCount(productId);
                //lst.setRequset(Request);
                PageResponse<ImageProduct> pageResponse = new PageResponse<ImageProduct>();
                pageResponse.Data = lst;
                pageResponse.Pagination = new Pagination(_limit, _page, toltal);
                return Ok(pageResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Không lấy được danh sách hình ảnh.", "None image product take"));
            }

        }
        [HttpGet("list")]
        public ActionResult GetAllImageProducts(
            int _limit = 10,
            int _page = 1,
            string _sort = "id:asc"
            )
        {
            try
            {
                List<ImageProduct> lst = entityDAO.getList(_page, _limit, _sort);
                int toltal = entityDAO.getCount();
                //lst.setRequset(Request);
                PageResponse<ImageProduct> pageResponse = new PageResponse<ImageProduct>();
                pageResponse.Data = lst;
                pageResponse.Pagination = new Pagination(_limit, _page, toltal);
                return Ok(pageResponse);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Không lấy được danh sách hình ảnh.", "None image product take"));
            }

        }
    }
}
