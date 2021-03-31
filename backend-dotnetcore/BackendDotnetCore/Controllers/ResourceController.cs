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

        public async Task<IActionResult> test(string filename)

        {
           
            if (FileProcess.FileProcess.fileIsExists("product\\" + filename))
            {
                string f = FileProcess.FileProcess.getFullPath("product\\" + filename);
                var imageFileStream = System.IO.File.OpenRead(f);
                return File(imageFileStream, "image/jpeg");
            }
            return Ok();
        }
    }
}
