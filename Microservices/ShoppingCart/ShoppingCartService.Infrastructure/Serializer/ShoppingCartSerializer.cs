using System.Text;
using System.Text.Json;
using Confluent.Kafka;
using ShoppingCartService.Core.Entities;

public class ShoppingCartSerializer : ISerializer<ShoppingCart>
{
    public byte[] Serialize(ShoppingCart data, SerializationContext context)
    {
        if (data == null)
        {
            return null;
        }

        var jsonString = JsonSerializer.Serialize(data);
        return Encoding.UTF8.GetBytes(jsonString);
    }
}