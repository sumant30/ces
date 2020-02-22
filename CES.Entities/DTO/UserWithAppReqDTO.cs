using System;
using System.Collections.Generic;
using System.Text;

namespace CES.Entities.DTO
{
    public class UserWithAppReqDTO: UserDTO
    {
        public List<AppReqDTO> AppReq { get; set; }
    }

}
