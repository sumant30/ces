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
        private IUserRepo _user;

        public LogoutCore(ILogoutRepo repo, IUserRepo user)
        {
            _repo = repo;
            _user = user;
        }
        public async Task<UserDTO> LogoutAsync(string username,string refreshToken)
        {
            var userId = await _user.GetAsync(username,refreshToken);

            if(userId != Guid.Empty) 
            {
                var user = await _repo.LogoutAsync(userId);

                return new UserDTO() { Username = user.Username, Role = user.Role, Token = string.Empty, RefreshToken = string.Empty };
            }
            return new UserDTO();
        }
    }
}
