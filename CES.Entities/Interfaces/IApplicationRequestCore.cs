using CES.Entities.DB;
using CES.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface IApplicationRequestCore
    {
        Task<bool> RequestExists(Guid userId, Guid appId);
        Task SaveAppRequest(Guid userId, Guid appId,string accessType);
        Task<List<AppReqDTO>> GetApplicationRequests(string username);
    }
}
