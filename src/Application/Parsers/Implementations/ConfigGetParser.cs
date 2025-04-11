using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Endpoints;
using Domain.Interfaces;

namespace Application.Parsers.Implementations
{
    public class ConfigGetParser : ICommandParser
    {
        public string Command => "CONFIG";

        private Result<EndpointBase> GetEndpointFromRawString(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == Command && !lines[i + 4].StartsWith("$"))
                {
                    int indexOfInput = i + 4;
                    ConfigGetEndpoint getEndpoint = new ConfigGetEndpoint { Input = lines[indexOfInput] };
                    return Result.Success((EndpointBase)getEndpoint);
                }
            }
            return Result.Failure<EndpointBase>("Could not parse the string");
        }
        //this definately needs some refactoring runnign 2 loops here.

        public Result<EndpointBase> Validate(Maybe<string[]> lines)
        {
            return lines.ToResult("Lines cannot be empty")
                .Ensure(lines => lines.Any(p => p.ToUpperInvariant() == Command), "Invalid command syntax")
                .Ensure(lines => lines.Any(p => p.ToUpperInvariant() == "GET"), "Invalid command syntax")
                .Ensure(lines => GetEndpointFromRawString(lines).IsSuccess, "Could not parse the string")
                .Map(lines => GetEndpointFromRawString(lines).Value);
        }
    }
}
