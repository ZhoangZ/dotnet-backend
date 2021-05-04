using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    //[Table("product_2")]
    public class Product2
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("SaleRate")]
        public uint promotionPercents { get; set; }
        public string Name { get; set; }
       /* [Column("brand_id")]
        [JsonIgnore]
        public int BrandId { get; set; }*/
        public Brand Brand { get; set; }
       /* [Column("Memory")]
        public int Memory { get; set; }
        [Column("RAM")]
        public int Ram { get; set; }*/
        [Column("DELETED")]
        [JsonIgnore]
        public bool deleted { get; set; }
        [Column("price")]
        public long OriginalPrice { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("LONG_DESCRIPTION")]
        public string longDescription { get; set; }

        [Column("DATE_SUBMITTED")]
        public DateTime CreatedAt { get; set; }
        [Column("AMOUNT_SOLD")]

        public int AmoutSold { get; set; }
        [Column("OS")]
        public string Os { get; set; }
        [Column("IS_HOT")]
        public bool IsHot {get;set;}
        public virtual List<ImageProduct> Images { get; set; }
        public virtual List<InformationProduct> Informations { get; set; }
        public virtual List<Product2Specific> Specifics { get; set; }
        public virtual List<CommentEntity> comments { get; set; }

        /*  [NotMapped]

          public List<String> color0
          {
              get;set;
              //get { return new List<string>(new string[]{"RED","GREEN","BLUE"}); }
          }*/

        public Product2()
        {
            //this.IsHot = true;
            
        }
        [Column("SALE_PRICE")]

        public long SalePrice  { get; set; }

      






     



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