using Application.RDBPersistence.Contracts;
using Domain.Contracts;
using Domain.Implementations;
using Domain.RDBPersistence;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class RedisKeyValueStoreInitiator : IRedisKeyValueStoreInitiator
    {

        private readonly RedisKeyValueStore _keyValueStore;
        private readonly IHexReader _hexReader;

        public RedisKeyValueStoreInitiator(RedisKeyValueStore redisKeyValueStore, IHexReader hexReader)
        {
            _keyValueStore = redisKeyValueStore;

            _hexReader = hexReader;
        }
        public async Task FillDictionary()
        {
            RedisMessage redisMessageFromFile = await  _hexReader.ReadRedisMessage();

            if (_keyValueStore.Get(redisMessageFromFile.RedisKeyValue.Key) is null){

                _keyValueStore.Add(redisMessageFromFile.RedisKeyValue.Key, redisMessageFromFile.RedisKeyValue.Value);
            }

        }
    }
}
