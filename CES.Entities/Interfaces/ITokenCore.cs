﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CES.Entities.Interfaces
{
    public interface ITokenCore
    {
        Task SaveTokenAsync(Guid userId, string refreshToken);
    }
}
