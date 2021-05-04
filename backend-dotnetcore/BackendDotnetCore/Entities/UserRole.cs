using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BackendDotnetCore.Entities
{
    public class UserRole


    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
       

        [JsonIgnore]
        public virtual UserEntity User { set; get; }
      //  [JsonIgnore]  
      
        public virtual RoleEntity Role { set; get; }



    }
}
