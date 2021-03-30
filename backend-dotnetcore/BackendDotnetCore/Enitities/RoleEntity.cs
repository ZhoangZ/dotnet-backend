using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public partial class RoleEntity
    {
        public int id;
        public string name;
        public string description;
        public string type;
        public string created_by;
        public string update_by;
        public int active;



    }
}
