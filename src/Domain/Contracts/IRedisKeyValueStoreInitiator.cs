namespace Domain.Contracts
{
    public interface IRedisKeyValueStoreInitiator
    {
        public Task FillDictionary();
    }
}
