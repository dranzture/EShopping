using System.Text;
using System.Text.Json;
using Confluent.Kafka;

namespace CheckoutService.Infrastructure.Helpers;

public static class SerializerHelper
{
    public static byte[] Serializer<T>(T data)
    {
        if (data == null)
        {
            return null;
        }

        var jsonString = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}