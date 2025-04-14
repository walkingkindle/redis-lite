using Domain.Contracts;
using Domain.Models;
using Domain.RDBPersistence;

namespace Infrastructure.Implementation
{
    public class RedisKeyValueStoreInitiator : IRedisKeyValueStoreInitiator
    {
        private readonly AppArguments _args;

        public RedisKeyValueStoreInitiator(AppArguments args)
        {
            _args = args;
        }

        public async Task FillDictionary()
        {
            string path = $"{_args.Dir}/{_args.DbFileName}";

            byte[] bytesArray = await File.ReadAllBytesAsync(path);

            RedisMessage redisMessage = new RedisMessage();

            redisMessage.Header = "REDIS011";




        }
    }
}
