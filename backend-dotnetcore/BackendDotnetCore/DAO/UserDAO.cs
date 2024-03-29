﻿using BackendDotnetCore.EF;
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
                //check local UserRole
                var localUR = dbContext.Set<UserRole>().AsNoTracking().Where(x=>x.User.Id == userEntity.Id).ToList<UserRole>();
                //foreach (UserRole ur in localUR)
                //{
                //    dbContext.roles.Where(x => x.Id == ur.Role.Id).AsNoTracking();//12092021
                //    var localRole = dbContext.Set<RoleEntity>()
                //                     .Local
                //                     .FirstOrDefault(entry => entry.Id == ur.Role.Id);
                //    if (localRole != null)
                //    {
                //        //detach
                //        dbContext.Entry(localRole).State = EntityState.Detached;
                //        Console.WriteLine("Detached role is success");
                //    }
                //}
                // check if local is not null 
                //var local = dbContext.Set<UserEntity>()
                //                    .Local
                //                    .FirstOrDefault(entry => entry.Id == userEntity.Id);
                //if (local != null)
                //{
                //    // detach
                //}
                List<UserRole> userRoles = dbContext.Set<UserRole>().Local.Where(x => x.User.Id == userEntity.Id).ToList();
                foreach(UserRole ur in userRoles)
                {
                    Console.WriteLine("ROLE OF USER UPDATE = " + ur.Role.Id);
                    var localRole = dbContext.Set<RoleEntity>().Local.FirstOrDefault(x=>x.Id == ur.Role.Id);
                    if(localRole != null)
                    dbContext.Entry(localRole).State = EntityState.Detached;
                }
                var local = dbContext.Set<UserEntity>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(userEntity.Id));
                if (local != null)
                {
                    dbContext.Entry(local).State = EntityState.Detached;
                }
                dbContext.Entry(userEntity).State = EntityState.Modified;
                //dbContext.users.Update(userEntity);
                dbContext.SaveChanges();
              return userEntity;
            }
            
        }
        public bool updateUserRole(UserEntity userEntity)
        {
            var localUR = dbContext.Set<UserRole>()
                                   .Local
                                   .ToList<UserRole>();
            foreach (UserRole ur in localUR)
            {
                var localRole = dbContext.Set<RoleEntity>()
                                 .Local
                                 .FirstOrDefault(entry => entry.Id == ur.Role.Id && ur.User.Id == userEntity.Id);
                if (localRole != null)
                {
                    //detach
                    dbContext.Entry(localRole).State = EntityState.Detached;
                    Console.WriteLine("Detached role is success");
                }
            }
            var local = dbContext.Set<UserEntity>()
                                .Local
                                .FirstOrDefault(entry => entry.Id == userEntity.Id);
            if (local != null)
            {
                // detach
                dbContext.Entry(local).State = EntityState.Detached;
            }
            dbContext.users.Update(userEntity);
            dbContext.SaveChanges();
            return true;
        }

        public bool BlockedAndUnblockedOneUser(int id, bool blocked)
        {
            var user = dbContext.users.Where(x => x.Id == id).SingleOrDefault();
            int active = user.Active;
            user.Active = blocked ? 1 : 0;
            UserEntity userBlocked = Save(user);
            return userBlocked.Active != active ? true : false;
        }

        public RoleEntity GetRoleFirst()
        {
            var role = dbContext.roles.Where(x => x.Type == "2");

            return role.FirstOrDefault(); 

        }
        public RoleEntity GetRoleByID(int id)
        {
            var role = dbContext.roles.Where(x => x.Id == id);
            return role.FirstOrDefault();
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
