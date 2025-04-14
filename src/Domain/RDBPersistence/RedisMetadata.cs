namespace Domain.RDBPersistence{
    public class RedisMetadata : RedisSubtypeBase
    {
        public override string Start => "FA";

        public string AttributeName { get; set; }

        public string AttributeValue { get; set; }
    }
}