using Microsoft.Extensions.Hosting;

namespace Main;
class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
                          .ConfigureServices((hostContext, services) =>
                          {
                              var startup = new Startup();
                              startup.ConfigureServices(services);
                          });
        var host = builder.Build();
        await host.RunAsync();
    }
}
