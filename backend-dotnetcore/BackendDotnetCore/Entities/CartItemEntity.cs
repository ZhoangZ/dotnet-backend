using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("cart_item")]   
    public class CartItemEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { set; get; }
        [Column("amount")]
        public int amount { set; get; }
        [Column("total_price")]
        public double totalPrice { set; get; }
        [Column("active")]
        public int active { set; get; }
        [Column("product_name")]
        public string productName { set; get; }
        [Column("product_price")]
        public double productPrice { set; get; }
        [Column("product_image")]
        public string productImg { set; get; }

        public virtual UserEntity user { set; get; }
        public virtual Product2 product { set; get; }

    }
}
