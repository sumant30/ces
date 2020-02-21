using CES.Entities.DB;
using CES.Entities.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CES.Repo
{
    public class LogoutRepo : ILogoutRepo
    {
        private IConfiguration _config;
        public LogoutRepo(IConfiguration config)
        {
            _config = config;
        }
           

        public async Task<User> LogoutAsync(Guid userId)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);


                var user = await con.QueryAsync<User>("RemoveRefreshToken", parameter, commandType: CommandType.StoredProcedure);

                return user.FirstOrDefault();
            }
        }

        
    }
}
