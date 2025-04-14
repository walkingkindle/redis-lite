using CSharpFunctionalExtensions;
using Domain.RDBPersistence;

namespace Infrastructure.RDBPersistence.Contracts
{
    public interface IHexReader
    {
        Task<Result<RedisMessage>> ReadRedisMessage(byte[] redisByteMessage);
    }
}