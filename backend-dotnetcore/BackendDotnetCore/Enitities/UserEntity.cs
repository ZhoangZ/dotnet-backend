using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public partial class UserEntity:BaseEntity
    {
        public string username;
        public string email;
        public string provider;
        public bool confirmed;
        public string blocked;
        public int active;


        //one to one (role)
        public virtual RoleEntity role { get; set; }
    }
}
