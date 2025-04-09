using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.Interfaces;
using System.Net.Sockets;

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
