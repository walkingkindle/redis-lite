using Microsoft.Extensions.DependencyInjection;
using Presentation.Impelementations;
using Presentation.Interfaces;
using System.Net;
using System.Net.Sockets;
using Application;
using Infrastructure;
using Domain;
using Domain.Contracts;
using Domain.Implementations;

namespace Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<ServerWorker>();
            services.AddHostedService<RedisKeyValueStoreWorker>();

            services.AddSingleton<TcpListener>(sp =>
            {
                var ipEndPoint = new IPEndPoint(IPAddress.Any, 6379);
                var listener = new TcpListener(ipEndPoint);
                return listener;
            });
            services.AddTransient<IRouteManagerService, Router>();

            services.AddTransient<IResponseBuilder, ResponseBuilder>();


            services.AddApplicationServices();

            services.AddInfrastructureServices();

            services.AddDomainServices();



           



           


        }

        }
    }