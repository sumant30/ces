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
    public class ApplicationRepo : IApplicationRepo
    {
        private IConfiguration _config;
        public ApplicationRepo(IConfiguration config)
        {
            _config = config;
        }
        public async Task<Application> SaveAsync(Guid appId, string appName)
        {
            string connectionString = Convert.ToString(_config.GetConnectionString("CESConnection"));
            using (IDbConnection con = new SqlConnection(connectionString))
            {
                if (con.State == ConnectionState.Closed)
                    con.Open();

                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@ApplicationName", appName);

                var app = await con.QueryAsync<Application>("SaveApplication", parameter, commandType: CommandType.StoredProcedure);

                return app?.FirstOrDefault();
            }
        }
    }
}
