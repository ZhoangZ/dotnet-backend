using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Rests
{
    [ApiController]
    [Route("api/upload")]
    public class UploadREST : ControllerBase
    {

        ImageProductDAO entityDAO;
        public UploadREST()
        {

            entityDAO = new ImageProductDAO();
        }
        [HttpPost("one")]
        public async Task<IActionResult> Index2(IFormFile file)
        {
                string filePath="";


                if (file.Length > 0)
                {
                // full path to file in temp location
                filePath = FileProcess.FileProcess.getFullPath("product\\"+file.FileName); //we are using Temp file name just for the example. Add your own file path.
                     Console.WriteLine(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
           
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(filePath);
        }
        [HttpPost("many")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    string filePath=FileProcess.FileProcess.getFullPath("product\\" + formFile.FileName); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(new { count = files.Count, size, filePaths });
        }

        [HttpPost("many/{productId}")]
        public async Task<IActionResult> Index45(List<IFormFile> files, int productId)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            var images = new List<ImageProduct>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    ImageProduct entity = new ImageProduct();
                    entity.ProductId = productId;
                    // full path to file in temp location
                    string filePath = FileProcess.FileProcess.getFullPath("product\\" + formFile.FileName); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    entity.Image = formFile.FileName;

                    var a = entityDAO.AddEntity(entity);
                    images.Add(a);
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(images);
        }

        [HttpPost("one/{productId}")]
        public async Task<IActionResult> Index3(IFormFile file, int productId)
        {
            string filePath = "";

            ImageProduct entity = new ImageProduct();
            entity.ProductId = productId;
            if (file.Length > 0)
            {
                // full path to file in temp location
                filePath = FileProcess.FileProcess.getFullPath("product\\" + file.FileName); //we are using Temp file name just for the example. Add your own file path.
                Console.WriteLine(filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            entity.Image = file.FileName;

            var a =entityDAO.AddEntity(entity);
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(a);
        }
    }
}
