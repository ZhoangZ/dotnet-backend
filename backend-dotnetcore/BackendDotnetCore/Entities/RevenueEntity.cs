using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    [Table("revenue")]
    public class RevenueEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("month")]
        public int Month { get; set; }

        [Column("year")]
        public int Year { get; set; }


        [Column("deleted")]
        //[JsonIgnore]
        public bool Deleted { set; get; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("money")]        
        public decimal Money { set; get; }


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("quantity")]
        public decimal Quantity { set; get; }

        [Column("UPDATE_AT")]
        public DateTime UpdatedAt { get; set; }

        [Column("create_at")]
        public DateTime CreatedAt { get; set; }






    }
}
