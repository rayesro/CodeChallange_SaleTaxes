using Core;
using Core.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace CoreTests
{
    public class TaxingTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Given_AnImportedProduct_When_ProcessingItThroughTaxingService_Then_ProductShouldHaveAnImportedTaxAssigned()
        {
            //Arrange
            var taxService = new TaxingService();
            var product = new Product("Imported box of chocolates", 5.60m);
            //Act
            taxService.AssignTaxesTo(product);
            //Assert
            Assert.Contains(TaxTypes.IMPORT_TAX, product.Taxes);
        }

        [Test]
        public void Given_ANorBookNorFoodNorMedPropProduct_When_ProcessingItThroughTaxingService_Then_ProductShouldHaveAnBasicSaleTaxAssigned()
        {
            //Arrange
            var taxService = new TaxingService();
            var product = new Product("Music CD", 5.60m);
            //Act
            taxService.AssignTaxesTo(product);
            //Assert
            Assert.Contains(TaxTypes.BASIC_SALE_TAX, product.Taxes);
        }

        [Test]
        public void Given_ANorBookNorFoodNorMedPropImportedProduct_When_ProcessingItThroughTaxingService_Then_ProductShouldHaveAnBasicSaleTaxAssigned()
        {
            //Arrange
            var taxService = new TaxingService();
            var product = new Product("Imported Music CD", 5.60m);
            //Act
            taxService.AssignTaxesTo(product);
            //Assert
            Assert.Contains(TaxTypes.BASIC_SALE_TAX, product.Taxes);
            Assert.Contains(TaxTypes.IMPORT_TAX, product.Taxes);
        }
    }
}
