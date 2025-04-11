using Microsoft.Extensions.DependencyInjection;
using Presentation.Impelementations;
using Presentation.Interfaces;
using System.Net;
using System.Net.Sockets;
using Application;
using Infrastructure;
using Domain;
using Domain.Contracts;
using Microsoft.Extensions.Configuration;
using Domain.Models;

namespace Main
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<ServerWorker>();
            services.AddHostedService<RedisKeyValueStoreWorker>();

            var config = new ConfigurationBuilder()
                .AddCommandLine(Environment.GetCommandLineArgs())
                .Build();

            var args = new AppArguments();

            config.Bind(args);

            services.AddSingleton(args);

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