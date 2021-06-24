using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    public class RamEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("ram")]
        public string Ram { get; set; }

        [Column("deleted")]
        public bool Deleted { get; set; }

        [Column("actived")]
        public bool Actived { get; set; }
        public RamEntity()
        {
            Deleted = false;
            Actived = true;
        }

    }
}
