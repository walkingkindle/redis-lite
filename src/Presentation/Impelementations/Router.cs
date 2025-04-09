using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Contracts;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Presentation.Interfaces;
using System.Net.Sockets;
using System.Text;

namespace Presentation.Impelementations
{
    public class Router: IRouteManagerService
    {
        private readonly IRespParser _parser;
        private readonly ICommandHandler _handler;
        private readonly IResponseBuilder _responseBuilder;
        public Router(IRespParser parser, ICommandHandler handler, IResponseBuilder responseBuilder)
        {
            _parser = parser;
            _handler = handler;
            _responseBuilder = responseBuilder;

            
        }

        public async Task HandleClient(TcpClient handler, NetworkStream stream)
        {
            try
            {
                byte[] buffer = new byte[256];

                while (handler.Connected)
                {
                    int readTotal = await stream.ReadAsync(buffer, 0, buffer.Length);

                    if (readTotal == 0) break;

                    Result<EndpointBase> endpoint = _parser.ParseRespString(buffer, readTotal);

                    Result<RedisResponse> handlerResult = _handler.Handle(endpoint);

                    string response = _responseBuilder.BuildRedisResponse(handlerResult);

                    await stream.WriteAsync(Encoding.UTF8.GetBytes(response));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Client error: {ex.Message}");
            }
            finally{
                stream.Close();
                handler.Close();
            }
        }

    }
 }
