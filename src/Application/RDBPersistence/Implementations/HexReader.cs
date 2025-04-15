using Application.RDBPersistence.Contracts;
using Domain.Models;
using Domain.RDBPersistence;

namespace Application.RDBPersistence.Implementations
{
    public class HexReader : IHexReader
    {
        private readonly AppArguments _args;

        private readonly IGenericKeyValueRDBParser _genericParser;

        public HexReader(AppArguments args, IGenericKeyValueRDBParser genericParser)
        {
            _args = args;
            _genericParser = genericParser;
        }
        public async Task<RedisMessage> ReadRedisMessage()
        {
            string path = $"{_args.Dir}/{_args.DbFileName}";

            byte[] byteMessage = await File.ReadAllBytesAsync(path);

            string[] stringByteMessage = byteMessage.Select(b => b.ToString($"{b:X2}")).ToArray();

            RedisMessage redisMessage = new RedisMessage();

            redisMessage.Header = "REDIS011";

            byteMessage = byteMessage.Skip(9).ToArray();

            int offset = 0;

            while(offset < byteMessage.Length)
            {
                byte current = byteMessage[offset];

                if(current == 0xfa)
                {
                    (string metadata, int bytesConusmed) = ParseValue(byteMessage, offset);

                    redisMessage.RedisMetadata = new RedisMetadata { Value = metadata };

                    offset += bytesConusmed;
                    continue;
                }

                if(current == 0xfe)
                {
                    var (index, bytesConsumed) = ParseValue(byteMessage, offset);

                    redisMessage.RedisDatabaseIndex = new RedisDatabaseIndex { Index = Int32.Parse(index) };

                    offset += bytesConsumed;

                    continue;
                }

                if(current == 0x00)
                {
                    var (key,value, bytesConsumed) = ParseKeyValue(byteMessage, offset);

                    redisMessage.RedisKeyValue = new RDBKeyValue { Key = key, Value = value };

                    offset += bytesConsumed;

                    continue;
                }

                if (current == 0xff)
                {
                    if (offset + 8 < byteMessage.Length)
                    {
                        byte[] checksum = byteMessage.Skip(offset + 1).Take(8).ToArray();
                        redisMessage.RedisChecksum = new RDBChecksum { Checksum = checksum }; // assuming RedisMessage has a `Checksum` property

                    }
                    break;
                }
                }

            return redisMessage;
            }

        private (string key, string value, int bytesConsumed) ParseKeyValue(byte[] byteMessage, int offset)
        {
            int start = offset;
            offset++;

            (string key, int keyOffset) = ParseValue(byteMessage, offset);

            offset += keyOffset;

            (string value, int valueOffset) = ParseValue(byteMessage, offset);
            offset += valueOffset;


            return (key, value, offset - start);
        }

        private (string metadata, int offset) ParseValue(byte[] byteMessage, int offset)
        {
            //for now assume that the first element determines length

            int start = offset;

            offset++;

            int length = DetermineLength(byteMessage[offset]);

            string encodedValue = _genericParser.Parse(byteMessage, length );

            offset += length;
                    
            return (encodedValue, offset - start);


            //what if here _parse returns an exception??
        }

        private int DetermineLength(byte lengthByte)
        {
            return lengthByte & 0x3F; //for now just 6 bit, later extend le
        }
    }
}
