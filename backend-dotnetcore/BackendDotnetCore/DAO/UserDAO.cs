using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO
{
    public class UserDAO
    {
        private BackendDotnetDbContext dbContext;
        public UserDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
        }

        //phuong thuc insert into table user
        public UserEntity Save(UserEntity UserEntity)
        {
            dbContext.users.AddAsync(UserEntity);
            dbContext.SaveChangesAsync();
            return UserEntity;
        }
    }
}
