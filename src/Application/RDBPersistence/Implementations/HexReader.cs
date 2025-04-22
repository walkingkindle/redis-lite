using Application.RDBPersistence.Contracts;
using Domain.Models;
using Domain.RDBPersistence;
using System.Text;

namespace Application.RDBPersistence.Implementations
{
    public class HexReader : IHexReader
    {
        private readonly AppArguments _args;

        public HexReader(AppArguments args)
        {
            _args = args;
        }
        public async Task<RedisMessage> ReadRedisMessage(string path)
        {

            byte[] byteMessage = File.ReadAllBytes(path);

            RedisMessage redisMessage = new RedisMessage();

            redisMessage.Header = "REDIS011";

            int offset = 0;

            RDBKeyValue keyValue = ParseKeyValue(byteMessage);
            redisMessage.RedisKeyValue = keyValue;

            return redisMessage;
        }

    public RDBKeyValue ParseKeyValue(byte[] byteMessage)
    {
        int offsetIndex = 0;

        for(int a = 0; a < byteMessage.Length; a++)
        {
            if (byteMessage[a] == 254)
            {
                offsetIndex = a;
                break;
            }

        }

        int offset = 254;
        int i = offsetIndex;

        if (byteMessage[i] != 0xFE)
            throw new Exception("Expected SELECTDB opcode (0xFE)");

        i++; 
        byte dbIndex = byteMessage[i]; 
        i++;

        if (byteMessage[i] != 0xFB) 
        throw new Exception("Expected String Value Type (0xFB)");

        i++; 
        i += 3;

        byte keyLength = byteMessage[i];
        i++;

        string key = Encoding.UTF8.GetString(byteMessage, i, keyLength);
        i += keyLength;

        byte valueLength = byteMessage[i];
        i++;

        string value = Encoding.UTF8.GetString(byteMessage, i, valueLength);
        i += valueLength;

        return new RDBKeyValue
        {
            Key = key,
            Value = value
        };
    }
    }
}
