using CSharpFunctionalExtensions;
using Domain.RDBPersistence;
using Infrastructure.RDBPersistence.Contracts;

namespace Infrastructure.RDBPersistence.Implementations
{
    public class HexReader : IHexReader
    {
        public async Task<Result<RedisMessage>> ReadRedisMessage(byte[] byteMessage)
        {
            throw new NotImplementedException();
        }
    }
}
