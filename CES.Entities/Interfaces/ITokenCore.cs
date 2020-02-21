using CES.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ITokenCore
    {
        Task<UserDTO> GenerateNewTokenAsync(Guid userId);
        Task Revoke(Guid userId);
    }
}
