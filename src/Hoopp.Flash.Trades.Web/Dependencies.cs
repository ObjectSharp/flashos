using Hoopp.Flash.Core.Services;
using Hoopp.Flash.Trades.Domain.Configuration;
using Hoopp.Flash.Trades.Domain.DataAccess;
using Hoopp.Flash.Trades.Domain.Models;
using Hoopp.Flash.Trades.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Hoopp.Flash.Trades.Web
{
    // TIP: Isolate your DI code to a single class
    public static class Dependencies
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration config)
        {
            // NOTE: Use singletons whenever possible to decrease memory utilization

            // Configuration
            services.Configure<ConnectionStringsOptions>(config.GetSection("ConnectionStrings"));
            services.Configure<TradeServiceOptions>(config.GetSection("TradeService"));

            // Data Access
            services.AddSingleton<ITradeRepository, InMemoryTradeRepository>();

            // Services 
            services.AddSingleton<ILocalTimeService, LocalTimeService>();
            services.AddSingleton<IGuidGeneratorService, GuidGeneratorService>();
            services.AddTradeService(config);
        }

        // TIP: Complex registrations can be further isolated into separate methods
        private static void AddTradeService(this IServiceCollection services, IConfiguration config)
        {
            // This could have been done using implicit service discovery, but just showing
            // an example of using code to do more specific service intialization
            services.AddSingleton<ITradeService>(r =>
            {
                var svc = new TradeService(
                    r.GetRequiredService<IOptions<TradeServiceOptions>>(),
                    r.GetRequiredService<ITradeRepository>(),
                    r.GetRequiredService<IGuidGeneratorService>(),
                    r.GetRequiredService<ILocalTimeService>()
                );
                return svc;
            });
        }
    }
}
