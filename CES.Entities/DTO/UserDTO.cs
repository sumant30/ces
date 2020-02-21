using System;
using System.Collections.Generic;
using System.Text;

namespace CES.Entities.DTO
{
    public class UserDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string RefreshToken { get; set; }
        public string Token { get; set; }
        public string ClientSecret { get; set; }
    }
}
