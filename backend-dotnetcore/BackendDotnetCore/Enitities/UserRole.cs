using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Enitities
{
    public class UserRole
    {
        public int userID { set; get; }
        public UserEntity user { set; get; }
        public int roleID { set; get; }
        public RoleEntity role { set; get; }


        public UserRole(int userID, int roleID)
        {
            this.userID = userID;
            this.roleID = roleID;
        }
    }
}
