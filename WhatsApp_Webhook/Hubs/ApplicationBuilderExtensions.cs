using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsApp_Webhook.Hubs
{
    public static class ApplicationBuilderExtensions
    {
        //public static void UseSqlTableDependency<T>(this IApplicationBuilder services, string connectionString)
        //   where T : IDatabaseSubscription
        //{
        //    var serviceProvider = services.ApplicationServices;
        //    var subscription = serviceProvider.GetService<T>();
        //    subscription.Configure(connectionString);
        //}

        public static void UseSqlTableDependencys<T>(this IApplicationBuilder services, string connectionString) where T : IDatabaseSubscription
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = serviceProvider.GetService<T>();
            subscription.Configure(connectionString);
        }

          
            public static void UseSqlTableDependency<T>(this IApplicationBuilder builder, string connectionString)
            {
            var _provider = builder.ApplicationServices;
            using (var serviceScope = _provider.GetRequiredService<IServiceScopeFactory>().CreateScope())
          
            {
                var  context = serviceScope.ServiceProvider.GetService<IDatabaseSubscription>();
                context.Configure(connectionString);
            }

           
        }
        


    }
}
