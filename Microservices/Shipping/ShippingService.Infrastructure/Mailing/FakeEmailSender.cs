using ShippingService.Core.Interfaces;

namespace ShippingService.Infrastructure.Mailing;

public class FakeEmailSender : IEmailSender
{
  public Task SendEmailAsync(string to, string from, string subject, string body)
  {
    return Task.CompletedTask;
  }
}
