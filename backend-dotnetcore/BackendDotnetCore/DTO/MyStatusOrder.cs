using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DTO
{
    public class MyStatusOrder
    {
        public int id { set; get; }
        public string statusString { set; get; }

        public MyStatusOrder()
        {

        }

        public MyStatusOrder(int id, string statusString)
        {
            this.id = id;
            this.statusString = statusString;
        }
    }
}
