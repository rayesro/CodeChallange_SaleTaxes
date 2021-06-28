using Core;
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

        public static IEnumerable<TestCaseData> ImportedProducts()
        {
            yield return new TestCaseData(new Product("Imported box of chocolates", 5.60m), new TaxTypes[] { TaxTypes.IMPORT_TAX });
            yield return new TestCaseData(new Product("Bottle of perfume", 106.60m), new TaxTypes[] { TaxTypes.BASIC_SALE_TAX });
            yield return new TestCaseData(new Product("Imported box of chocolates", 4.49m), new TaxTypes[] { TaxTypes.IMPORT_TAX, TaxTypes.BASIC_SALE_TAX });
        }

        [Test]
        public void Given_AnImportedProduct_When_ProcessingItThroughTaxingService_Then_ProductShouldHaveAnImportedTaxAssigned()
        {
            //Arranged
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
            //Arranged
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
            //Arranged
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
