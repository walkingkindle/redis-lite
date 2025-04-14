namespace Domain.RDBPersistence
{
    public class RDBSizeInformation : RedisSubtypeBase
    {
        public override string Start => "FB";

        public int AmountOfKeyValuePairs { get; set; }

        public int AmountOfKeyValuePairsWithExpiry { get; set; }
    }
}
