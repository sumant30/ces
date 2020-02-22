using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task SaveAppRequest(Guid userId, Guid appId,string accessType)
        {
            await _repo.SaveAppRequest(userId, appId, accessType);
            await Task.CompletedTask;
        }
    }
}
