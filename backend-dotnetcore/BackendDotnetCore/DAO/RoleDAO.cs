
using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
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
            dbContext.RoleEntities.Add(roleEntity);
            return roleEntity;
        }

    }
}
