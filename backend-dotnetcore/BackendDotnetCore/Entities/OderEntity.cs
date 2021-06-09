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
    [Table("order")]   
    public class OrderEntity
    {
        [Column("id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[JsonIgnore]
        public long Id { set; get; }

        [Column("address_delivery")]
        public string AddressDelivery { set; get; }

        [Column("total_price")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Decimal TotalPrice { set; get; }

        [Column("total_item")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int TotalItem { set; get; }

        [Column("user_id")]
        [JsonIgnore]
        public int UserId { set; get; }
        //[JsonIgnore]
        public virtual UserEntity User { set; get; }

        public virtual List<OrderItemEntity> Items { set; get; }



    }
}
