using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class RamEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ram")]
        public string Ram { get; set; }
        [JsonIgnore]
        public List<Product2Specific> Product2Specifics { get; set; }
    }
}
