﻿using FluentValidation.AspNetCore;
using Honoplay.Common.Constants;
using Honoplay.Domain.Entities;
using Honoplay.Persistence;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;

namespace Honoplay.AdminWebAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Application.AssemblyIdentifier>());

            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>)); // TODO: çalışan bir nokta görülmediği için kontrol amaçlı kapatıldı
            services.AddMediatR(Application.AssemblyIdentifier.Get());

            // Add DbContext using SQL Server Provider
            services.AddDbContext<HonoplayDbContext>(options =>
                options.UseSqlServer(StringConstants.ConnectionString,
                                    b => b.MigrationsAssembly("Honoplay.Persistence")));

            services.AddIdentity<AdminUser, IdentityRole>()
                    //.AddEntityFrameworkStores<HonoplayDbContext>()
                    .AddDefaultTokenProviders();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = System.Text.Encoding.ASCII.GetBytes(appSettings.JWTSecret);
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
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = y => Task.CompletedTask,
                    OnAuthenticationFailed = y =>
                    {
                        Console.WriteLine("Exception:{0}", y.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "HonoPlay API", Version = "v1" });
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "HonoPlay API V1");
            });

            app.UseMvc();
        }
    }
}