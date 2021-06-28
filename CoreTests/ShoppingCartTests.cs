using Domain;
using Domain.Entities;
using NUnit.Framework;

namespace DomainTests
{
    public class ShoppingCartTests
    {

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


    }
}
