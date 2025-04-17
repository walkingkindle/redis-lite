using Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Main;
class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
                          .ConfigureServices((hostContext, services) =>
                          {
                              var startup = new Startup(args);
                              startup.ConfigureServices(services);
                          });
        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var initiator = scope.ServiceProvider.GetRequiredService<IRedisKeyValueStoreInitiator>();
            await initiator.FillDictionary(); // Fill on startup
        }
        await host.RunAsync();
    }
}
