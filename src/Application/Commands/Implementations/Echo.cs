using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Application.Commands.Implementations
{
    public class Echo : IMethodExecutor
    {
        public Result<RedisResponse> Execute(EndpointBase endpoint)
        {
            return Result.Success(RedisResponse.Bulk(endpoint.Input));
        }
    }
}
