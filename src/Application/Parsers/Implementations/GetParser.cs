using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Endpoints;
using Domain.Interfaces;

namespace Application.Parsers.Implementations
{
    public class GetParser : ICommandParser
    {
        public string Command => "GET";

        public Result<EndpointBase> GetEndpointFromRawString(string[] lines)
        {
            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i] == Command && !lines[i + 2].StartsWith("$"))
                {
                    int indexOfKey = i + 2;
                    GetEndpoint getEndpoint = new GetEndpoint {Input = lines[indexOfKey] };
                    return Result.Success((EndpointBase)getEndpoint);
                }
            }
               return Result.Failure<EndpointBase>("Invalid");
        }

        public Result<EndpointBase> Validate(Maybe<string[]> lines)
        {
            return lines.ToResult("Lines should not be null")
                .Ensure(lines => Array.Empty<string>() != lines, "lines array is empty")
                .Ensure(lines => lines.Length >= Array.IndexOf(lines, lines.Where(p => p == Command)) + 2, "Invalid command")
                .Ensure(lines => GetEndpointFromRawString(lines).IsSuccess, "Could not parse the GET command from string")
                .Map(endpoint => GetEndpointFromRawString(endpoint).Value);
       }
    }
}
