using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CES.Api.Helpers;
using CES.Core;
using CES.Entities.Interfaces;
using CES.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace CES
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {

            Configuration = configuration;

            BuildAppSettingsProvider();
        }

        private void BuildAppSettingsProvider()
        {
            TokenHelper.Config = Configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            //configure JWT authentication
            var key = Encoding.ASCII.GetBytes(Configuration.GetSection("Secret").Value);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            //configure DI container
            services.AddScoped<ILoginCore, LoginCore>();
            services.AddScoped<ILoginRepo, LoginRepo>();

            services.AddScoped<ILogoutCore, LogoutCore>();
            services.AddScoped<ILogoutRepo, LogoutRepo>();

            services.AddScoped<ITokenCore, TokenCore>();
            services.AddScoped<ITokenRepo, TokenRepo>();

            services.AddScoped<IUserCore, UserCore>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IUserCore, UserCore>();
            services.AddScoped<IUserRepo, UserRepo>();

            services.AddScoped<IUserManagementCore, UserManagementCore>();
            services.AddScoped<IUserManagementRepo, UserManagementRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
