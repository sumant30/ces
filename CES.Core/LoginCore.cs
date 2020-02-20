using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Core
{
    public class LoginCore : ILoginCore
    {
        private ILoginRepo _repo;
        public LoginCore(ILoginRepo repo)
        {
            _repo = repo;
        }
        public async Task<UserDTO> AuthenticateAsync(string username, string password)
        {
            var user = await _repo.AuthenticateAsync(username, password);
            if (user != null && user?.Id != Guid.Empty)
            {
                var refreshToken = Guid.NewGuid().ToString().Replace("-", "");

                await _repo.SaveTokenAsync(user.Id, refreshToken); 
                
                return new UserDTO() { Username = user.Username, RefreshToken = refreshToken, Role = user.Role };
            }
            return new UserDTO();
        }
    }
}
