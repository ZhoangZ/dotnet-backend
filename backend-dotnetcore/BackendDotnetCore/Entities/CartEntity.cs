using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("cart")]   
    public class CartEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[JsonIgnore]
        public long Id { set; get; }

        [Column("total_price")]
       // [StoreGeneratedPattern.Identity]
       //[StoreGeneratedPattern = Computed]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Decimal TotalPrice { set; get; }       

        [Column("user_id")]
        [JsonIgnore]
        public int UserId { set; get; }
        //[JsonIgnore]
        public virtual UserEntity User { set; get; }

        public virtual List<CartItemEntity> Items { set; get; }



    }
}
