using AutoMapper;
using KissLog;
using KissLog.Apis.v1.Listeners;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MT.API.ApiConfiguration;
using MT.API.Extensions;
using MT.Data.Context;
using System;
using System.Diagnostics;
using System.Text;

namespace MT.Api
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
      

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.WebApiConfig();

            services.AddSwaggerConfig();
            services.AddLoggingConfiguration(Configuration);

            services.ResolveDependencies();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            UpdateDatabase();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseCors("Development");
                app.UseDeveloperExceptionPage();
            }
            else
            {
           
                app.UseHsts();
            }
            //app.UseKissLogMiddleware(options => {
            //    ConfigureKissLog(options);
            //});

            app.UseLoggingConfiguration();

            app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

            app.UseMvcConfiguration();

            app.UseSwaggerConfig(provider);
        }

        private void ConfigureKissLog(IOptionsBuilder options)
        {
            // register KissLog.net cloud listener
            options.Listeners.Add(new KissLogApiListener(new KissLog.Apis.v1.Auth.Application(
                Configuration["KissLog:KissLog.OrganizationId"],   
                Configuration["KissLog:KissLog.ApplicationId"])    
            )
            {
                ApiUrl = Configuration["KissLog:KissLog.ApiUrl"]  
            });

            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((Exception ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is System.NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };
        }

        private void UpdateDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BContext>();
            var options =
                optionsBuilder
                .UseSqlite(Configuration.GetConnectionString("DefaultConnection"))
                .Options;

            using (var context = new BContext(options))
            {
                context.Database.Migrate();
            }
        }
    }
}
