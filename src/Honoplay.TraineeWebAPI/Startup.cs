﻿using FluentValidation.AspNetCore;
using Honoplay.Application;
using Honoplay.Common.Constants;
using Honoplay.Persistence;
using Honoplay.Persistence.CacheManager;
using Honoplay.Persistence.CacheService;
using Honoplay.TraineeWebAPI.Interfaces;
using Honoplay.TraineeWebAPI.Services;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Honoplay.TraineeWebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddResponseCaching();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Application.AssemblyIdentifier>());

            //Add RedisCache
            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "honoplay";
                options.Configuration = Environment.GetEnvironmentVariable("REDIS_CACHE_SERVICE")?.ToString();
            });
            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
            // Add MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>)); // TODO: çalışan bir nokta görülmediği için kontrol amaçlı kapatıldı
            services.AddMediatR(AssemblyIdentifier.Get());

            services.AddScoped<ITraineeUserService, TraineeUserService>();
            services.AddSingleton<ICacheService, CacheManager>();

            // Add DbContext using SQL Server Provider
            services.AddDbContextPool<HonoplayDbContext>(options =>
                options.UseSqlServer(StringConstants.ConnectionString,
                                    b => b.MigrationsAssembly("Honoplay.Persistence")));

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
                //x.RequireHttpsMetadata = false;
                //x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                x.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = y =>
                    {
                        if (y.Principal.Claims.First(c => c.Type == ClaimTypes.Webpage).Value != y.HttpContext.Request.Host.Host)
                        {
                            y.Fail(new UnauthorizedAccessException());
                        }
                        return Task.CompletedTask;
                    },
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
                c.AddSecurityDefinition("Bearer",
                new ApiKeyScheme
                {
                    In = "header",
                    Description = "Please enter into field the word 'Bearer' following by space and JWT",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
            });
                // Configure Swagger to use the xml documentation file
                var xmlFile = Configuration.GetValue<string>(WebHostDefaults.ContentRootKey) + "/SwaggerDoc.xml";
                c.IncludeXmlComments(xmlFile);

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
            app.UseResponseCaching();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api/trainee/swagger/v1/swagger.json", "HonoPlay API V1");

            });

            app.UseCors(builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            app.UseMvc();
        }
    }
}
