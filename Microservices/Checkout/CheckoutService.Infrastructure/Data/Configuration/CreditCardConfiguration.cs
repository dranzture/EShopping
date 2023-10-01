using CheckoutService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CheckoutService.Infrastructure.Data.Configuration;

public class CreditCardConfiguration : IEntityTypeConfiguration<CreditCard>
{
    public void Configure(EntityTypeBuilder<CreditCard> builder)
    {
        builder.HasIndex(e => e.CardNumber);
        builder.HasOne<PaymentMethod>()
            .WithMany(e => e.CreditCards)
            .HasForeignKey(e => e.Username);
    }
}