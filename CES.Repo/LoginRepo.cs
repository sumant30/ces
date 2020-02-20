using CES.Entities.DB;
using CES.Entities.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CES.Repo
{
    public class LoginRepo : ILoginRepo
    {
        private IConfiguration _config;
        public LoginRepo(IConfiguration config)
        {
            _config = config;
        }
        public async Task<User> AuthenticateAsync(string username, string password)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);
                parameter.Add("@Password", password);

                var user = await con.QueryAsync<User>("AuthenticateUser", parameter, commandType: CommandType.StoredProcedure);

                return user.FirstOrDefault();
            }
        }

        public async Task SaveTokenAsync(Guid userId, string refreshToken)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);
                parameter.Add("@RefreshToken", refreshToken);

                await con.ExecuteAsync("SaveRefreshToken", parameter, commandType: CommandType.StoredProcedure);

                await Task.CompletedTask;
            }
        }
    }
}
