using CES.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface IApplicationRequestRepo
    {
        Task<int> RequestExists(Guid userId, Guid appId);
        Task SaveAppRequest(Guid userId, Guid appId,string accessType);
        Task<List<AppReq>> GetApplicationRequests(string username);
    }
}
