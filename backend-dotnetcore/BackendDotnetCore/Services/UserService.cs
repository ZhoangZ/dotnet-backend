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
        AuthenticateResponse loginAuthenticateByEmail(LoginForm model);
        AuthenticateResponse createUserJWT(UserEntity model);
        bool checkEmail(string email);

        Account getAccountById(int accountId);
        UserEntity getUserById(int userID);
        bool save(UserEntity ue);
        bool save(Account account);
    }

    public class UserService : IUserService
    {
        private AccountDAO _accountDAO ;
        public UserDAO userDAO = new UserDAO();
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
            //var dao = new AccountDAO();
            //if (_accountDAO == null)
            //    Console.WriteLine("_accountDAO null");

            //if (dao.getAccountByEmail(email) == null) return false;
            if (null == userDAO.getOneByEmail(email)) return false;
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public AuthenticateResponse loginAuthenticateByEmail(LoginForm model)
        {
            var dao = new UserDAO();
            UserEntity userResponse = null;
            bool successed = dao.loginByEmailVer2(model.Email, model.Password);
            if (successed == false)
            {
                Console.WriteLine("Login fail");
                return null;
            }
            else
            {
                userResponse = dao.getOneByEmail(model.Email);
            }
            var token = generateJwtToken(userResponse);
            return new AuthenticateResponse(token, userResponse);
        }

        ///
        ///
        //
        public AuthenticateResponse createUserJWT(UserEntity model)
        {
            var dao = new UserDAO();
            if (model.Id == 0)
            {
                Console.WriteLine("Resgiter fail");
                return null;
            }
            else
            {
                var token = generateJwtToken(model);
                return new AuthenticateResponse(token, model);
            }
        }

        public UserEntity getUserById(int userID)
        {
            Console.WriteLine("userID  =" + userID);
           return userDAO.getOneById(userID);
        }

        public bool save(UserEntity ue)
        {
            if(null != userDAO.Save(ue)) return true;
            return false;
        }
    }
}