using CES.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface IUserRepo
    {
        Task<Guid> GetAsync(string username, string refreshToken);
        Task<Guid> GetAsync(string username);
        Task<User> GetUserDetails(string username);
        Task ChangePassword(string username, string password);
        Task<string> ForgotPassword(string username);
    }
}
