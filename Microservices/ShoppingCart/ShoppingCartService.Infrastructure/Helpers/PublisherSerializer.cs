using System.Text;
using System.Text.Json;

namespace ShoppingCartService.Infrastructure.Helpers;

public static class PublisherHelpers
{
    public static byte[] PublishSerializationHelper<T>(this T data)
    {
        var jsonString = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}