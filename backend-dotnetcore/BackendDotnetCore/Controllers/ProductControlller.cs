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

        [HttpGet("list")]
        public IActionResult getAll()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product(1, "Samsung J7 pro"));
            products.Add(new Product(2, "Huawei 3i"));
            return Ok(products);
        }

        [HttpGet]
        public IActionResult getOneById(int id1)
        {
            Product product = new Product();
            Console.WriteLine(id1);
            if (id1 == 1)
            {
                product.id = 1;
                product.image = "GetOneById";
                return Ok(product);
            }
            else
            {
                return getAll();
            }
        }
        [HttpGet("brand")]
        public IActionResult getAllBrand()
        {
            List<Brand> brands = new List<Brand>();
            brands.Add(new Brand("Samsung", "logo Samsung", 1));
            brands.Add(new Brand("Iphone", "logo Iphone", 1));
            brands.Add(new Brand("Huawei", "logo Huawei", 1));

            return Ok(brands);
        }
        [HttpPost("new")]
        public Product createProduct([FromBody] Product product)
        {
            //insert into table here
            return product;
        }
        [HttpPut("{new}/{id}")]
        public Product updateProduct([FromBody] Product product, int id)
        {
            //update product by id
            return product;
        }
        [HttpDelete]
        public string deleteProducts([FromBody] int[] ids)
        {
            string rs = "";
            //find id and unactive product action
            foreach(int id in ids)
            {
                if(id==1 || id == 2)
                {
                    //action something
                    rs = "deleted 1, 2";
                }
            }
            return rs;
        }
    }
}
