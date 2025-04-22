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
        private readonly AppArguments _args;
        private readonly IRDBByteParser _rdbByteParser;


        public RedisKeyValueStoreInitiator(RedisKeyValueStore redisKeyValueStore, IRDBByteParser rdbyteParser, AppArguments appArguments)
        {
            _keyValueStore = redisKeyValueStore;
            _args = appArguments;
            _rdbByteParser = rdbyteParser;
            
        }
        public void FillDictionary()
        {
            string path = $"{_args.Dir}/{_args.DbFileName}";

            if (!File.Exists(path))
            {
                return;
            }
            RedisMessage redisMessageFromFile = _rdbByteParser.ParseRDBFile(File.ReadAllBytes(path));

            foreach (var entry in redisMessageFromFile.RedisKeyValue)
            {

                if (_keyValueStore.Get(entry.Key) is null)
                {
                    _keyValueStore.Add(entry.Key, entry.Value);
                }
            }

        }
    }
}
