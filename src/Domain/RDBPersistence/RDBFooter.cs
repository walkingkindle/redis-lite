namespace Domain.RDBPersistence
{
    public class RDBFooter : RedisSubtypeBase
    {
        public override string Start => "FF";

        public string Checksum { get; set; }
    }
}
