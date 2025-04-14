using Application.Commands.Contracts;
using CSharpFunctionalExtensions;
using Domain.Implementations;
using Domain.Interfaces;

namespace Application.Commands.Implementations
{
    public class Keys : IMethodExecutor
    {
        private readonly RedisKeyValueStore _redisKeyValueStore;

        public Keys(RedisKeyValueStore redisKeyValueStore)
        {
            _redisKeyValueStore = redisKeyValueStore;
        }

        public Result<RedisResponse> Execute(EndpointBase endpoint)
        {
            List<RedisResponse> keys = new();
            if (_redisKeyValueStore.RedisKeyValueStoreDictionary.IsEmpty)
            {
                return Result.Success(RedisResponse.NullArray());
            }
            foreach(var entry in _redisKeyValueStore.RedisKeyValueStoreDictionary)
            {
                keys.Add(RedisResponse.Bulk(entry.Key));
            }

            return Result.Success(RedisResponse.Array(keys));




            
        }
    }
}
