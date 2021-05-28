using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Consts;
using BackendDotnetCore.DAO;
using BackendDotnetCore.Entities;
using BackendDotnetCore.Froms;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendDotnetCore.Services;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class LoginController : ControllerBase
    {
        private UserDAO userDAO;
        private IUserService _userService;
        public LoginController( IUserService userService)
        {
            this.userDAO = new UserDAO();
            this._userService = userService;
        }
        
        [HttpPost("user")]
        public IActionResult doLogin([FromBody] LoginForm loginForm)
        {
            Console.WriteLine("Login with: " + loginForm.Username + ", pass: " + loginForm.Password);
            if (userDAO.getOneByUsername(loginForm.Username) == null) return BadRequest(new { message = "Username hoặc password không đúng!" });
            var response = _userService.loginAuthenticate(loginForm);
            if (response == null) return BadRequest(new { message = "Username hoặc password không đúng!" });
            return Ok(response);
            //if (userDAO.getOneByUsername(loginForm.Username) == null) return null;
            //var account = userDAO.loginMD5(loginForm.Username, loginForm.Password);
            //if(account == null) return BadRequest(new { message = "Username hoặc password không đúng!" });
            //if (account == null) return null;

            //if (FileProcess.FileProcess.fileIsExists(account.Avatar))
            //{
            //    byte[] b = System.IO.File.ReadAllBytes(FileProcess.FileProcess.getFullPath(account.Avatar));
            //    account.Avatar = "data:image/png;base64," + Convert.ToBase64String(b);
            //}
            //if (account != null)
            //{
            //    string jsonAcount = JsonConvert.SerializeObject(account);
            //    HttpContext.Session.SetString(SessionConsts.CURRENT_ACCOUNT, jsonAcount);
            //    if (loginForm.RemenberMe)
            //    {
            //        string json = HttpContext.Session.GetString(SessionConsts.LOGIN_HISTORY);
            //        Console.WriteLine("Remenber Me");
            //        Console.WriteLine(json);
            //        Dictionary<int, UserEntity> dic = null;
            //        if (json != null)
            //        {
            //            try
            //            {
            //                dic = JsonConvert.DeserializeObject<Dictionary<int, UserEntity>>(json);

            //            }
            //            catch (Exception e)
            //            {
            //                Console.WriteLine(e.Message);
            //            }
            //        }

            //        else
            //        {
            //            dic = new Dictionary<int, UserEntity>();
            //        }

            //        dic[account.Id] = account;
            //        string jsonHistoryAccount = JsonConvert.SerializeObject(dic);
            //        HttpContext.Session.SetString(SessionConsts.LOGIN_HISTORY, jsonHistoryAccount);
            //    }
            //    return Ok(account);
            //}

            //return Ok(account);
        }

        

        

    }
}
