using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class TaxingService
    {
        private List<string> nonTaxedProducts;
        public TaxingService()
        {
            nonTaxedProducts = new List<string>();
            nonTaxedProducts.Add("chocolate");
            nonTaxedProducts.Add("headache pill");
            nonTaxedProducts.Add("book");
        }

        public void AssignTaxesTo(Product product)
        {
            if (product.Name.ToLower().Contains("imported"))
                product.AddTax(TaxTypes.IMPORT_TAX);

            if (!nonTaxedProducts.Any(ntp => product.Name.ToLower().Contains(ntp)))
                product.AddTax(TaxTypes.BASIC_SALE_TAX);

        }
    }
}
