using CES.Entities.DTO;
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
        Task<UserWithAppReqDTO> GetUserSessionDetails(string username);
        Task ChangePassword(string username, string password);
        Task<string> ForgotPassword(string username);
    }
}
