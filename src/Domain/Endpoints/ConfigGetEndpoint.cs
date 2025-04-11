using Domain.Interfaces;

namespace Domain.Endpoints
{
    public class ConfigGetEndpoint : EndpointBase
    {
        public override string Command => "CONFIG GET";

        public override required string Input { get; set; }
    }
}
