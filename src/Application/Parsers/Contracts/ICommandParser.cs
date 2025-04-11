using CSharpFunctionalExtensions;
using Domain.Interfaces;

namespace Application.Parsers.Contracts
{
    public interface ICommandParser 
    {
        public string Command { get;}
        public Result<EndpointBase> Validate(Maybe<string[]> lines);
    }
}
