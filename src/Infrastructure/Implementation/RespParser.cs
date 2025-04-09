using Application.Parsers.Contracts;
using CSharpFunctionalExtensions;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using System.Text;
using System.Threading.Tasks;

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
            Console.WriteLine($"Received incoming message : {incomingMessage}");
            string[] lines = incomingMessage.Split(Environment.NewLine);

            return ParseInternal(lines);
        }

        private Result<EndpointBase> ParseInternal(string[] lines)
        {
            var command = lines[2].ToUpperInvariant();

            if (lines[2].Contains("PING"))
            {
                command = "PING";
            }

            lines[2] = command;

            if(_parsers.TryGetValue(command, out var parserObject) && parserObject is ICommandParser parser)
            {
                return parser.Validate(lines);
            }

            return Result.Failure<EndpointBase>("Unsupported command");
        }
    }
}
