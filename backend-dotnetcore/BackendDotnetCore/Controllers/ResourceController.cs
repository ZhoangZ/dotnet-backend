using BackendDotnetCore.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("resource")]
    public class ResourceController:ControllerBase

    {
      
        [HttpGet("product/{filename}")]

        public async Task<IActionResult> imageProduct(string filename)

        {
           
            if (FileProcess.FileProcess.fileIsExists("product\\" + filename))
            {
                string f = FileProcess.FileProcess.getFullPath("product\\" + filename);
                var imageFileStream = System.IO.File.OpenRead(f);
                return File(imageFileStream, "image/jpeg");
            }
            return BadRequest(new MessageResponse("Hình có thể đã bị xóa.", "Not found image"));
        }
        [HttpGet("brand/{filename}")]

        public async Task<IActionResult> iconBrand(string filename)

        {

            if (FileProcess.FileProcess.fileIsExists("brand\\" + filename))
            {
                string f = FileProcess.FileProcess.getFullPath("brand\\" + filename);
                var imageFileStream = System.IO.File.OpenRead(f);
                return File(imageFileStream, "image/jpeg");
            }
            return BadRequest(new MessageResponse("Hình có thể đã bị xóa.", "Not found image"));
        }
    }
}
