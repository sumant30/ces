using CES.Core.Helpers;
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
        private ITokenRepo _token;

        public LoginCore(ILoginRepo repo, ITokenRepo token)
        {
            _repo = repo;
            _token = token;
        }
        public async Task<UserDTO> AuthenticateAsync(string username, string password)
        {
            var user = await _repo.AuthenticateAsync(username, password);
            if (user != null && user?.Id != Guid.Empty)
            {
                var refreshToken = GenerateRefreshToken.GetToken();

                await _token.SaveTokenAsync(user.Id, refreshToken);

                return new UserDTO() { Username = user.Username, RefreshToken = refreshToken, Role = user.Role };
            }
            return new UserDTO();
        }
    }
}
