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
        public long Id { set; get; }
        [Column("amount")]
        public int Amount { set; get; }
       
       

        [Column("actived")]
        public bool Actived { set; get; }

        [Column("deleted")]
        public bool Deleted { set; get; }


        [Column("order_id")]
        //[JsonIgnore]
        public long OrderId { set; get; }
        [JsonIgnore]
        public virtual OrderEntity Order { set; get; }

       [Column("product_specific_id")]
       // [NotMapped]
        [JsonIgnore]
        public long ProductSpecificId { set; get; }       

       // [NotMapped]
        public virtual Product2Specific ProductSpecific { set; get; }

    }
}
