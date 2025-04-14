using Infrastructure.Impelementations;
using Infrastructure.Interfaces;
using Infrastructure.RDBPersistence.Contracts;
using Infrastructure.RDBPersistence.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IRespParser, RespParser>();

            services.AddTransient<IHexReader, HexReader>();

            return services;

            
        }
    }
}
