using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class Product
    {
        public int Id { get; set; }
        [Column("SaleRate")]
        public int promotionPercents { get; set; }        
        public string Name { get; set; }
        [Column("id_brand")]
        public string Brand { get; set; }
        public int Memory { get; set; }
        public int RAM { get; set; }
        [Column("price")]
        public double OriginalPrice { get; set; }
        public string DESCRIPTION { get; set; }
        [Column("DATE_SUBMITTED")]
        public DateTime CreatedAt { get; set; }
        [Column("AMOUNT_SOLD")]
        public int AMOUNT_SOLD { get; set; }
        public string OS { get; set; }
        



        [NotMapped]
        public double GoalPrice { get { return  (100-this.promotionPercents) * this.OriginalPrice / 100; }
            }





        public virtual List<ImageProduct> Images { get; set; }



        public override String ToString()
        {
            Type objType = this.GetType();
            PropertyInfo[] propertyInfoList = objType.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            StringBuilder result = new StringBuilder();
            result.AppendFormat(objType.Name + "[");
            bool flag = false;
            foreach (PropertyInfo propertyInfo in propertyInfoList)
            {
                result.AppendFormat("{0}={1}, ", propertyInfo.Name, propertyInfo.GetValue(this));
                flag = true;
            }
            if (flag)
                result.Remove(result.Length - 2, 1);
            result.AppendFormat("]");
            return result.ToString();
        }
       
    }
}
