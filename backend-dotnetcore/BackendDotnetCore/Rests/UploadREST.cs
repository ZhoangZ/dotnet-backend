using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
       /* [HttpPost("one")]
        public async Task<IActionResult> Index2(IFormFile file)
        {
                string filePath="";


                if (file.Length > 0)
                {

                string timeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
                Regex regex = new Regex("\\.(?<ext>.+)$");
                Match match = regex.Match(file.FileName);
                if (match.Success)
                {
                    timeNow += "." + match.Groups["ext"];
                }
                Console.WriteLine(timeNow);
                // full path to file in temp location
                filePath = FileProcess.FileProcess.getFullPath("product\\"+ timeNow); //we are using Temp file name just for the example. Add your own file path.
                     Console.WriteLine(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
           
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(filePath);
        }*/
        /*[HttpPost("many")]
        public async Task<IActionResult> Index(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {

                    string timeNow = DateTime.Now.ToString("yyyyMMddHHmmss");
                    Regex regex = new Regex("\\.(?<ext>.+)$");
                    Match match = regex.Match(formFile.FileName);
                    if (match.Success)
                    {
                        timeNow += "." + match.Groups["ext"];
                    }
                    Console.WriteLine(timeNow);
                    // full path to file in temp location
                    string filePath=FileProcess.FileProcess.getFullPath("product\\" + timeNow); //we are using Temp file name just for the example. Add your own file path.
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
        }*/

        [HttpPost("many/{productId}")]
        [Authorize]
        public async Task<IActionResult> Index45(List<IFormFile> files, int productId)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (files == null) return BadRequest("Phải có files.");
            if (user == null) return BadRequest("Chưa đăng nhập.");
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            var images = new List<ImageProduct>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    ImageProduct entity = new ImageProduct();
                    entity.ProductId = productId;
                    string timeNow = DateTime.Now.ToString("yyyyMMddHHmmss") + productId + user.Id;
                    Regex regex = new Regex("\\.(?<ext>.+)$");
                    Match match = regex.Match(formFile.FileName);
                    if (match.Success)
                    {
                        timeNow += "." + match.Groups["ext"];
                    }
                    Console.WriteLine(timeNow);

                    // full path to file in temp location


                    string filePath = FileProcess.FileProcess.getFullPath("product\\" + timeNow); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    entity._image = timeNow;

                    var a = entityDAO.AddEntity(entity);
                    a.setRequest(Request);
                    images.Add(a);
                }
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(images);
        }

        [HttpPost("one/{productId}")]
        [Authorize]
        public async Task<IActionResult> Index3(IFormFile file, int productId)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (file == null) return BadRequest("Phải có file.");
            if (user == null) return BadRequest("Chưa đăng nhập.");
            string filePath = "";

            ImageProduct entity = new ImageProduct();
            entity.ProductId = productId;
            string timeNow = DateTime.Now.ToString("yyyyMMddHHmmss")+productId+ user.Id;
            Regex regex = new Regex("\\.(?<ext>.+)$");
            Match match = regex.Match(file.FileName);
            if (match.Success)
            {
                timeNow += "."+match.Groups["ext"];
            }
            Console.WriteLine(timeNow);
            if (file.Length > 0)
            {
                // full path to file in temp location
                filePath = FileProcess.FileProcess.getFullPath("product\\" + timeNow); //we are using Temp file name just for the example. Add your own file path.
                Console.WriteLine(filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            entity._image = timeNow;

            var a =entityDAO.AddEntity(entity);

            a.setRequest(Request);
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return Ok(a);
        }
    }
}
