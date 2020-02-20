using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Core
{
    public class LogoutCore : ILogoutCore
    {
        private ILogoutRepo _repo;
        public LogoutCore(ILogoutRepo repo)
        {
            _repo = repo;
        }
        public async Task<UserDTO> Logout(string username,string refreshToken)
        {
            var userId = await _repo.RefreshTokenExists(username,refreshToken);

            if(userId != Guid.Empty) 
            {
                var user = await _repo.Logout(userId);

                return new UserDTO() { Username = user.Username, Role = user.Role, Token = string.Empty, RefreshToken = string.Empty };
            }
            return new UserDTO();
        }
    }
}
