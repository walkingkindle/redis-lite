using Domain.Interfaces;

namespace Domain.Endpoints
{
    public class EchoEndpoint : EndpointBase
    {
        public override string Command => "ECHO";
        public override required string Input { get; set; }
    }
}
