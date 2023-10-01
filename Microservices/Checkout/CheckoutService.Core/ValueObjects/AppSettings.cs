namespace CheckoutService.Core.ValueObjects;

public class AppSettings
{
    public KafkaSettings? KafkaSettings { get; set; }
}

public class KafkaSettings
{
    public string? BootstrapServers { get; set; }
}