using Application.RDBPersistence.Contracts;
using Domain.Contracts;
using Domain.Implementations;
using Domain.Models;
using Domain.RDBPersistence;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class RedisKeyValueStoreInitiator : IRedisKeyValueStoreInitiator
    {

        private readonly RedisKeyValueStore _keyValueStore;
        private readonly IHexReader _hexReader;
        private readonly AppArguments _args;


        public RedisKeyValueStoreInitiator(RedisKeyValueStore redisKeyValueStore, IHexReader hexReader, AppArguments appArguments)
        {
            _keyValueStore = redisKeyValueStore;

            _hexReader = hexReader;
            _args = appArguments;
        }
        public async Task FillDictionary()
        {
            string path = $"{_args.Dir}/{_args.DbFileName}";

            if (!File.Exists(path))
            {
                return;
            }
            RedisMessage redisMessageFromFile = await  _hexReader.ReadRedisMessage(path);

            if (_keyValueStore.Get(redisMessageFromFile.RedisKeyValue.Key) is null){

                _keyValueStore.Add(redisMessageFromFile.RedisKeyValue.Key, redisMessageFromFile.RedisKeyValue.Value);
            }

        }
    }
}
