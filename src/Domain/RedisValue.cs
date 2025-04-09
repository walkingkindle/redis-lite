namespace Domain.Implementations
{
    public class RedisValue
    {
        public required string Value { get; set; }
        
        public DateTime ExpirationDate { get; set; }

    }
}