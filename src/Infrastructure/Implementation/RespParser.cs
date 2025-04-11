using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using System.Text;

namespace Infrastructure.Impelementations
{
    public class RespParser : IRespParser
    {
        private readonly Dictionary<string, ICommandParser> _parsers;

        public RespParser(IEnumerable<ICommandParser> parsers)
        {
            _parsers = parsers.ToDictionary(p => p.Command, p => p);
        }
        public Result<EndpointBase> ParseRespString(byte[] buffer, int readTotal)
        {
            string incomingMessage = Encoding.UTF8.GetString(buffer, 0, readTotal);

            if (string.IsNullOrEmpty(incomingMessage))
            {
                return Result.Failure<EndpointBase>("Incoming message must not be empty");
            }

            string[] lines = incomingMessage.Split(new[] { "\r\n" }, StringSplitOptions.None);

            if (lines.Length < 3)
            {
                return Result.Failure<EndpointBase>("Message format is invalid. Expected at least 3 lines.");
            }

            return ParseInternal(lines);
        }
        private Result<EndpointBase> ParseInternal(string[] lines)
        {
            var command = lines[2].ToUpperInvariant();
            lines[2] = command;

            if (_parsers.TryGetValue(command, out var parserObject) && parserObject is ICommandParser parser)
            {
                return parser.Validate(lines);
            }

            return Result.Failure<EndpointBase>("Unsupported command");
        }
    }
}
