using CSharpFunctionalExtensions;
using Domain.RDBPersistence;

namespace Application.RDBPersistence.Contracts
{
    public interface IHexReader
    {
       Task<RedisMessage> ReadRedisMessage();
    }
}