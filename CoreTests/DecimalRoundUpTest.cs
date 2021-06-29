using Application.Services;
using Domain;
using Domain.Entities;
using NUnit.Framework;
using System.Collections.Generic;

namespace DomainTests
{
    public class DecimalRoundUpTest
    {
        public static IEnumerable<TestCaseData> RoundUpTaxProducts()
        {
            //Ceil
            yield return new TestCaseData(new Product("Shiny thing",14.99m), 1.5m);
            yield return new TestCaseData(new Product("Shiny thing",5.60m), 0.6m);
            yield return new TestCaseData(new Product("Shiny thing",106.60m), 10.7m);
            //Floor
            yield return new TestCaseData(new Product("Shiny thing",4.49m), 0.4m);
            yield return new TestCaseData(new Product("Shiny thing",53.25m), 5.3m);
            yield return new TestCaseData(new Product("Shiny thing",221.40m), 22.1m);
            yield return new TestCaseData(new Product("Shiny thing",25.50m), 2.55m);

        }

        [Test]
        [TestCaseSource(nameof(RoundUpTaxProducts))]
        public void Given_ATaxForAProduct_When_RoundUpTheSaleTax_Then_SaleTaxIsRoundedUpToNearest5Cents(Product product, decimal expectedTax)
        {
            //Arrange
            var taxingService = new TaxingService();
            var tax = 0.1m;
            //Act
            var roundesUpTax = taxingService.RoundUpTax(product.ShelfPrice, tax);
            //Assert
            Assert.AreEqual(expectedTax, roundesUpTax);
        }
    }
}
