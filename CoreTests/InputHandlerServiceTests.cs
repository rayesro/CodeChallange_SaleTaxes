using Domain;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace DomainTests
{
    public class InputHandlerServiceTests
    {
        InputHandlerService inputHandlerService = null;

        [SetUp]
        public void Setup()
        {
            inputHandlerService = new InputHandlerService();
        }

        public static IEnumerable<TestCaseData> ProductInputTests()
        {
            yield return new TestCaseData("1 Book at 12.49", new Product("Book", 12.49m));
            yield return new TestCaseData("1 Imported box of chocolates at 10.00", new Product("Imported box of chocolates", 10.00m));
            yield return new TestCaseData("1 Packet of headache pills at 9.75", new Product("Packet of headache pills", 9.75m));
        }

        [Test]
        [TestCaseSource(nameof(ProductInputTests))]
        public void Given_AProductDefinenOnString_When_ProcessingInput_Then_AProductIsCreated(string input, Product expectedProduct)
        {
            //Arrange

            //Act
            var productCreated = inputHandlerService.GetProductFromInput(input);
            //Assert
            Assert.AreEqual(expectedProduct.Name, productCreated.Name);
            Assert.AreEqual(expectedProduct.ShelfPrice, productCreated.ShelfPrice);
        }

        


    }
}
