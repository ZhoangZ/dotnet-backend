using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendDotnetCore.Forms
{
    public class CartItemDTO
    {
        public int id { set; get; }
        public int productID { set; get; }
        public int userID { set; get; }
        public string productName { set; get; }
        public string productImg { set; get; }
        public double productPrice { set; get; }
        public int amount { set; get; }
        public double totalPrice { set; get; }
        public int active { set; get; }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(productID + ", " + userID);
            return sb.ToString();
        }
    }
}
