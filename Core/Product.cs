using System;
using System.Collections.Generic;

namespace Core
{
    public class Product
    {
        public decimal ShelfPrice { get; private set; }
        public decimal SaleTax { get; private set; }
        public List<TaxTypes> Taxes { get; private set; }
        public decimal TotalPrice => ShelfPrice + SaleTax;
        public Product( decimal shelfPrice)
        {
            ShelfPrice = shelfPrice;
            Taxes = new List<TaxTypes>();
        }

        public void AddTax(TaxTypes taxType)
        {
            Taxes.Add(taxType);
        }

        public void CalculateTax()
        {
            SaleTax = 0;
            if (Taxes.Contains(TaxTypes.BASIC_SALE_TAX))
                SaleTax += RoundUpTaxes(0.1m);
            if (Taxes.Contains(TaxTypes.IMPORT_TAX))
                SaleTax += RoundUpTaxes(0.05m);
        }

        public decimal RoundUpTaxes(decimal tax)
        {
            var saleTax = ShelfPrice * tax;

            decimal decimalPart = (int)((saleTax - (int)saleTax) * 100); ;

            if (decimalPart % 10 >= 5)
                saleTax = (int)(saleTax) + (Math.Ceiling(decimalPart / 10) / 10);
            else if (decimalPart % 10 < 5)
                saleTax = (int)(saleTax) + (Math.Floor(decimalPart / 10) / 10);

            return Math.Round(saleTax, 2);
        }
    }
}
