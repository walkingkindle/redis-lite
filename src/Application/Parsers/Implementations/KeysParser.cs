using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Endpoints;
using Domain.Interfaces;

namespace Application.Parsers.Implementations
{
    public class KeysParser : ICommandParser
    {
        public string Command => "KEYS";

        public Result<EndpointBase> Validate(Maybe<string[]> lines)
        {
           return lines.ToResult("lines cannot be empty")
                .Ensure(lines => lines.Length >= 2, "Wrong line size")
                .Ensure(lines => GetInputFromString(lines).IsSuccess, "could not parse the input from the endpoint")
                .Map(lines => GetInputFromString(lines).Value);
        }

        private Result<EndpointBase> GetInputFromString(string[] lines)
        {
            for(int i =0; i < lines.Length; i++)
            {
                if (lines[i] == Command)
                {
                    //int length;
                    //Int32.TryParse(lines[i + 1].Replace("$", ""),out length);

                    //TODO Refactor this and all other parsers to parse only the string value from the lenght, like $3 will parse 3 characters as a key or value.
                    KeysEndpoint keysEndpoint = new KeysEndpoint { Input = lines[i + 2] };
                    return Result.Success((EndpointBase)keysEndpoint);
                }
            }
            return Result.Failure<EndpointBase>("Could not parse the endpoint string");
        }
    }
}
