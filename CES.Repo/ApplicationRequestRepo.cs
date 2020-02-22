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

namespace CES.Repo
{
    public class ApplicationRequestRepo : IApplicationRequestRepo
    {
        private IConfiguration _config;
        public ApplicationRequestRepo(IConfiguration config)
        {
            _config = config;
        }
        
        public async Task<int> RequestExists(Guid userId, Guid appId)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);
                parameter.Add("@AppId", appId);

                var count = await con.ExecuteScalarAsync<int>("GetAppRequest", parameter, commandType: CommandType.StoredProcedure);

                return count;
            }
        }

        public async Task SaveAppRequest(Guid userId, Guid appId, string accessType)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);
                parameter.Add("@AppId", appId);
                parameter.Add("@AccessType", accessType);

                await con.ExecuteScalarAsync("SaveAppRequest", parameter, commandType: CommandType.StoredProcedure);

                await Task.CompletedTask;
            }
        }

        public async Task<List<AppReq>> GetApplicationRequests(string username)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Username", username);


                var appReq = await con.QueryAsync<AppReq>("GetUserAppReqDetails", parameter, commandType: CommandType.StoredProcedure);

                return appReq?.AsList();
            }
        }

        public async Task Approve(Guid userId, Guid appId)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);
                parameter.Add("@AppId", appId);

                await con.ExecuteAsync("Approve", parameter, commandType: CommandType.StoredProcedure);

                await Task.CompletedTask;
            }
        }

        public async Task Reject(Guid userId, Guid appId)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@UserId", userId);
                parameter.Add("@AppId", appId);

                await con.ExecuteAsync("Reject", parameter, commandType: CommandType.StoredProcedure);

                await Task.CompletedTask;
            }
        }
    }
}
