using Application.Interface.Services;
using Application.Services;
using Domain;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace DomainTests
{
    public class ReceiptTests
    {
        ReceiptService receiptService = null;
        ShoppingCart shoppingCart = null;
        ITaxingService taxingService = null;

        [SetUp]
        public void Setup()
        {
            shoppingCart = new ShoppingCart();
            taxingService = new TaxingService();
            receiptService = new ReceiptService(shoppingCart);
        }

        [Test]
        public void Given_AProductWithTax_When_PrintingAReceipt_Then_OutputWithFormat_Product_SaleTaxes_TotalPrice()
        {
            /*expected format
                ProductName: TotalProductPrice
                Sales Taxes: TotalSaleTaxes
                Total: Sum of TotalProductPrice and TotalSaleTaxes
            */
            
            var product = new Product("Music CD", 14.99m);
            shoppingCart.AddProduct(product);
            taxingService.AssignTaxesTo(product);
            taxingService.CalculateTaxesFor(product);
            
            var receipt = receiptService.GetReceipt();
            
            StringAssert.Contains("Music CD: 16.49", receipt);
            StringAssert.Contains("Sales Taxes: 1.50", receipt);
            StringAssert.Contains("Total: 16.49", receipt);
        }

        [Test]
        public void Given_TwoSameProductWithNoTaxes_When_PrintingAReceipt_Then_OutputWithFormat_Product_SaleTaxes_TotalPrice()
        {
            /*expected format
                ProductName: TotalProductsPrice (ProductQuantity @ ShelfPrice)
                Sales Taxes: TotalSaleTaxes
                Total: Sum of TotalProductPrice and TotalSaleTaxes
            */
            
            var product1 = new Product("Book", 12.49m);
            var product2 = new Product("Book", 12.49m);

            
            shoppingCart.AddProduct(product1);
            shoppingCart.AddProduct(product2);

            taxingService.AssignTaxesTo(product1);
            taxingService.CalculateTaxesFor(product1);

            taxingService.AssignTaxesTo(product2);
            taxingService.CalculateTaxesFor(product2);


            var receipt = receiptService.GetReceipt();
            
            StringAssert.Contains("Book: 24.98 (2 @ 12.49)", receipt);
            StringAssert.Contains("Sales Taxes: 0", receipt);
            StringAssert.Contains("Total: 24.98", receipt);
        }


        public static IEnumerable<TestCaseData> ShoppingCartLists()
        {
            yield return new TestCaseData(
                new List<Product>()
                {
                    new Product("Book",           12.49m),
                    new Product("Book",           12.49m),
                    new Product("Music CD",       14.99m),
                    new Product(" Chocolate bar", 0.85m),
                },
                new List<string>()
                {
                    "Book: 24.98 (2 @ 12.49)",
                    "Music CD: 16.49",
                    "Chocolate bar: 0.85",
                    "Sales Taxes: 1.50",
                    "Total: 42.32"
                }
                );

            yield return new TestCaseData(
                new List<Product>()
                {
                    new Product("Imported box of chocolates", 10.00m),
                    new Product("Imported bottle of perfume", 47.50m)
                },
                new List<string>()
                {
                    "Imported box of chocolates: 10.50",
                    "Imported bottle of perfume: 54.65",
                    "Sales Taxes: 7.65",
                    "Total: 65.15"
                }
                );

            yield return new TestCaseData(
                new List<Product>()
                {
                    new Product("Imported bottle of perfume", 27.99m),
                    new Product("Bottle of perfume", 18.99m),
                    new Product(" Packet of headache pills", 9.75m),
                    new Product("Imported box of chocolates", 11.25m),
                    new Product("Imported box of chocolates", 11.25m),
                },
                new List<string>()
                {
                    "Imported bottle of perfume: 32.19",
                    "Bottle of perfume: 20.89",
                    "Imported box of chocolates: 23.70 (2 @ 11.85)",
                    "Sales Taxes: 7.30",
                    "Total: 86.53"
                }
                );
        }

        [Test]
        [TestCaseSource(nameof(ShoppingCartLists))]
        public void Given_AProductList_When_PrintingAReceipt_Then_ReceiptShouldMatchExpectedReceipt(List<Product> list, List<string> expectedItemsOnReceipt)
        {
            /*expected format
                ProductName: TotalProductsPrice (ProductQuantity @ ShelfPrice)
                Sales Taxes: TotalSaleTaxes
                Total: Sum of TotalProductPrice and TotalSaleTaxes
            */
            
            foreach (var item in list)
            {
                shoppingCart.AddProduct(item);
                taxingService.AssignTaxesTo(item);
                taxingService.CalculateTaxesFor(item);
            }

            

            
            var receipt = receiptService.GetReceipt();
            
            foreach (var item in expectedItemsOnReceipt)
            {
                StringAssert.Contains(item, receipt);
            }
        }

    }
}
