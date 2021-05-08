using BackendDotnetCore.Entities;
using System.Text.Json.Serialization;

namespace BackendDotnetCore.Models
{
    public class AuthenticateResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public Account ac { get; set; }
        public string jwt { get; set; }
        public UserEntity user { get; set; }
        [JsonIgnore]
        public string Username { set; get; }
        [JsonIgnore]
        public string Token { get; set; }
       

        public AuthenticateResponse(Account user, string token)
        {
            Id = user.Id;
            ac = user;
            Username = user.Username;
            Token = token;
        }
        public AuthenticateResponse(string token, UserEntity user)
        {
            this.jwt = token;
            this.user = user;
        }

    }
}