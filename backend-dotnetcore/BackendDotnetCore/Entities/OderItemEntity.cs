using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("order_detail")]
    public class OrderItemEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public long Id { set; get; }
        [Column("amount")]
        public int Quantity { set; get; }


        [JsonIgnore]
        [Column("actived")]
        public bool Actived { set; get; }

        [Column("deleted")]
        [JsonIgnore]
        public bool Deleted { set; get; }


        [Column("order_id")]
        //[JsonIgnore]
        public long OrderId { set; get; }
        [JsonIgnore]
        public virtual OrderEntity Order { set; get; }

        [Column("product_id")]
        // [NotMapped]
        [JsonIgnore]
        public int ProductId { set; get; }
        public virtual Product2 Product { set; get; }



    }
}
