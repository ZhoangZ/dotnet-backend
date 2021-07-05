using BackendDotnetCore.Models;
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
       
        [Column("DELETED")]
        [JsonIgnore]
        public bool deleted { get; set; }
        [Column("price")]
        public decimal OriginalPrice { get; set; }

        [Column("DESCRIPTION")]
        public string Description { get; set; }

        [Column("LONG_DESCRIPTION")]
        public string longDescription { get; set; }

        [Column("DATE_SUBMITTED")]
        public DateTime CreatedAt { get; set; }
        [Column("UPDATE_AT")]
        public DateTime UpdatedAt { get; set; }
        [Column("AMOUNT_SOLD")]

        public int AmountSold { get; set; }
        [Column("AMOUNT")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Amount { get; set; }

        [Column("IS_HOT")]
        public bool IsHot { get; set; }
        public virtual ICollection<ImageProduct> Images { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<InformationProduct> Informations { get; set; }
      
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public virtual ICollection<CommentEntity> comments { get; set; } = new List<CommentEntity>();


        [Column("ram_id")]
        //[JsonIgnore]
        public int RamId { get; set; }
        public virtual RamEntity Ram { get; set; }
        [Column("brand_id")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Column("rom_id")]
        //[JsonIgnore]
        public int RomId { get; set; }
        public virtual RamEntity Rom { get; set; }
        
        [Column("SALE_PRICE")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]

        public decimal SalePrice { get; set; }
        [NotMapped]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public CommentResponse commentResponse{get;set;}

       







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