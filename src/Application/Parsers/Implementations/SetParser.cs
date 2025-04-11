using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Endpoints;
using Domain.Interfaces;

namespace Application.Parsers.Implementations
{
    public class SetParser : ICommandParser
    {
        public string Command => "SET";

        public string Expiry => "px";

        private Result<EndpointBase> GetEndpointFromRawString(string[] lines)
        {
            bool hasExpiry = false;
            
            if(lines.Any(p=> p == Expiry || p == Expiry.ToUpperInvariant()))
            {
                hasExpiry = true;
            }
            for(int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != Command || lines[i + 2].StartsWith("$"))
                {
                    continue;
                }
                int indexOfKey = i + 2;
                int indexOfValue = indexOfKey + 2;
                SetEndpoint setEndpoint = new SetEndpoint { Input = lines[indexOfKey], Value = lines[indexOfValue] };
                if (!hasExpiry)
                {
                    return Result.Success((EndpointBase)setEndpoint);
                }
                int indexOfExpiry = indexOfValue + 4;
                int expiry;
                if (Int32.TryParse(lines[indexOfExpiry], out expiry))
                {
                    setEndpoint.Expiry = expiry;
                }
                return Result.Success((EndpointBase)setEndpoint);

            }
            return Result.Failure<EndpointBase>("Invalid");

        }

        public Result<EndpointBase> Validate(Maybe<string[]> lines)
        {

            return lines.ToResult("Lines cannot be empty")
                .Ensure(lines => lines.Any(p => p == Command), "line has to contain the SET command")
                .Ensure(lines => lines.Length >= Array.IndexOf(lines, lines.Where(p => p == Command)) + 4, "Lines have to contain to have key and value")
                .Map(lines => GetEndpointFromRawString(lines).Value);

         }
    }

}
