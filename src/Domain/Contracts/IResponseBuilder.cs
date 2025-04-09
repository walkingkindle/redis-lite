using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Domain.Contracts
{
    public interface IResponseBuilder
    {
        public string BuildRedisResponse(Result<RedisResponse> response);
    }
}
