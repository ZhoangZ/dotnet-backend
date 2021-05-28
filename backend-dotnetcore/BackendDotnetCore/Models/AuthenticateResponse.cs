using BackendDotnetCore.Entities;
using System.Text.Json.Serialization;

namespace BackendDotnetCore.Models
{
    public class AuthenticateResponse
    {
        public string jwt { get; set; }
        public UserEntity user { get; set; }
        public AuthenticateResponse(string token, UserEntity user)
        {
            this.jwt = token;
            this.user = user;
        }

    }
}