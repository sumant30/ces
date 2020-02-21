using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ITokenRepo
    {
        Task SaveTokenAsync(Guid userId, string refreshToken);
    }
}
