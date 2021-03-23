using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class Product
    {
        public int id { set; get; }
        public string image {set; get;}


        public Product()
        {

        }
        public Product(int id, string image)
        {
            this.id = id;
            this.image = image;
        }


    }
}
