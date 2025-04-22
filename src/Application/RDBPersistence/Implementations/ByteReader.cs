using Application.RDBPersistence.Contracts;
namespace Application.RDBPersistence.Implementations
{
    public class ByteReader : IByteReader
    {
        private readonly byte[] _buffer;
        private int _offset;

        public ByteReader(byte[] buffer)
        {
            _buffer = buffer;
            _offset = 0;
        }

        public int Offset => _offset;

        public byte ReadByte()
        {
            if (_offset >= _buffer.Length)
                throw new InvalidOperationException("Attempt to read past end of buffer.");
            return _buffer[_offset++];
        }

        public byte PeekByte()
        {
            if (_offset >= _buffer.Length)
                throw new InvalidOperationException("Attempt to peek past end of buffer.");
            return _buffer[_offset];
        }

        public byte[] ReadBytes(int count)
        {
            if (_offset + count > _buffer.Length)
                throw new InvalidOperationException("Attempt to read past end of buffer.");
            var result = _buffer.Skip(_offset).Take(count).ToArray();
            _offset += count;
            return result;
        }

        public bool HasMore => _offset < _buffer.Length;
        public async Task<byte[]> ReadBytesFromFile(string path)
        {
            return await File.ReadAllBytesAsync(path);
        }
    }
}
