using BackendDotnetCore.EF;
using BackendDotnetCore.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO

{
    
    public class AccountDAO
    {
        private BackendDotnetCoreDbContext dbContext;
        public AccountDAO()
        {
            this.dbContext = new BackendDotnetCoreDbContext();
        }
        public Account getAccount(int Id)
        {
            var tmp = from accounts in dbContext.Accounts
                      where accounts.Id == Id
                      select new Account
                      {
                          Id = accounts.Id,
                          Username = accounts.Username,
                          Password = accounts.Password,
                          Email = accounts.Email,
                          Active = accounts.Active,
                          Delete = accounts.Delete,
                          Level = accounts.Level,
                          Avatar=accounts.Avatar,

                      };
            return tmp.ToList()[0];
        }
        public Account login(string username, string password)
        {
            var account = dbContext.Accounts.Where(x => 
                (x.Username == username && x.Password== password)
            ).SingleOrDefault();
            
            return account;
        }
        public int getIdByUsername(string username)
        { 
            var account = dbContext.Accounts.Where(x => x.Username == username).SingleOrDefault();
            return account.Id;
        }

    }
}
