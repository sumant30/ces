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
    public class UserManagementRepo : IUserManagementRepo
    {
        private IConfiguration _config;

        public UserManagementRepo(IConfiguration config)
        {
            _config = config;
        }
        public async Task<List<User>> Get()
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();                          
                
                var users = await con.QueryAsync<User>("GetAllUsers",commandType: CommandType.StoredProcedure);

                return users?.ToList();
            }
        }

        public async Task<User> Get(Guid userId)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);
                
                var user = await con.QueryAsync<User>("GetUser", parameter, commandType: CommandType.StoredProcedure);

                return user?.FirstOrDefault();
            }
        }
    }
}
