using Microsoft.AspNetCore.Mvc;
using BackendDotnetCore.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendDotnetCore.Controllers
{
    [ApiController]
    [Route("api/logout")]
    public class LogoutController:ControllerBase
    {
        [HttpGet]
        public String logout()
        {
            HttpContext.Session.Remove(SessionConsts.CURRENT_ACCOUNT);
            return "OK";
        }

    }
}
