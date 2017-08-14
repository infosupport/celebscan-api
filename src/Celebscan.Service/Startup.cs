using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Celebscan.Service.Configuration;
using Celebscan.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Celebscan.Service
{
    /// <summary>
    /// Startup class for the application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Startup"/>
        /// </summary>
        /// <param name="env"></param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        /// <summary>
        /// This method is called by the runtime to setup the inversion of control container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IWikipediaBrowser, WikipediaBrowser>();
            services.AddSingleton<ICelebrityCache, CelebrityCache>();
            services.AddSingleton<ICelebrityStorage, CelebrityStorage>();

            services.Configure<DatabaseSettings>(Configuration.GetSection("Database"));
        }

        /// <summary>
        /// This method is called by the runtime to setup the HTTP pipeline
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="serviceProvider"></param>
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            loggerFactory.AddApplicationInsights(serviceProvider, LogLevel.Information);

            app.UseMvc();
        }
    }
}