using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface IUserCore
    {
        Task<Guid> GetAsync(string username, string refreshToken);
        Task<bool> ValidateClientSecret(string username,string clientSecret);
    }
}
