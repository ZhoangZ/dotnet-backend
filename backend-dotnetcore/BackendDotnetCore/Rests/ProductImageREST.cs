using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
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
    }
}
