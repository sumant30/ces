using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.Core
{
    public class ApplicationRequestCore : IApplicationRequestCore
    {
        private IApplicationRequestRepo _repo;
        public ApplicationRequestCore(IApplicationRequestRepo repo)
        {
            _repo = repo;
        }

        public async Task<bool> RequestExists(Guid userId, Guid appId)
        {
            var requestExists = await _repo.RequestExists(userId, appId);
            if (requestExists > 0)
            {
                return true;
            }
            return false;
        }

        public async Task SaveAppRequest(Guid userId, Guid appId, string accessType)
        {
            await _repo.SaveAppRequest(userId, appId, accessType);
            await Task.CompletedTask;
        }

        public async Task<List<AppReqDTO>> GetApplicationRequests(string username)
        {
            var req = await _repo.GetApplicationRequests(username);
            if (req?.Count > 0)
            {
                var appReq = new List<AppReqDTO>();
                req.ToList().ForEach(p =>
                {
                    appReq.Add(new AppReqDTO() { AppName = p.AppName, AccessType = p.AccessType });
                });

                return appReq;
            }
            return new List<AppReqDTO>();
        }

        public async Task Approve(Guid userId, Guid appId)
        {
            await _repo.Approve(userId, appId);
            await Task.CompletedTask;
        }

        public async Task Reject(Guid userId, Guid appId)
        {
            await _repo.Reject(userId, appId);
            await Task.CompletedTask;
        }
    }
}
