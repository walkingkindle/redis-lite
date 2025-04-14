using Domain.Interfaces;

namespace Domain.Endpoints
{
    public class KeysEndpoint : EndpointBase
    {
        public override string Command => "KEYS";

        public override required string Input { get; set; }
    }
}
