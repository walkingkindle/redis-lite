using Domain.RDBPersistence;

namespace Application.RDBPersistence.Contracts
{
    public interface IGenericKeyValueRDBParser
    {
        public string Parse(byte[] byteArr, int length);
    }
}
