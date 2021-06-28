﻿using Core;
using Core.Entities;
using System;
using System.Collections.Generic;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> input = new List<string>();

            Console.WriteLine("Input your products...Enter x to finish");
            do
            {
                var cmdInput = Console.ReadLine();
                if (cmdInput == "X" || cmdInput == "x")
                    break;
                input.Add(cmdInput);
            } while (true);

            InputHandlerService inputHandlerService = new InputHandlerService();

            var productList = new List<Product>();
            foreach (var item in input)
            {
                var product = inputHandlerService.GetProductFromInput(item);
                if (product == null)
                    continue;
                productList.Add(product);
            }

            TaxingService taxingService = new TaxingService();
            ShoppingCart shoppingCart = new ShoppingCart();
            ReceiptService receiptService = new ReceiptService(shoppingCart);

            foreach (var product in productList)
            {
                shoppingCart.AddProduct(product);
                taxingService.AssignTaxesTo(product);
                taxingService.CalculateTaxesFor(product);
            }

            Console.WriteLine(receiptService.GetReceipt());

        }
    }
}
