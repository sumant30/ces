﻿using CES.Entities.DB;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ILogoutRepo
    {
        Task<User> LogoutAsync(Guid userId);
    }
}
