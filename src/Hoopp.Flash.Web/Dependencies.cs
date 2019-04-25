using Hoopp.Flash.Domain.Configuration;
using Hoopp.Flash.Domain.DataAccess;
using Hoopp.Flash.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hoopp.Flash.Web
{
    // TIP: Isolate your DI code to a single class
    public static class Dependencies
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            // Configuration
            services.Configure<ConnectionStringsOptions>(config.GetSection("ConnectionStrings"));
            services.Configure<TradeServiceOptions>(config.GetSection("TradeService"));

            // Data Access
            services.AddSingleton<ITradeRepository, InMemoryTradeRepository>();
            
            // Services 
            services.AddComplexDependency(config);
        }

        // TIP: Complex registrations can be further isolated into separate methods
        public static void AddComplexDependency(this IServiceCollection services, IConfiguration config)
        {
            
        }
    }
}
