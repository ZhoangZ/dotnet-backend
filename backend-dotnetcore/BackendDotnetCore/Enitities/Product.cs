using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;

using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    [Table("product")]
    public class Product
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("SaleRate")]
        public int promotionPercents { get; set; }
        public string Name { get; set; }
        [Column("id_brand")]
        public string Brand { get; set; }
        [Column("Memory")]
        public int Memory { get; set; }
        [Column("RAM")]
        public int Ram { get; set; }
        [Column("price")]
        public int OriginalPrice { get; set; }
        [Column("DESCRIPTION")]
        public string Description { get; set; }
        [Column("DATE_SUBMITTED")]
        public DateTime CreatedAt { get; set; }
        [Column("AMOUNT_SOLD")]

        public int AmoutSold { get; set; }
        [Column("OS")]
        public string Os { get; set; }
        




        [NotMapped]

        public int salePrice
        { get { return  (100-this.promotionPercents) * this.OriginalPrice / 100; }
            }

        public double GoalPrice
        {
            get { return (100 - this.promotionPercents) * this.OriginalPrice / 100; }
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