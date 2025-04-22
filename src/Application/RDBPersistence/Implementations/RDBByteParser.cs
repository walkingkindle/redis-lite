using Application.RDBPersistence.Contracts;
using Domain.RDBPersistence;
using System.Text;

namespace Application.RDBPersistence.Implementations
{
    public class RDBByteParser : IRDBByteParser
    {
        public RedisMessage ParseRDBFile(byte[] fullFile)
        {
            var reader = new ByteReader(fullFile);
            var message = new RedisMessage();

            // Skip header (REDIS000x)
            reader.ReadBytes(9);

            List<RDBKeyValue> redisKeyValue = new();

            while (reader.HasMore)
            {
                byte opcode = reader.ReadByte();

                switch (opcode)
                {
                    case 0xFA:
                        string metadata = ParseString(reader);
                        message.RedisMetadata = new RedisMetadata { Value = metadata };
                        break;

                    case 0xFE:
                        string dbIndex = ParseString(reader);
                        message.RedisDatabaseIndex = new RedisDatabaseIndex { Index = !string.IsNullOrEmpty(dbIndex) ? int.Parse(dbIndex) : 0 };
                        break;

                    case 0x00:
                        if (reader.PeekByte() == 0)
                        {
                            reader.ReadByte();
                        }
                        var keyValue = ParseKeyValue(reader);
                        redisKeyValue.Add(keyValue);
                        break;

                    case 0xFF:
                        var checksum = reader.ReadBytes(8);
                        message.RedisChecksum = new RDBChecksum { Checksum = checksum };
                        break;
                    default:
                        continue;
                }
            }
            message.RedisKeyValue = redisKeyValue;

            return message;

        }

        private string ParseString(ByteReader reader)
        {
            byte lengthByte = reader.ReadByte();
            int length = DetermineLength(lengthByte); // your logic (e.g., lengthByte & 0x3F)
            byte[] stringBytes = reader.ReadBytes(length);
            return Encoding.UTF8.GetString(stringBytes);
        }

        private int DetermineLength(byte lengthByte)
        {
            return lengthByte & 0x3F;
        }

        private RDBKeyValue ParseKeyValue(ByteReader reader)
        {
            string key = ParseLengthPrefixedString(reader);

            // Parse value
            string value = ParseLengthPrefixedString(reader);

            return new RDBKeyValue { Key = key, Value = value };
        }

        private string ParseLengthPrefixedString(ByteReader reader)
        {
            byte lengthByte = reader.ReadByte();
            int length = DetermineLength(lengthByte);
            byte[] stringBytes = reader.ReadBytes(length);
            return Encoding.UTF8.GetString(stringBytes);
        }


    }
}

