using CES.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ILogoutCore
    {
        Task<UserDTO> Logout(string username,string refreshToken);
    }
}
