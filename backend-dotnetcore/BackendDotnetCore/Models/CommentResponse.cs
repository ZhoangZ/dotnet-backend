using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Models
{
    public class CommentResponse
    {
        public double tbcRate { set; get; }
        public int tongCmt { set; get; }
        public ICollection<CommentEntity> listCommentByProduct { set; get; } = new List<CommentEntity>();

        public void computeTbcRate()
        {
             
           foreach(CommentEntity ce in listCommentByProduct)
            {
                tbcRate += ce.rate;
            }
            tbcRate = Math.Round(tbcRate/computeSumOfList(), 1);
        }

        public int computeSumOfList()
        {
            tongCmt = listCommentByProduct.Count;
            return tongCmt;
        }
    }
}
