using Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace CoreTests
{
    public class TaxCalulationTests
    {
        TaxingService taxingService = null;

        [SetUp]
        public void Setup()
        {
            taxingService = new TaxingService();
        }

        [Test]
        public void Given_ANonTaxedProduct_When_CalculatingTaxes_Then_TotalPriceRemainsTheSame()
        {
            //Arrange
            var product = new Product("Music CD", 14.99m);
            //Act
            taxingService.CalculateTaxes(product);
            //Assert
            Assert.AreEqual(14.99, product.TotalPrice);
        }

        [Test]
        public void Given_ABasicTaxProduct_When_CalculatingTaxes_Then_TotalPriceIsShelfPricePlusA10Percent()
        {
            //Arrange
            var product = new Product("Music CD", 14.99m);
            product.AddTax(TaxTypes.BASIC_SALE_TAX);
            //Act
            taxingService.CalculateTaxes(product);
            //Assert
            Assert.AreEqual(16.49, product.TotalPrice);
        }

        [Test]
        public void Given_AImportTaxProduct_When_CalculatingTaxes_Then_TotalPriceIsShelfPricePlusA5Percent()
        {
            //Arrange
            var product = new Product("Music CD", 10.0m);
            product.AddTax(TaxTypes.IMPORT_TAX);
            //Act
             taxingService.CalculateTaxes(product);
            //Assert
            Assert.AreEqual(10.5, product.TotalPrice);
        }

        [Test]
        public void Given_ABasicTaxAndImportTaxProduct_When_CalculatingTaxes_Then_TotalPriceIsShelfPricePlusA15Percent()
        {
            //Arrange
            var product = new Product("Music CD", 27.99m);
            product.AddTax(TaxTypes.IMPORT_TAX);
            product.AddTax(TaxTypes.BASIC_SALE_TAX);
            //Act
             taxingService.CalculateTaxes(product);
            //Assert
            Assert.AreEqual(32.19, product.TotalPrice);
        }
    }
}
