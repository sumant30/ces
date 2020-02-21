using CES.Entities.DB;
using CES.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ILoginRepo
    {
        Task<User> AuthenticateAsync(string username, string password);        
    }
}
