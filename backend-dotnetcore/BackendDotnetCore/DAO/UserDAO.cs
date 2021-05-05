using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BackendDotnetCore.Ultis;

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
            dbContext.users.Add(UserEntity);
            dbContext.SaveChanges();
            return UserEntity;
        }
        public int Save1(UserEntity UserEntity)
        {
            dbContext.users.AddAsync(UserEntity);
            dbContext.SaveChangesAsync();
            return UserEntity.Id;
        }
        public RoleEntity GetRoleFirst()
        {
            var role = dbContext.roles.Where(x => x.Type == "2");

            return role.FirstOrDefault(); ;

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

       
        public UserEntity loginMD5(string username, string password)
        {
            var passMD5 = EncodeUltis.MD5(password);
            var account = (from u in dbContext.users
                           where u.Username == username && u.Password == passMD5
                           select new UserEntity
                           {
                               Id = u.Id,
                               Username = u.Username,
                               Email = u.Email,
                               UserRoles = u.UserRoles,
                               Avatar = u.Avatar
                           }).ToList()[0];
                          

            return account;
        }
        public UserEntity login(string username, string password)
        {
            var passMD5 = EncodeUltis.MD5(password);
            var account = dbContext.users.Where(x =>
                (x.Username == username && x.Password == passMD5)
            ).SingleOrDefault();

            return account;
        }
        public int getIdByUsername(string username)
        {
            var account = dbContext.users.Where(x => x.Username == username).SingleOrDefault();
            return account.Id;
        }

        public UserEntity getAccountByEmail(string email)
        {
            var account = dbContext.users.Where(x => x.Email == email).SingleOrDefault();
            return account;

        }
       

    }
}
