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
        public Task SaveTokenAsync(Guid userId, string refreshToken)
        {
            throw new NotImplementedException();
        }
    }
}
