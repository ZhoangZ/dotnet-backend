using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class RomEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [Column("rom")]
        public string Rom { get; set; }
        [JsonIgnore]
        public List<Product2Specific> Product2Specifics { get; set; }
    }
}
