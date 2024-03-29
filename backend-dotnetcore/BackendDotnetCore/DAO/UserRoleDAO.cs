﻿using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
            
            foreach (UserRole r in userRoles)
            {

                //? LAY RA ROLE_ID CUA user_role co id
                int role_id = dbContext.UserRoles.Where(u => u.Id == r.Id).Select(u => u.Role.Id).SingleOrDefault();
                r.Role = (from role in dbContext.roles
                          where role.Id == role_id
                          select new RoleEntity
                          {
                              Id = role.Id,
                              Name = role.Name,
                              Type = role.Type
                          }).ToList()[0];
            }
            return userRoles;
        }

    }
}
