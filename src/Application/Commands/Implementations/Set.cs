using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Implementations;
using Domain.Interfaces;

namespace Application.Commands.Implementations
{
    public class Set : IMethodExecutor
    {
        private readonly RedisKeyValueStore _redisKeyValueStore;

        public Set(RedisKeyValueStore redisKeyValueStore)
        {
            _redisKeyValueStore = redisKeyValueStore;
        }

        public Result<RedisResponse> Execute(EndpointBase endpoint)
        {
            try
            {
                if(endpoint.Value is null)
                {
                    return Result.Failure<RedisResponse>("ERR");
                }
                _redisKeyValueStore.Add(endpoint.Input, endpoint.Value,endpoint.Expiry);
                return Result.Success(RedisResponse.Simple("OK"));
            }
            catch(Exception)
            {
                return Result.Failure<RedisResponse>("Adding value failed");
            }


        }
    }
}
