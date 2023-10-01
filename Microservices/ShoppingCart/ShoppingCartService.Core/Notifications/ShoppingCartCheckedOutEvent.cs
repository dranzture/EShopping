using MediatR;

namespace ShoppingCartService.Core.Notifications;

public class ShoppingCartCheckedOutEvent : INotification
{
    public string Username { get; }

    public ShoppingCartCheckedOutEvent(string username)
    {
        Username = username;
    }
}