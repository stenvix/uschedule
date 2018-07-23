using System;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using USchedule.Core.Enums;
using USchedule.Models.Domain;
using USchedule.Models.Domain.Base;
using USchedule.Models.DTO;
using USchedule.Persistence.Database;
using ILogger = NLog.ILogger;

namespace USchedule.API
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
            ConfigureDatabase(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper(typeof(IModel));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
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

            ConfigureLogging(serviceProvider.GetService<ILoggerFactory>());
            serviceProvider.GetService<DataContext>().Initialize(serviceProvider.GetService<ILogger<DataContext>>());


            app.UseMvc();
//            ConfigureDatabase(env.EnvironmentName, dataContext);
        }

        private void ConfigureLogging(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog(new NLogProviderOptions { CaptureMessageTemplates = true, CaptureMessageProperties = true });
            LogManager.LoadConfiguration("nlog.config");
        }

        private void ConfigureDatabase(IServiceCollection services)
        {
            var dbConfig = Configuration.GetSection("DbConfiguration").Get<DbConfig>();
            if (dbConfig == null)
            {
                var provider = Configuration["DbProvider"];
                var connection = Configuration["DbConnection"];
                if (Enum.TryParse(provider, out DatabaseProvider dbProvider) && !string.IsNullOrEmpty(connection))
                {
                    SetProviderConfiguration(services, dbProvider, connection);
                    return;
                }

                throw new ArgumentNullException(nameof(dbConfig), "Database configuration missing.");
            }

            SetProviderConfiguration(services, dbConfig.Provider, dbConfig.Connection);
        }

        private void SetProviderConfiguration(IServiceCollection services, DatabaseProvider provider, string connection)
        {
            switch (provider)
            {
                case DatabaseProvider.Postgres:
                {
                    services.AddEntityFrameworkNpgsql()
                        .AddDbContext<DataContext>(options => options.UseNpgsql(connection));
                    break;
                }
                case DatabaseProvider.InMemory:
                {
                    services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase(connection));
                    break;
                }
            }
        }
    }
}