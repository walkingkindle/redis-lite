namespace Domain.RDBPersistence
{
    public class RedisDatabaseIndex : RedisSubtypeBase
    {
        public override string Start => "FE";

        public int Index { get; set; }
    }
}
