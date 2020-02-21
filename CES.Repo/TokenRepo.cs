using CES.Entities.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace CES.Repo
{
    public class TokenRepo : ITokenRepo
    {
        private IConfiguration _config;
        public TokenRepo(IConfiguration config)
        {
            _config = config;
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
