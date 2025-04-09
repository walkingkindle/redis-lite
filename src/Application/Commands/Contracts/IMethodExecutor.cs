using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Application.Commands.Contracts
{
    public interface IMethodExecutor
    {
        public Result<RedisResponse> Execute(EndpointBase endpoint);
    }
}