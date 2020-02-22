using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CES.Core
{
    public class UserCore : IUserCore
    {
        private IUserRepo _repo;
        private IApplicationRequestRepo _req;

        public UserCore(IUserRepo repo, IApplicationRequestRepo req)
        {
            _repo = repo;
            _req = req;
        }
        public async Task<UserWithAppReqDTO> GetUserSessionDetails(string username)
        {
            var userReq = new UserWithAppReqDTO();

            var user = await _repo.GetUserDetails(username);
            var req = await _req.GetApplicationRequests(username);

            userReq.Username = user.Username;
            userReq.RefreshToken = user.RefreshToken;
            userReq.Role = user.Role;
            userReq.ClientSecret = user.Id.ToString().Replace("-", "");

            userReq.AppReq = new List<AppReqDTO>();
            req?.ToList().ForEach(p =>
            {
                userReq.AppReq.Add(new AppReqDTO()
                {
                    AppName = p.AppName,
                    AccessType = p.AccessType
                });
            });

            return userReq;
        }
        public async Task ChangePassword(string username, string password)
        {
            await _repo.ChangePassword(username, password);
        }

        public async Task<string> ForgotPassword(string username)
        {
            return await _repo.ForgotPassword(username);
        }
        public async Task<Guid> GetAsync(string username, string refreshToken)
        {
            return await _repo.GetAsync(username, refreshToken);
        }
        public async Task<bool> ValidateClientSecret(string username, string clientSecret)
        {
            var userId = await _repo.GetAsync(username);
            if (userId != Guid.Empty)
            {
                if (userId.ToString().Replace("-", "").Equals(clientSecret))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
