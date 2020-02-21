using CES.Entities.Interfaces;
using System;
using System.Threading.Tasks;

namespace CES.Core
{
    public class UserCore : IUserCore
    {
        private IUserRepo _repo;
        public UserCore(IUserRepo repo)
        {
            _repo = repo;
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
