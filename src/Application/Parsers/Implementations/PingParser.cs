using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Endpoints;
using Domain.Interfaces;

namespace Application.Parsers.Implementations
{
    public class PingParser : ICommandParser
    {
        public string Command => "PING";

        private Result<EndpointBase> GetEndpointFromRawString(string[] lines)
        {
           for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == Command)
                {
                    return new PingEndpoint { Input = "PING" };
                }
            }

            return Result.Failure<EndpointBase>("Invalid command");
        }

        public Result<EndpointBase> Validate(Maybe<string[]> lines)
        {
            return lines.ToResult("Lines cannot be empty")
                .Ensure(lines => lines[Array.IndexOf(lines, lines.Where(p => p == Command).First())] == Command, "PING command invalid")
                .Map(lines => GetEndpointFromRawString(lines).Value);
        }
       }
}
