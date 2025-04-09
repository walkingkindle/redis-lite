using CSharpFunctionalExtensions;
using Domain.Contracts;
using Domain.Interfaces;
using System.Text;

namespace Application
{
    public class ResponseBuilder : IResponseBuilder
    {
        public string BuildRedisResponse(Result<RedisResponse> result)
        {
        var builder = new StringBuilder();

        if (result.IsFailure)
        {
            builder.Append($"-ERR {result.Error}\r\n");
            return builder.ToString();
        }

        var response = result.Value;

        switch (response.Type)
        {
            case RedisResponseType.SimpleString:
                builder.Append($"+{response.Value}\r\n");
                break;

            case RedisResponseType.Error:
                builder.Append($"-ERR {response.Value}\r\n");
                break;

            case RedisResponseType.Integer:
                builder.Append($":{response.Value}\r\n");
                break;

            case RedisResponseType.BulkString:
                var str = (string?)response.Value ?? "";
                builder.Append($"${str.Length}\r\n{str}\r\n");
                break;

            case RedisResponseType.NullBulk:
                builder.Append($"$-1\r\n");
                break;

            case RedisResponseType.Array:
                var items = (List<RedisResponse>)response.Value!;
                builder.Append($"*{items.Count}\r\n");
                foreach (var item in items)
                {
                    builder.Append(BuildRedisResponse(Result.Success(item)));
                }
                break;

            case RedisResponseType.NullArray:
                builder.Append("*-1\r\n");
                break;

            default:
                builder.Append($"-ERR Unknown response type\r\n");
                break;
        }
        return builder.ToString();
    }
  }
}
