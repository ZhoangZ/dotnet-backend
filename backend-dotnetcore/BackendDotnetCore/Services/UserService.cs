using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Helpers;
using BackendDotnetCore.Models;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Froms;

namespace BackendDotnetCore.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);

        AuthenticateResponse loginAuthenticate(LoginForm model);
        bool checkEmail(string email);

        Account getAccountById(int accountId);

        bool save(Account account);
    }

    public class UserService : IUserService
    {
        private AccountDAO _accountDAO ;
        public UserService()
        {
            _accountDAO = new AccountDAO();
           
        }
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
       

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {

            /*  var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

              if (user == null) return null;

              // authentication successful so generate jwt token
              var token = generateJwtToken(user);
            return new AuthenticateResponse(user, token);
             */
            var dao = new AccountDAO();
            if (_accountDAO == null)
                Console.WriteLine("check");

            var account = dao.login(model.Username, model.Password);
            // return null if user not found
             if (account == null) return null;
             var token = generateJwtToken(account);
             return new AuthenticateResponse(account, token);
        }

        public AuthenticateResponse loginAuthenticate(LoginForm model)
        {
            var dao = new UserDAO();
            var account = dao.loginMD5(model.Username, model.Password);
            if (account == null) return null;
            var token = generateJwtToken(account);
            return new AuthenticateResponse(token, account);
        }

        private string generateJwtToken(UserEntity account)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




        private string generateJwtToken(Account account)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        bool IUserService.checkEmail(string email)
        {
            var dao = new AccountDAO();
            if (_accountDAO == null)
                Console.WriteLine("_accountDAO null");

            if (dao.getAccountByEmail(email) == null) return false;
            return true;
        }

        public Account getAccountById(int accountId)
        {
            var dao = new AccountDAO();
            if (_accountDAO == null)
                Console.WriteLine("_accountDAO null");
            Account account = null;
            if ((account= dao.getAccount(accountId)) == null) return null;
            return account;
        }

        public bool save(Account account)
        {
            var dao = new AccountDAO();
            if (_accountDAO == null)
                Console.WriteLine("_accountDAO null");
            dao.save(account);
            return true;
        }

        
    }
}