using BackendDotnetCore.EF;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Ultis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.DAO

{
    
    public class AccountDAO
    {
        private BackendDotnetDbContext dbContext;
        public AccountDAO()
        {
            this.dbContext = new BackendDotnetDbContext();
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
        public Account loginMD5(string username, string password)
        {
            
            var account = dbContext.Accounts.Where(x => 
                (x.Username == username && x.Password== password)
            ).SingleOrDefault();
            
            return account;
        }
        public Account login(string username, string password)
        {
            var passMD5 = EncodeUltis.MD5(password);
            var account = dbContext.Accounts.Where(x =>
                (x.Username == username && x.Password == passMD5)
            ).SingleOrDefault();

            return account;
        }
        public int getIdByUsername(string username)
        { 
            var account = dbContext.Accounts.Where(x => x.Username == username).SingleOrDefault();
            return account.Id;
        }

        public Account getAccountByEmail(string email)
        {
            var account = dbContext.Accounts.Where(x => x.Email == email).SingleOrDefault();
            return account;

        }
        public Account save(Account account)
        {
            //su dung cho dang ki va cap nhat
            if (account.Id == 0)
            {
                dbContext.Accounts.Add(account);
            }
            else
            {
                dbContext.Accounts.Update(account);
            }
                dbContext.SaveChanges();
           
            return account;
        }
        

    }
}
