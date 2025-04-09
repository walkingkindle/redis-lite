using Domain.Interfaces;

namespace Domain.Endpoints
{
    public class GetEndpoint : EndpointBase
    {
        public override string Command => "GET";

        public override required string Input { get; set; }
    }
}
