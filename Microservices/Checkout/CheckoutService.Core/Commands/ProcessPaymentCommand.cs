﻿using CheckoutService.Core.Dtos;
using CheckoutService.Core.Entities;
using CheckoutService.Core.Interfaces;

namespace CheckoutService.Core.Commands;

public class ProcessPaymentCommand : ICommand
{
    private readonly CreditCard? _creditCard;
    private readonly ShoppingCartDto _shoppingCartDto;

    public ProcessPaymentCommand(CreditCard? creditCard, ShoppingCartDto shoppingCartDto)
    {
        _creditCard = creditCard;
        _shoppingCartDto = shoppingCartDto;
    }

    public Task<bool> CanExecute()
    {
        return Task.FromResult(_creditCard.CardNumber.ToString().Length == 16 &&
                               _shoppingCartDto.ShoppingItems.Sum(e => e.TotalPrice) > 0);
    }

    public Task Execute()
    {
        Console.WriteLine($"---> Using credit card: {_creditCard.CardNumber}");
        Console.WriteLine("Processing payment");
        Console.WriteLine("Payment processed");
        return Task.CompletedTask;
    }
}