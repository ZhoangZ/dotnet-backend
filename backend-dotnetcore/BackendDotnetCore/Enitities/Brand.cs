using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class Brand
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logo { set; get; }
        public int active { set; get; }


        public Brand(string name, string logo, int active)
        {
            this.name = name;
            this.logo = logo;
            this.active = active;
        }

        public string toString()
        {
            return "Brand:" + " id=" + id + ", ";
        }
    }
}
