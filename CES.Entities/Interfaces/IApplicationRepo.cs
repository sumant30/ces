using CES.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CES.Entities.Interfaces
{
    public interface IApplicationRepo
    {
        Task<Application> SaveAsync(Guid appId,string appName);       
    }
}
