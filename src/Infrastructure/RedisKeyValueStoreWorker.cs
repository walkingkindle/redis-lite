using Domain.Contracts;
using Domain.Implementations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure
{
    public class RedisKeyValueStoreWorker : BackgroundService
    {
        private readonly ILogger<RedisKeyValueStoreWorker> _logger;

        private readonly RedisKeyValueStore _store;

        private readonly IRedisKeyValueStoreInitiator _redisKeyValueStoreInitiator;

        public RedisKeyValueStoreWorker(ILogger<RedisKeyValueStoreWorker> logger, RedisKeyValueStore store, IRedisKeyValueStoreInitiator redisKeyValueStoreInitiator)
        {
            _logger = logger;
            _store = store;
            _redisKeyValueStoreInitiator = redisKeyValueStoreInitiator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _redisKeyValueStoreInitiator.FillDictionary();
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_store.RedisKeyValueStoreDictionary.IsEmpty)
                {
                    await Task.Delay(5, stoppingToken);
                    continue;
                }
                foreach (var entry in _store.RedisKeyValueStoreDictionary)
                {
                    if (entry.Value.ExpirationDate >= DateTime.Now)
                    {
                        continue;
                    }
                    RemoveExpiredItem(entry.Key);
                }

                await Task.Delay(5, stoppingToken); 
            }
        }

        
    public void RemoveExpiredItem(string key)
    {
        bool removed = _store.RedisKeyValueStoreDictionary.TryRemove(key, out var value);
        _logger.LogInformation($"Found some expired keys {key}");
        if (removed)
        {
            _logger.LogInformation($"Removed expired key: {key}");
        }
    }
    }
}
