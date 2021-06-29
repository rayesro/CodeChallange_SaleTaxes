using Application.Interfaces.Repositories;
using Domain;
using Domain.Entities;
using Infrastructure.Memory.Repositories;
using NUnit.Framework;

namespace DomainTests
{
    public class ShoppingCartTests
    {
        IShoppingCartRepository shoppingCart = null;

        [SetUp]
        public void Setup()
        {
            shoppingCart = new ShoppingCartRepository();
        }

        [Test]
        public void Given_ShoppingCartItem_When_IncresingBy3_Then_QuantityMustBe4()
        {
            //Arrange
            var product = new Product("Imported box of chocolates", 5.60m);
            var item = new ShoppingCartItem(product);
            //Act
            item.IncreaseQuantityBy(3);
            //Assert
            Assert.AreEqual(4, item.Quantity);
        }

        [Test]
        public void Given_ShoppingCartItem_When_IncresingBy3Negative_Then_QuantityMustBe1()
        {
            //Arrange
            var product = new Product("Imported box of chocolates", 5.60m);
            var item = new ShoppingCartItem(product);
            //Act
            item.IncreaseQuantityBy(-3);
            //Assert
            Assert.AreEqual(1, item.Quantity);
        }

        [Test]
        public void Given_AProduct_When_AddingItToShoppingCart_Then_ItemListShouldBe1()
        {
            /*expected format
                ProductName: TotalProductPrice
                Sales Taxes: TotalSaleTaxes
                Total: Sum of TotalProductPrice and TotalSaleTaxes
            */

            var product = new Product("Music CD", 14.99m);

            shoppingCart.AddProductAsync(product);
            var list = shoppingCart.GetShoppingCartListAsync().Result;

            Assert.AreEqual(1, list.Count);
        }


    }
}
