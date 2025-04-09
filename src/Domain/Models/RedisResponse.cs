namespace Domain.Interfaces
{
public class RedisResponse
{
    public RedisResponseType Type { get; }
    public object? Value { get; }

    private RedisResponse(RedisResponseType type, object? value)
    {
        Type = type;
        Value = value;
    }

    // Factory methods for different RESP types
    public static RedisResponse Simple(string message) =>
        new RedisResponse(RedisResponseType.SimpleString, message);

    public static RedisResponse Error(string message) =>
        new RedisResponse(RedisResponseType.Error, message);

    public static RedisResponse Integer(long number) =>
        new RedisResponse(RedisResponseType.Integer, number);

    public static RedisResponse Bulk(string message) =>
        new RedisResponse(RedisResponseType.BulkString, message);

    public static RedisResponse NullBulk() =>
        new RedisResponse(RedisResponseType.NullBulk, null);

    public static RedisResponse Array(IEnumerable<RedisResponse> items) =>
        new RedisResponse(RedisResponseType.Array, items.ToList());

    public static RedisResponse NullArray() =>
        new RedisResponse(RedisResponseType.NullArray, null);
}
public enum RedisResponseType
{
    SimpleString, // +OK\r\n
    Error,        // -ERR\r\n
    Integer,      // :123\r\n
    BulkString,   // $6\r\nfoobar\r\n
    NullBulk,     // $-1\r\n
    Array,        // *2\r\n...
    NullArray     // *-1\r\n
}


}