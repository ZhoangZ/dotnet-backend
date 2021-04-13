using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class UserRoleDAO
    {
        private BackendDotnetDbContext dbContext;
        public UserRoleDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }
        public List<UserRole> getAllRoleOfUserId(int userId)
        {
            var userRoles = dbContext.UserRoles.FromSqlRaw("SELECT * FROM user_role WHERE users_id={0}",userId).ToList();
            foreach(UserRole r in userRoles)
            {
                r.Role = dbContext.roles.FromSqlRaw("SELECT * FROM role WHERE role_id={0}", r.Id).SingleOrDefault();
            }
            return userRoles;
        }
             
    }
}
