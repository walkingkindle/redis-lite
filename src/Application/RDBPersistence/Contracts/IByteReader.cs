using CSharpFunctionalExtensions;

namespace Application.RDBPersistence.Contracts
{
    public interface IByteReader
    {
        public bool HasMore { get; }
        public Task<byte[]> ReadBytesFromFile(string path);

        public byte ReadByte();

        public byte PeekByte();

        public byte[] ReadBytes(int count);



    }
}
