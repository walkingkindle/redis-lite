using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Application.Commands.Contracts
{
    public interface ICommandHandler{
        public Result<RedisResponse> Handle(Result<EndpointBase> endpointPayload);
    }
}
