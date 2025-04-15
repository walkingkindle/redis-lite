namespace Domain.RDBPersistence{
    public class RedisMessage
    {
        public string Header { get; set; } = "REDIS0011";

        public RedisMetadata RedisMetadata { get; set; }

        public RDBSizeInformation RedisSizeInformation { get; set; }

        public RedisDatabaseIndex RedisDatabaseIndex { get; set; }

        public RDBKeyValue RedisKeyValue { get; set; }

        public RDBChecksum RedisChecksum { get; set; }
    }

}
