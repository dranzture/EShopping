using CheckoutService.Core.Entities;
using Confluent.Kafka;

namespace CheckoutService.Infrastructure.Serializer;

public class ShoppingCartSerializer : ISerializer<ShoppingCart>
{
    public byte[] Serialize(ShoppingCart data, SerializationContext context)
    {
       return Helpers.SerializerHelper.Serializer(data);
    }
}