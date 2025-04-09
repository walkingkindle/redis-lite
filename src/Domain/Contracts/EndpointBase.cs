namespace Domain.Interfaces
{
    public abstract class EndpointBase
    {
        public abstract string Command { get;}

        public abstract string Input { get; set; }

        public virtual string? Value { get; set; }

        public virtual int? Expiry { get; set; }
    
    }
}