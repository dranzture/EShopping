using System.Text;
using System.Text.Json;

namespace OrderService.Infrastructure.Helpers;

public static class PublisherHelpers
{
    public static byte[] PublishSerializationHelper<T>(T data)
    {
        if (data == null) return null;
        var jsonString = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}