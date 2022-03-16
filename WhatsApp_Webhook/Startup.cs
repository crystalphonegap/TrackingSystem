using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NPS.Models;
using NPS.Services;
using WhatsApp_Webhook.Hubs;
using Newtonsoft;

namespace WhatsApp_Webhook
{
    public class Startup
    {
       // private const string ConnectionString = @"server=NYVMA13;database=NPS;Integrated Security = true";

        private const string ConnectionString = @"Data Source=NYVMA13;Database=NPS;Persist Security Info=false; Integrated Security=false;User Id=sa;Password=ESXserver35";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddDbContextPool<AppDbContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("NPSDBConnection"));
            //});


            services.AddDbContextHelpers<AppDbContext>(Configuration.GetConnectionString("NPSDBConnection"));
            services.AddControllers().AddNewtonsoftJson(options =>
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

           // services.AddControllers();
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddSignalR();
            services.AddRazorPages();
           
           
           // services.AddSingleton<IDatabaseSubscription, NPSDatabaseSubscription>();
            services.AddSingleton<INPSRespository, SQLNpsRepository>();
            services.AddSingleton<IWABARepository, SQLWABARepository>();
            //services.AddSingleton<DataProtectionPurposeStrings>();

            //services.AddSingleton<IDatabaseSubscription, NPSDatabaseSubscription>();
            //services.AddScoped<IHubContext<Chart>, Hubs<Chart>>();
            //services.AddSingleton<IHubContext<Chart>, HubContext<Chart>>();
            //services.AddScoped<IHubContext<Chart>, IHubContext<Chart>>();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });


            //services.Configure<RouteOptions>(options =>
            //    {
            //        options.LowercaseUrls = true;
            //        //options.LowercaseQueryStrings = true;
            //        options.AppendTrailingSlash = true;
            //    });
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
            app.UseStaticFiles();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();

                endpoints.MapHub<Chart>("/charts");
            });

            app.UseCors("CorsPolicy");
            //using (var serviceScope = app.ApplicationServices.CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetService<IDatabaseSubscription>();
            //    context.Configure(ConnectionString);
            //    // Seed the database.
            //}
            //app.UseSqlTableDependency<INPSRespository>(ConnectionString);
   //app.UseSqlTableDependency<IDatabaseSubscription>(Configuration.GetConnectionString("NPSDBConnection"));
            // app.UseSqlTableDependency<IDatabaseSubscription>(Configuration.GetConnectionString("NPSDBConnection"));

            // app.UseSqlTableDependency<NPSDatabaseSubscription>(Configuration.GetConnectionString("NPSDBConnection"));

        }
    }
}
