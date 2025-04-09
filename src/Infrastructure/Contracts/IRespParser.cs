using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Infrastructure.Interfaces
{
    public interface IRespParser
    {
        public Result<EndpointBase> ParseRespString(byte[] buffer, int readTotal);
    }
}
