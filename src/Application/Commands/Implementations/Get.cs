using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Implementations;
using Domain.Interfaces;

namespace Application.Commands.Implementations
{
    public class Get : IMethodExecutor
    {
        private readonly RedisKeyValueStore _redisKeyValueStore;

        public Get(RedisKeyValueStore redisKeyValueStore)
        {
            _redisKeyValueStore = redisKeyValueStore;
        }

        public Result<RedisResponse> Execute(EndpointBase endpoint)
        {
            string? redisValue = _redisKeyValueStore.Get(endpoint.Input);
            if (redisValue is not null)
            {
                return Result.Success(RedisResponse.Bulk(redisValue));
            }
            return Result.Success<RedisResponse>(RedisResponse.Simple("nil"));


        }
    }
}
