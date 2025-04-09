using Domain.Interfaces;

namespace Domain.Endpoints
{
    public class SetEndpoint : EndpointBase
    {
        public override string Command => "SET";
        public override required string Input { get; set; }
        public override string? Value { get; set; }
    }
}
