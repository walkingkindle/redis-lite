using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Commands.Implementations
{
    public class ConfigGet : IMethodExecutor
    {

        private readonly AppArguments _args;

        public ConfigGet(AppArguments args)
        {
            _args = args;
        }

        public Result<RedisResponse> Execute(EndpointBase endpoint)
        {
            if (!string.IsNullOrEmpty(_args.Dir))
            {
                return Result.Success(RedisResponse.Array(new List<RedisResponse> { RedisResponse.Bulk("dir"), RedisResponse.Bulk(_args.Dir) }));
            }

            return Result.Success(RedisResponse.Error("Error reading args"));
        }
    }
}
