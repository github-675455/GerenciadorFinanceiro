using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Gerenciador_Financeiro.Context;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;

namespace Gerenciador_Financeiro
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
            .AddAuthorization()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFormatterMappings()
            .AddJsonFormatters();
            services.AddDbContextPool<GerenciadorFinanceiroContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
            mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql); // replace with your Server Version and Type
                    }));
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";            
            })
            .AddJwtBearer("JwtBearer", jwtBearerOptions =>
            {
                var secret = Environment.GetEnvironmentVariable("appsecretkey");
                if(secret == null)
                    secret = "test secret key, please change it";
                
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {                            
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

                    ValidateIssuer = true,
                    ValidIssuer = "backend-gerenciador-financeiro",

                    ValidateAudience = true,
                    ValidAudience = "frontend-gerenciador-financeiro",

                    ValidateLifetime = true, //validate the expiration and not before values in the token

                    ClockSkew = TimeSpan.FromMinutes(5) //5 minute tolerance for the expiration date
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
                app.UseHttpsRedirection();
            }

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
