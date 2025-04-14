using Domain.RDBPersistence;
using Infrastructure.RDBPersistence.Contracts;

namespace Infrastructure.RDBPersistence.Implementations
{
    public class RDBParser : IRDBParser
    {
        public RDBKeyValue ParseKeyValueString(byte[][] byteArr)
        {
            byte[] keyArr = byteArr[1];

            byte[] valueArr = byteArr[2];

            int keyLength = DetermineLength(keyArr[0]);

            int valueLenght = DetermineLength(keyArr[1]);

            return new RDBKeyValue { Key = "", Value = "" };

        }

        private int DetermineLength(byte v)
        {
            throw new NotImplementedException();
        }
    }
}
