using CES.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ILoginCore
    {
        Task<UserDTO> AuthenticateAsync(string username, string password);
    }
}
