using BackendDotnetCore.Entities;

namespace BackendDotnetCore.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public Account ac { get; set; }

        public string Username { set; get; }
        public string Token { get; set; }

        
        public AuthenticateResponse(Account user, string token)
        {
            Id = user.Id;
            ac = user;
            Username = user.Username;
            Token = token;
        }
       
    }
}