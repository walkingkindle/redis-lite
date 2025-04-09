using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Application.Commands.Implementations
{
    public class Ping : IMethodExecutor
    {
        public Result<RedisResponse> Execute(EndpointBase pingEndpoint)
        {
            return Result.Success(RedisResponse.Simple("PONG"));
        }
    }
}
