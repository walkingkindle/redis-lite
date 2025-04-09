using Domain.Interfaces;

namespace Domain.Endpoints
{
    public class PingEndpoint : EndpointBase
    {
        public override string Command => "PING";

        public override required string Input { get; set; }
    }
}