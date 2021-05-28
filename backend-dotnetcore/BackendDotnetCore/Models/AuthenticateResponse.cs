using BackendDotnetCore.Entities;
using System.Text.Json.Serialization;

namespace BackendDotnetCore.Models
{
    public class AuthenticateResponse
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonIgnore]
        public string jwt { get; set; }
        public UserEntity user { get; set; }
        [JsonIgnore]
        public string Username { set; get; }
        [JsonIgnore]
        public string Token { get; set; }
       

        public AuthenticateResponse(string token, UserEntity user)
        {
            this.jwt = token;
            this.user = user;
        }

    }
}