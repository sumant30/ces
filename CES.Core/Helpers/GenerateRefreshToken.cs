using System;
using System.Collections.Generic;
using System.Text;

namespace CES.Core.Helpers
{
    public static class GenerateRefreshToken
    {
        public static string GetToken() 
        { 
            return Guid.NewGuid().ToString().Replace("-", "");
        } 
    }
}
