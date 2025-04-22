using Domain.RDBPersistence;

namespace Application.RDBPersistence.Contracts
{
    public interface IRDBByteParser
    {
        RedisMessage ParseRDBFile(byte[] fullFile);
    }
}
