
using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class RoleDAO
    {
        private BackendDotnetDbContext dbContext;
        public RoleDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }

        //phuong thuc insert into table role
        public RoleEntity Save(RoleEntity roleEntity)
        {
            dbContext.roles.AddAsync(roleEntity);
            dbContext.SaveChangesAsync();
            return roleEntity;
        }

        //lay ra cac role hien co trong he thong
        public List<RoleEntity> getAllRole()
        {
            var roles = from r in dbContext.roles
                        select new RoleEntity
                        {
                            Id = r.Id,
                            Name = r.Name,
                            Type = r.Type
                        };
            return roles.ToList();
        }

        //lay ra ds cac role cua 1 user nao do
        public List<RoleEntity> getAllRoleByUser(int userID)
        {
            var roles = from r in dbContext.UserRoles
                        where r.User.Id == userID
                        select new RoleEntity
                        {
                            Id = r.Role.Id,
                            Name = r.Role.Name,
                            Type = r.Role.Type
                        };
            return roles.ToList();
        }


        

    }
}
