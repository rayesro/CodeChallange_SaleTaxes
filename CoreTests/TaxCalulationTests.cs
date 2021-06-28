using Core;
using NUnit.Framework;
using System.Collections.Generic;

namespace CoreTests
{
    public class TaxCalulationTests
    {

        [Test]
        public void Given_ANonTaxedProduct_When_CalculatingTaxes_Then_TotalPriceRemainsTheSame()
        {
            //Arranged
            var product = new Product(14.99m);
            //Act
            product.CalculateTax();
            //Assert
            Assert.AreEqual(14.99, product.TotalPrice);
        }

        [Test]
        public void Given_ABasicTaxProduct_When_CalculatingTaxes_Then_TotalPriceIsShelfPricePlusA10Percent()
        {
            //Arranged
            var product = new Product(14.99m);
            product.AddTax(TaxTypes.BASIC_SALE_TAX);
            //Act
            product.CalculateTax();
            //Assert
            Assert.AreEqual(16.49, product.TotalPrice);
        }

        [Test]
        public void Given_AImportTaxProduct_When_CalculatingTaxes_Then_TotalPriceIsShelfPricePlusA5Percent()
        {
            //Arranged
            var product = new Product(10.0m);
            product.AddTax(TaxTypes.IMPORT_TAX);
            //Act
            product.CalculateTax();
            //Assert
            Assert.AreEqual(10.5, product.TotalPrice);
        }

        [Test]
        public void Given_ABasicTaxAndImportTaxProduct_When_CalculatingTaxes_Then_TotalPriceIsShelfPricePlusA15Percent()
        {
            //Arranged
            var product = new Product(27.99m);
            product.AddTax(TaxTypes.IMPORT_TAX);
            product.AddTax(TaxTypes.BASIC_SALE_TAX);
            //Act
            product.CalculateTax();
            //Assert
            Assert.AreEqual(32.19, product.TotalPrice);
        }
    }
}
