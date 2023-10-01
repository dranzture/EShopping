using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using Confluent.Kafka;

namespace CheckoutService.Infrastructure.Serializer;

public class ShoppingCartSerializer : ISerializer<ShoppingCartDto>
{
    public byte[] Serialize(ShoppingCartDto data, SerializationContext context)
    {
       return Helpers.SerializerHelper.Serializer(data);
    }
}