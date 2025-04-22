using System.Collections.Concurrent;

namespace Domain.Implementations
{
    public class RedisKeyValueStore
    {
        public ConcurrentDictionary<string, RedisValue> RedisKeyValueStoreDictionary { get; } = new ConcurrentDictionary<string, RedisValue>();


        public void Add(string key, string value, double? expiryDate = null)
        {
            RedisKeyValueStoreDictionary.AddOrUpdate(
            key, 
            new RedisValue { Value = value, ExpirationDate =  expiryDate != null ? DateTime.Now.AddMilliseconds(expiryDate.Value) : null},
            (existingKey, existingValue) => new RedisValue
            {
                Value = value, 
                ExpirationDate = DateTime.Now.AddMilliseconds(expiryDate.Value) 
            });
        }

        public string? Get(string key)
        {
            RedisValue? redisValue;
            if(RedisKeyValueStoreDictionary.TryGetValue(key, out redisValue))
            {
                return redisValue.Value;
            }

            return null;
        }


        

    }

}
