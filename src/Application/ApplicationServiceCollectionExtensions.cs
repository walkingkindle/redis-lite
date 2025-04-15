using Application.CommandHandlers.Implementations;
using Application.Commands.Contracts;
using Application.Commands.Implementations;
using Application.Parsers.Contracts;
using Application.Parsers.Implementations;
using Application.RDBPersistence.Contracts;
using Application.RDBPersistence.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
             services.AddTransient<ICommandParser, EchoParser>();

            services.AddTransient<ICommandParser, PingParser>();

            services.AddTransient<ICommandParser, GetParser>();
            services.AddTransient<ICommandParser, SetParser>();

            services.AddTransient<ICommandParser, KeysParser>();

            services.AddTransient<ICommandParser, ConfigGetParser>();

            services.AddTransient<Ping>();

            services.AddTransient<Echo>();

            services.AddTransient<Error>();

            services.AddTransient<Set>();

            services.AddTransient<Get>();

            services.AddTransient<ConfigGet>();

            services.AddTransient<Keys>();

            services.AddTransient<ICommandHandler, CommandHandler>();

            services.AddTransient<IHexReader, HexReader>();

            services.AddTransient<IGenericKeyValueRDBParser, GenericKeyValueRDBParser>();




            return services;

        }
    }
}
