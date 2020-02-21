using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface IUserRepo
    {
        Task<Guid> GetAsync(string username, string refreshToken);
    }
}
