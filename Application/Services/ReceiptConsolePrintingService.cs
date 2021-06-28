﻿using Application.Interfaces.Services;
using Domain.Entities;
using System.Text;

namespace Application.Services
{
    public class ReceiptConsolePrintingService : IReceiptPrintingService<string>
    {
        public ShoppingCart _shoppingCart;

        public ReceiptConsolePrintingService(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public string PrintReceipt()
        {
            StringBuilder sb = new StringBuilder();

            decimal total = 0;
            decimal saleTaxes = 0;
            foreach (var cartItem in _shoppingCart.ItemList)
            {
                var currentTotal = cartItem.Item.TotalPrice * cartItem.Quantity;

                sb.Append($"{cartItem.Item.Name}: {currentTotal:0.00}");
                if (cartItem.Quantity > 1)
                    sb.Append($" ({cartItem.Quantity} @ {cartItem.Item.ShelfPrice + cartItem.Item.SaleTax:0.00})");

                sb.AppendLine();

                total += currentTotal;
                saleTaxes += cartItem.Item.SaleTax * cartItem.Quantity;
            }
            sb.AppendLine($"Sales Taxes: {saleTaxes:0.00}");
            sb.Append($"Total: {total:0.00}");

            return sb.ToString();
        }
    }
}
