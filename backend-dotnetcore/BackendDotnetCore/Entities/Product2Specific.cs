using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    public class Product2Specific
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("color")]
        public string Color { get; set; }
        [Column("amount")]
        public uint Amount { get; set; }

        [Column("price")]
        public Decimal Price { get; set; }

        [Column("sale_price")]
        public Decimal SalePrice { get; set; }

        public RamEntity Ram { get; set; }
        public RomEntity Rom { get; set; }
        [JsonIgnore]
        public Product2 Product { get; set; }
    }
}
