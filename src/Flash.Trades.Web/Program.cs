using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.EnvironmentVariables;

namespace Flash.Trades.Web
{
    [ExcludeFromCodeCoverage]
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var envVarConfig = (EnvironmentVariablesConfigurationSource)config.Sources
                        .FirstOrDefault(x => x.GetType() == typeof(EnvironmentVariablesConfigurationSource));

                    if (envVarConfig != null)
                        envVarConfig.Prefix = "FLASHOS-TRADE-";

                })
                .UseStartup<Startup>();

            return builder;
        }
    }
}
