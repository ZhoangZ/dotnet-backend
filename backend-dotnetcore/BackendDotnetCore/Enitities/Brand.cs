using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
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
        [Column("active")]
        [JsonIgnore]
        public bool Active { set; get; }
        [JsonIgnore]
        public List<Product2> Product2s { get; set; }




    }
}
