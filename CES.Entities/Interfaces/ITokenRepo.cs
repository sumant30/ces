using CES.Entities.DB;
using CES.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ITokenRepo
    {
        Task<User> SaveTokenAsync(Guid userId, string refreshToken);
        Task Revoke(Guid userId);
    }
}
