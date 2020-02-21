using CES.Core.Helpers;
using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Core
{
    public class TokenCore : ITokenCore
    {
        private ITokenRepo _repo;
        public TokenCore(ITokenRepo repo)
        {
            _repo = repo;
        }
        public async Task<UserDTO> GenerateNewTokenAsync(Guid userId)
        {
            var refreshToken = GenerateRefreshToken.GetToken();

            var user = await _repo.SaveTokenAsync(userId, refreshToken);

            return new UserDTO() { Username = user.Username, RefreshToken = refreshToken, Role = user.Role,ClientSecret=user.Id.ToString().Replace("-", "") };
        }

        public async Task Revoke(Guid userId)
        {
            await _repo.Revoke(userId);
        }
    }
}
