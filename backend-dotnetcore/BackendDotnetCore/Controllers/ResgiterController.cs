using BackendDotnetCore.Enitities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BackendDotnetCore.Controllers
{
    
    [Route("/auth/local/")]
    public class ResgiterController : Controller
    {
        [HttpPost("register")]
        public IActionResult Index(string email, string username, string password, string fullName)
        {
            UserEntity user = new UserEntity();


            return View();
        }
    }
}
