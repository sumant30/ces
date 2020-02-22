using CES.Entities.DB;
using CES.Entities.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CES.Repo
{
    public class UserRepo : IUserRepo
    {
        private IConfiguration _config;
        public UserRepo(IConfiguration config)
        {
            _config = config;
        }

        public async Task<Guid> GetAsync(string username, string refreshToken)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);
                parameter.Add("@RefreshToken", refreshToken);


                var userId = await con.ExecuteScalarAsync<Guid>("GetUserId", parameter, commandType: CommandType.StoredProcedure);

                return userId;
            }
        }

        public async Task<Guid> GetAsync(string username)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);

                var userId = await con.ExecuteScalarAsync<Guid>("GetUserIdByUsername", parameter, commandType: CommandType.StoredProcedure);

                return userId;
            }
        }

        public async Task<User> GetUserDetails(string username)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);


                var user = await con.QueryAsync<User>("GetUserDetails", parameter, commandType: CommandType.StoredProcedure);

                return user?.FirstOrDefault();
            }
        }

        public async Task ChangePassword(string username, string password)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);
                parameter.Add("@Password", password);


                await con.ExecuteAsync("ChangePassword", parameter, commandType: CommandType.StoredProcedure);

                await Task.CompletedTask;
            }
        }

        public async Task<string> ForgotPassword(string username)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);

                var password = await con.ExecuteScalarAsync<string>("ForgotPassword", parameter, commandType: CommandType.StoredProcedure);

                return password;
            }
        }
    }
}
