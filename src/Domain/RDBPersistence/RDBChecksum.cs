namespace Domain.RDBPersistence
{
    public class RDBChecksum : RedisSubtypeBase
    {
        public override string Start => "FF";

        public byte[] Checksum { get; set; }
    }
}
