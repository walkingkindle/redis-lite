using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Presentation.Interfaces;
using System.Net.Sockets;

namespace Infrastructure
{
    public class ServerWorker : BackgroundService
    {
        private readonly ILogger<ServerWorker> _logger;
        private readonly TcpListener _listener;
        private readonly IRouteManagerService _clientHandler;

        public ServerWorker(ILogger<ServerWorker> logger, TcpListener listener, IRouteManagerService clientHandler)
        {
            _logger = logger;
            _listener = listener;
            _clientHandler = clientHandler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listener.Start();
            _logger.LogInformation("Server is running on port 6739...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var handler = await _listener.AcceptTcpClientAsync();
                    var stream = handler.GetStream();

                    _ = Task.Run(() => _clientHandler.HandleClient(handler, stream));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Server error: {ex.Message}");
            }
        }
    }

}
