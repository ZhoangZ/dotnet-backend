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
using BackendDotnetCore.Froms;

namespace BackendDotnetCore.Services
{
    public interface IUserService
    {
        AuthenticateResponse loginAuthenticateByEmail(LoginForm model);
        AuthenticateResponse createUserJWT(UserEntity model);
        bool checkEmail(string email);

        UserEntity getUserById(int userID);
        bool save(UserEntity ue);
        UserEntity getUserByEmail(string email);
    }

    public class UserService : IUserService
    {
        public UserDAO userDAO = new UserDAO();
        public UserService()
        {
        }
       
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
       

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
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

        bool IUserService.checkEmail(string email)
        {
            if (null == userDAO.getOneByEmail(email)) return false;
            return true;
        }

        public UserEntity getUserById(int userId)
        {
            var dao = new UserDAO();           
            UserEntity account =  dao.getOneById(userId);

            if (account!=null  && account.Blocked == 1) return null;
            return account;
        }

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
                userResponse.UserRoles = new UserRoleDAO().getAllRoleOfUserId(userResponse.Id);
                
            }
            //xac dinh blocked
            var token = generateJwtToken(userResponse);
            return new AuthenticateResponse(token, userResponse);
        }

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
                model.UserRoles = new UserRoleDAO().getAllRoleOfUserId(model.Id);
                var token = generateJwtToken(model);
                return new AuthenticateResponse(token, model);
            }
        }

        public bool save(UserEntity ue)
        {
            if(null != userDAO.Save(ue)) return true;
            return false;
        }

        public UserEntity getUserByEmail(string email)
        {
            return userDAO.getOneByEmail(email);
        }

    }
}