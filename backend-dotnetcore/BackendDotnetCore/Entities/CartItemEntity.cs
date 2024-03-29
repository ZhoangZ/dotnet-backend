﻿using System;
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
        [JsonIgnore]

        public long Id { set; get; }
        [Column("amount")]
        public int Quantity { set; get; }
       

        [Column("actived")]
        [JsonIgnore]
        public bool Actived { set; get; }
        [JsonIgnore]

        [Column("deleted")]
        public bool Deleted { set; get; }


        [Column("cart_id")]
        [JsonIgnore]
        public long CartId { set; get; }
        [JsonIgnore]
        public virtual CartEntity Cart { set; get; }

       [Column("product_id")]
       // [NotMapped]
        [JsonIgnore]
        public int ProductId { set; get; }
        public virtual Product2 Product { set; get; }

        [NotMapped]
        public int Idp { get { return ProductId; } set { ProductId = value; } }




    }
}
