using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    public class RomEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
       
        [Column("rom")]
        public string Rom { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }

        [Column("actived")]
        public bool Actived { get; set; }
        public RomEntity()
        {
            Deleted = false;
            Actived = true;
        }

    }
}
