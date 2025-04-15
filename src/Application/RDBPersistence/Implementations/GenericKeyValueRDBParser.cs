using Application.RDBPersistence.Contracts;
using Domain.RDBPersistence;
using System.Text;

namespace Application.RDBPersistence.Implementations
{
    public class GenericKeyValueRDBParser : IGenericKeyValueRDBParser
    {
        public string Parse(byte[] byteArr, int length)
        {
            return Encoding.UTF8.GetString(byteArr.Skip(1).ToArray(),0, length);
        }
    }
}
