using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public partial class RoleEntity
    {
        public int Id;
        [Column("name")]
        public string Name;

        [Column("description")]
        public string Description;

        [Column("type")]
        public string Type;

        [Column("create_by")]
        public string Created_by;

        [Column("update_by")]
        public string Update_by;

        [Column("active")]
        public int Active;



    }
}
