using CheckoutService.Core.Dtos;
using Confluent.Kafka;

namespace CheckoutService.Infrastructure.Serializer;

public class OrderSerializer : ISerializer<OrderDto>
{
    public byte[] Serialize(OrderDto data, SerializationContext context)
    {
        return Helpers.SerializerHelper.Serializer(data);
    }
}