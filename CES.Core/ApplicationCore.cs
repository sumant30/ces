using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Core
{
    public class ApplicationCore : IApplicationCore
    {
        private IApplicationRepo _repo;
        public ApplicationCore(IApplicationRepo repo)
        {
            _repo = repo;
        }
        public async Task<ApplicationDTO> SaveAsync(string appName)
        {
            var application = await _repo.SaveAsync(Guid.NewGuid(),appName);
            if(application!=null && application?.ID != Guid.Empty) 
            {
                return new ApplicationDTO()
                {
                     ID = application.ID,
                      Name = application.Name
                };
            }
            return new ApplicationDTO();
        }
    }
}
