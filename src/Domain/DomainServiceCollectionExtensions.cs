using Domain.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Domain
{
   public static class DomainServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<RedisKeyValueStore>();

            return services;
        }
    }
}
