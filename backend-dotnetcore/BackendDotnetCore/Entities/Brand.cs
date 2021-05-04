using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("brand")]
    public class Brand
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("logo")]
        [JsonIgnore]
        public string Logo { set; get; }

        [Column("actived")]
        [JsonIgnore]
        public bool Actived { set; get; }

        [Column("deleted")]
        [JsonIgnore]
        public bool Deleted { set; get; }

        [Column("amount")]
        [JsonIgnore]
        public bool Amount { set; get; }

        [JsonIgnore]
        public List<Product2> Product2s { get; set; }




    }
}
