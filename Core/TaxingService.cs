using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class TaxingService
    {
        private List<string> nonTaxedProducts;
        private Dictionary<TaxTypes, decimal> taxPercentages;
        public TaxingService()
        {
            nonTaxedProducts = new List<string>();
            nonTaxedProducts.Add("chocolate");
            nonTaxedProducts.Add("headache pill");
            nonTaxedProducts.Add("book");

            taxPercentages = new Dictionary<TaxTypes, decimal>();
            taxPercentages.Add(TaxTypes.BASIC_SALE_TAX, 0.1m);
            taxPercentages.Add(TaxTypes.IMPORT_TAX, 0.05m);
        }

        public void AssignTaxesTo(Product product)
        {
            if (product.Name.ToLower().Contains("imported"))
                product.AddTax(TaxTypes.IMPORT_TAX);

            if (!nonTaxedProducts.Any(ntp => product.Name.ToLower().Contains(ntp)))
                product.AddTax(TaxTypes.BASIC_SALE_TAX);
        }

        public void CalculateTaxesFor(Product product)
        {
            decimal saleTaxes = 0;

            foreach (var item in taxPercentages)
            {
                if (product.Taxes.Contains(item.Key))
                    saleTaxes += RoundUpTax(product.ShelfPrice, item.Value);
            }
            product.SetTaxes(saleTaxes);
        }

        public decimal RoundUpTax(decimal shelfPrice, decimal tax)
        {
            var saleTax = shelfPrice * tax;

            decimal decimalPart = (int)((saleTax - (int)saleTax) * 100); ;

            if (decimalPart % 10 > 5)
                saleTax = (int)(saleTax) + (Math.Ceiling(decimalPart / 10) / 10);
            else if (decimalPart % 10 < 5)
                saleTax = (int)(saleTax) + (Math.Floor(decimalPart / 10) / 10);

            return Math.Round(saleTax, 2);
        }
    }
}
