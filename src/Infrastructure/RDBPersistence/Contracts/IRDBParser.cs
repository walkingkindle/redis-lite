using Domain.RDBPersistence;

namespace Infrastructure.RDBPersistence.Contracts
{
    public interface IRDBParser
    {
        public RDBKeyValue ParseKeyValueString(byte[][] byteArr);
    }
}
