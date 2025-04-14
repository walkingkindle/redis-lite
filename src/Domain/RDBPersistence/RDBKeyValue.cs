namespace Domain.RDBPersistence
{
    public class RDBKeyValue : RedisSubtypeBase
    {
        public override string Start => "00"; //assume that the type is always string for now

        public string Key { get; set; }

        public string Value { get; set; }
    }
}
