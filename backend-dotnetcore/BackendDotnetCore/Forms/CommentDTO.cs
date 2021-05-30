using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    public class CommentDTO
    {
        public int id { set; get; }
        public string content { set; get; }
        public int active {set;get; }
        public int productID { set; get; }
        public int userID { set; get; }
        public double rate { set; get; }
        public int sumOfProduct { set; get; }


        public override string ToString()
        {
            return "CommentDTO = "+id+", content=" + content + ", productID =" + productID + ", userID=" + userID+", sum="+sumOfProduct;
        }

    }
}
