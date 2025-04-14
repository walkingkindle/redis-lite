namespace Domain.RDBPersistence
{
    public class RedisDatabaseIndex : RedisSubtypeBase
    {
        public override string Start => "FE";

        public string Index { get; set; }
    }
}
