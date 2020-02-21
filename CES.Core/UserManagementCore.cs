using CES.Entities.DTO;
using CES.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CES.Core
{
    public class UserManagementCore : IUserManagementCore
    {
        private IUserManagementRepo _repo;
        public UserManagementCore(IUserManagementRepo repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<UserForAdminDTO>> Get()
        {
            var users = await _repo.Get();
            if (users?.Count > 0)
            {
                var lst = new List<UserForAdminDTO>();

                users?.ToList().ForEach(p =>
                {
                    lst.Add(new UserForAdminDTO()
                    {
                        Id = p.Id,
                        RefreshToken = p.RefreshToken,
                        Username = p.Username
                    });
                });

                return lst;
            }
            return new List<UserForAdminDTO>();
        }

        public async Task<UserForAdminDTO> Get(Guid userId)
        {
            var user = await _repo.Get(userId);
            if (user!=null)
            {
                return new UserForAdminDTO() 
                { 
                    Id = user.Id, 
                    RefreshToken = user.RefreshToken, 
                    Username = user.Username 
                };
            }
            return new UserForAdminDTO();
        }
    }
}
