using Domain.Contracts;
using Infrastructure.Impelementations;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<IRespParser, RespParser>();

            services.AddTransient<IRedisKeyValueStoreInitiator, RedisKeyValueStoreInitiator>();


            return services;

            
        }
    }
}
