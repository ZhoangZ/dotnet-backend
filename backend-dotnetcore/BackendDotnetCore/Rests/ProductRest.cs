using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Models;
using BackendDotnetCore.Response;
using BackendDotnetCore.Ultis;
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
    [Route("product")]
    //[Route("products")]
    public class ProductRest : ControllerBase
    {
        //auto wired productDAO
        private Product2DAO ProductDAO;
        private CommentDAO commentDAO;
        ImageProductDAO entityDAO;

        public ProductRest()
        {
           
            this.ProductDAO = new Product2DAO();
            this.commentDAO = new CommentDAO();
            this.entityDAO = new ImageProductDAO();

        }

        //lấy danh sách dữ liệu sản phẩm theo tiêu chí
        [HttpGet("list")]
        [HttpGet("/products")]
        public ActionResult GetAllProducts(int _limit = 10, int _page = 1, string _sort = "id:asc", int salePrice_lte = -1, int salePrice_gte = -1
            , 
            int brand_id = 0,
            int rom_id = 0,
            int ram_id = 0, 
            string title_like = null, 
            int deleted=0,
            int isHot =0)
        {
            try
            {
                List<Product2> lst = ProductDAO.getList(_page, _limit, _sort, salePrice_lte, salePrice_gte, brand_id, rom_id, ram_id, isHot, title_like, deleted);
                int toltal = ProductDAO.getCount(salePrice_lte, salePrice_gte, brand_id, rom_id, ram_id, isHot,title_like, deleted);
                lst.setRequset(Request);
                PageResponse<Product2> pageResponse = new PageResponse<Product2>();
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
                foreach ( ImageProduct ip in product.Images) { 
                    ip.setRequest(Request);
                };

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
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không có quyền admin.");

            try
            {
                Product.CreatedAt = DateTime.Now;
                Product.UpdatedAt = DateTime.Now;
                Product2 product = ProductDAO.AddProduct(Product);
                if (product == null) return BadRequest();

                var a = ProductDAO.getProduct(product.Id);
                foreach (ImageProduct ip in a.Images)
                {
                    ip.setRequest(Request);
                };
                return Ok(a);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }

        }


        [HttpPost("v2")]
        
        public async Task<IActionResult> CreateNewProduct(
           [FromForm] uint promotionPercents,
           [FromForm] string name,
           [FromForm] int brandId,
           [FromForm] decimal originalPrice,
           [FromForm] string description,
          [FromForm] string longDescription,
          [FromForm] int amount,
         [FromForm] bool isHot,
         [FromForm] int ramId,
         [FromForm] int romId,
          [FromForm] List<IFormFile> files)
        {
            Product2 Product = new Product2();
            Product.promotionPercents = promotionPercents;
            Product.Name = name;
            Product.BrandId = brandId;
            Product.OriginalPrice = originalPrice;
            Product.Description = description;
            Product.longDescription = longDescription;
            Product.Amount = amount;
            Product.IsHot= isHot;
            Product.RamId = ramId;
            Product.RomId = romId;
            Console.WriteLine("rom id" + romId);
            Console.WriteLine("ramid" + ramId);
            Console.WriteLine("name" + name);
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không có quyền admin.");


            try
            {
                Product.CreatedAt = DateTime.Now;
                Product.UpdatedAt = DateTime.Now;
                Product2 product = ProductDAO.AddProduct(Product);
                if (product == null) return BadRequest();





                var filePaths = new List<string>();
                var images = new List<ImageProduct>();
                int i = 0;
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        ImageProduct entity = new ImageProduct();
                        entity.ProductId = product.Id;
                        Console.WriteLine("ProductId" + product.Id);
                        string timeNow = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + entity.ProductId+"_" + user.Id+"_" +i;
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

                        var a2 = entityDAO.AddEntity(entity);
                        a2.setRequest(Request);
                        images.Add(a2);
                    }
                }

                var a = ProductDAO.getProduct(product.Id);
                foreach (ImageProduct ip in a.Images)
                {
                    ip.setRequest(Request);
                };
                return Ok(a);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }

        }
        [HttpPost("v3")]

        public async Task<IActionResult> CreateNewProduct2(
          [FromForm] Product2 Product,          
         [FromForm] List<IFormFile> files)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không có quyền admin.");


            try
            {
                Product.CreatedAt = DateTime.Now;
                Product.UpdatedAt = DateTime.Now;
                Product2 product = ProductDAO.AddProduct(Product);
                if (product == null) return BadRequest();
                var filePaths = new List<string>();
                var images = new List<ImageProduct>();
                Console.WriteLine("ProductId" + product.Id);
                if(files!=null)
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        ImageProduct entity = new ImageProduct();
                        entity.ProductId = product.Id;
                        string timeNow = DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + entity.ProductId + "_" + user.Id;
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

                        var a2 = entityDAO.AddEntity(entity);
                        a2.setRequest(Request);
                        images.Add(a2);
                    }
                }

                var a = ProductDAO.getProduct(product.Id);
                foreach (ImageProduct ip in a.Images)
                {
                    ip.setRequest(Request);
                };
                return Ok(a);
            }
            catch (Exception e)
            {
                if (e.InnerException != null) Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }

        }

        [HttpPut("{id}")]
        //phương thức cập nhật một sản phẩm theo id
        //tham số truyền vào [FromBody] Product Product và id
        public ActionResult UpdateProductById([FromBody] Product2 Product, int id)
        {
            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không có quyền admin.");

            try
            {
                Product.Id = id;
                Product.UpdatedAt = DateTime.Now;
                //Console.WriteLine(Product.BrandId);
                Product = ProductDAO.Save(Product);               
                var a=ProductDAO.getProduct(id);
                return Ok(a);

               
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
        public ActionResult deleteProducts(int[] ids)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không có quyền admin.");

            IList<Product2> rs = new List<Product2>();
            foreach (int id in ids)
            {

                var a=ProductDAO.RemoveProductById(id);
                if (a != null) rs.Add(a);
                Console.WriteLine("Remove productID={0}", id);
            }
            return Ok(rs);
        }

        [HttpDelete("{id}")]
        //phương thức delete danh sách sản phẩm theo id
        //tham số truyền vào là một mảng id
        public ActionResult deleteProduct(int id)
        {

            // Lấy UserEntity đang đăng nhập từ jwt
            UserEntity user = (UserEntity)HttpContext.Items["User"];
            //Console.WriteLine(user);
            // Xóa bộ nhớ đệm chứa userentity
            HttpContext.Items["User"] = null;


            if (user == null) return BadRequest("Chưa đăng nhập.");
            if (!user.IsAdmin) return BadRequest("Không có quyền admin.");

            try
            {

                Product2 rs = ProductDAO.RemoveProductById(id);
                if (rs!=null)
                    return Ok(rs);
                return BadRequest(new MessageResponse("Không tìm thấy sản phẩm.", "Request failture"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return BadRequest(new MessageResponse("Thao tác không thành công.", "Request failture"));
            }



        }

        [HttpGet("brands")]

        public ActionResult GetBrand(int deleted = 0)
        {

            try
            {
                List<Brand> product = ProductDAO.GetBrands(deleted);
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

        public ActionResult GetRam(int deleted = 0)
        {

            try
            {
                List<RamEntity> product = ProductDAO.GetRams(deleted);
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

        public ActionResult GetRom(int deleted = 0)
        {

            try
            {
                List<RomEntity> product = ProductDAO.GetRoms(deleted);
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
