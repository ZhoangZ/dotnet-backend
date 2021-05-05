
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


        

    }
}
