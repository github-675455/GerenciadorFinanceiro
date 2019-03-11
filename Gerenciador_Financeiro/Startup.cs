﻿using System;
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
using AutoMapper;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Versioning;
using System.Diagnostics;
using Gerenciador_Financeiro.Services;
using Gerenciador_Financeiro.Interfaces;

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
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                    builder
                    .AllowAnyOrigin() 
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));

            services.AddMvcCore()
            .AddAuthorization()
            .SetCompatibilityVersion(CompatibilityVersion.Latest)
            .AddFormatterMappings()
            .AddJsonFormatters().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            services.AddApiVersioning(o => {
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                o.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddTransient<IUsuarioService, UsuarioService>();

            var defaultConnection = Environment.GetEnvironmentVariable("DefaultConnection");

            defaultConnection = String.IsNullOrEmpty(defaultConnection) ? Configuration.GetConnectionString("DefaultConnection") : defaultConnection;

            services.AddHealthChecks()
            .AddMySql(defaultConnection);

            services.AddDbContextPool<GerenciadorFinanceiroContext>(options => options.UseMySql(defaultConnection,
            mySqlOptions =>
            {
                mySqlOptions.ServerVersion(new Version(5, 7, 17), ServerType.MySql);
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
                
                jwtBearerOptions.Configuration = new OpenIdConnectConfiguration();

                jwtBearerOptions.Audience = "http://localhost:5000/";
                jwtBearerOptions.Authority = "http://localhost:5000/";
                jwtBearerOptions.RequireHttpsMetadata = false;

                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                {                            
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),

                    ValidateIssuer = true,
                    ValidIssuer = "backend-gerenciador-financeiro",

                    ValidateAudience = true,
                    ValidAudience = "frontend-gerenciador-financeiro",

                    ValidateLifetime = true, //validate the expiration and not before values in the token
                    ClockSkew = TimeSpan.FromMinutes(5), //5 minute tolerance for the expiration date
                };
            });

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, GerenciadorFinanceiroContext context)
        {
            context.Database.Migrate();

            app.UseHealthChecks("/health");

            if (env.IsDevelopment())
            {
                app.UseCors("MyPolicy");
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
