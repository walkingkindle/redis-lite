using Application.Commands.Contracts;
using Application.Commands.Implementations;
using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Application.CommandHandlers.Implementations
{
    public class CommandHandler : ICommandHandler
    {
        private readonly Ping _ping;

        private readonly Error _error;

        private readonly Echo _echo;

        private readonly Get _get;

        private readonly Set _set;

        private readonly ConfigGet _configGet;

        private readonly Keys _keys;

        public CommandHandler(Ping ping, Error error, Echo echo, Get get, Set set, ConfigGet configGet, Keys keys)
        {
            _ping = ping;
            _error = error;
            _echo = echo;
            _get = get;
            _set = set;
            _configGet = configGet;
            _keys = keys;
        }

        public Result<RedisResponse> Handle(Result<EndpointBase> endpointPayload)
        {
            if (endpointPayload.IsFailure)
            {
                return Result.Failure<RedisResponse>(endpointPayload.Error);
            }
            switch (endpointPayload.Value.Command)
            {
                case "PING":
                    return _ping.Execute(endpointPayload.Value);
                case "ECHO":
                    return _echo.Execute(endpointPayload.Value);
                case "GET":
                    return _get.Execute(endpointPayload.Value);
                case "SET":
                    return _set.Execute(endpointPayload.Value);
                case "CONFIG GET":
                    return _configGet.Execute(endpointPayload.Value);
                case "KEYS":
                    return _keys.Execute(endpointPayload.Value);
                default:
                    return _error.Execute(endpointPayload.Value);
            }

      }
    }
}
