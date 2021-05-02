using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        //phuong thuc insert into table user
        public UserEntity Save2(UserEntity UserEntity)
        {
            dbContext.users.Add(UserEntity);
            dbContext.SaveChanges();
            return UserEntity;
        }
        public RoleEntity GetRoleFirst()
        {
            var role=dbContext.roles.First();
            
            return role;
        }
        public UserEntity GetUserFirst()
        {
            var role = dbContext.users.Include(a=>a.UserRoles).ThenInclude(c=>c.Role).First();

            return role;
        }
        public UserRole GetUserRolesFirst()
        {
            var role = dbContext.UserRoles.Include("User").Include("Role").First();

            return role;
        }

        public List<UserEntity> getAll()
        {
            var users = dbContext.users.FromSqlRaw("SELECT * FROM users").ToList();
            UserRoleDAO userRoleDAO = new UserRoleDAO();
            foreach(UserEntity e in users)
            {
                e.UserRoles = userRoleDAO.getAllRoleOfUserId(e.Id);
            }

            return users;
        }

        public UserEntity getOneById(int userID)
        {
            var user = dbContext.users.Where(x => x.Id == userID).FirstOrDefault();
            if (null == user) return null;
            return user;
        }
    }
}
