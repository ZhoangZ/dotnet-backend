using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("cart_item")]
    public class CartItemEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { set; get; }
        [Column("amount")]
        public int Amount { set; get; }
       

        [Column("actived")]
        public bool Actived { set; get; }

        [Column("deleted")]
        public bool Deleted { set; get; }


        [Column("cart_id")]
        [JsonIgnore]
        public long CartId { set; get; }
        [JsonIgnore]
        public virtual CartEntity Cart { set; get; }

       [Column("product_specific_id")]
       // [NotMapped]
        [JsonIgnore]
        public long ProductSpecificId { set; get; }       

       // [NotMapped]
        public virtual Product2Specific ProductSpecific { set; get; }

    }
}
