namespace Domain.RDBPersistence{
    public class RedisMetadata : RedisSubtypeBase
    {
        public override string Start => "FA";
        public required string Value { get; set; }
    }
}