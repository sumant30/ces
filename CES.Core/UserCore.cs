using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
