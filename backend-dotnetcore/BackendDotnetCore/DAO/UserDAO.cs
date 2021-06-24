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
        public UserEntity Save(UserEntity userEntity)
        {
            if (userEntity.Id == 0)
            {
                if (getOneByEmail(userEntity.Email) == null)
                {
                    Console.WriteLine("Them moi");
                    dbContext.users.Add(userEntity);
                    dbContext.SaveChanges();
                    return userEntity;
                }
                else
                {
                    Console.WriteLine("Email đăng kí đã tồn tại trong hệ thống!");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Cap nhat");
                dbContext.users.Where(x => x.Id == userEntity.Id).AsNoTracking();
                var local = dbContext.Set<UserEntity>()
                                     .Local
                                     .FirstOrDefault(entry => entry.Id.Equals(userEntity.Id));
                //check local UserRole
                var localUR = dbContext.Set<UserRole>().Local.ToList<UserRole>();
                foreach (UserRole ur in localUR)
                {
                    Console.WriteLine(ur.Id+", "+ur.Role.Id+", "+", "+ur.User.Id);
                    var localRole = dbContext.Set<RoleEntity>()
                                     .Local
                                     .FirstOrDefault(entry => entry.Id == ur.Role.Id);
                    if (localRole != null)
                    {
                        //detach
                        dbContext.Entry(localRole).State = EntityState.Detached;
                        Console.WriteLine("Detached role is success");
                    }
                }
                
                // check if local is not null 
                if (local != null)
                {
                    // detach
                    dbContext.Entry(local).State = EntityState.Detached;
                }
                dbContext.users.Update(userEntity);
                    dbContext.SaveChanges();
                    return userEntity;
            }
            
        }

        public bool BlockedOneUser(int id)
        {
            var user = dbContext.users.Where(x => x.Id == id).SingleOrDefault();
            user.Active = 0;
            UserEntity userBlocked = Save(user);
            return userBlocked.Active == 0 ? true : false;
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

        public List<UserEntity> GetListUsers()
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
            Console.WriteLine("UserDAO userID = " + userID);
            dbContext.ChangeTracker.LazyLoadingEnabled = false;
            var user = dbContext.users.Where(x => x.Id == userID)
                .Include(x=>x.UserRoles)
                .ThenInclude(x=>x.Role)
                .SingleOrDefault();
            //dbContext.ChangeTracker.LazyLoadingEnabled = true;
            if (null == user) return null;
            return user;
        }
        public UserEntity getOneByEmail(string email)
        {
            var user = dbContext.users.Where(x => x.Email.Equals(email)).SingleOrDefault();
            return user;
        }
        public bool loginByEmailVer2(string email, string password)
        {
            Console.Write("LOGIN WITH EMAIL = {0}, {1}, {2} ", email, password, EncodeUltis.MD5(password));
            var userLogin = dbContext.users.Where(x => x.Email.Equals(email) && x.Password.Equals(EncodeUltis.MD5(password))).SingleOrDefault();
            if (null != userLogin) return true;
            return false;
        }
        public UserEntity loginMD5(string email, string password)
        {
            var userLogin = dbContext.users.Where(x => x.Email.Equals(email) && x.Password.Equals(EncodeUltis.MD5(password))).SingleOrDefault();
            return userLogin;
        }

        public bool isAdmin(int userID)
        {
            foreach (UserRole ur in new UserRoleDAO().getAllRoleOfUserId(userID))
                if (ur.Role.Id == 2) return true;

            return false;
        }

    }
}
