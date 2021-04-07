using BackendDotnetCore.Enitities;
using BackendDotnetCore.Entities;

namespace BackendDotnetCore.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
      
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(Account user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Token = token;
        }
    }
}