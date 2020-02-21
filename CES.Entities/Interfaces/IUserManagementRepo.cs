using CES.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface IUserManagementRepo
    {
        Task<List<User>> Get();
        Task<User> Get(Guid userId);
    }
}
