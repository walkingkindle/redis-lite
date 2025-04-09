using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Endpoints;
using Domain.Interfaces;

namespace Application.Parsers.Implementations
{
    public class EchoParser : ICommandParser
    {
        public string Command => "ECHO";

        public Result<EndpointBase> GetEndpointFromRawString(string[] lines)
        {
            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == Command && !lines[i + 2].StartsWith("$"))
                {
                    int indexOfKey = i + 2;
                    EchoEndpoint setEndpoint = new EchoEndpoint {Input = lines[indexOfKey] };
                    return Result.Success((EndpointBase)setEndpoint);
                }
            }
               return Result.Failure<EndpointBase>("Invalid");
        }

        public Result<EndpointBase> Validate(Maybe<string[]> lines)
        {
            return lines.ToResult("Lines cannot be empty")
                .Ensure(lines => lines.Length >= Array.IndexOf(lines, lines.Where(p => p == Command)) + 2, "Invalid command supplied")
                .Map(lines => GetEndpointFromRawString(lines).Value);
        }

    }
}
