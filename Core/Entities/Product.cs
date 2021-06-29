using Domain.Enums;
using System.Collections.Generic;

namespace Domain.Entities
{
    public class Product
    {
        public string Name { get; private set; }
        public decimal ShelfPrice { get; private set; }
        public decimal SaleTax { get; private set; }
        public List<TaxTypes> Taxes { get; private set; }
        public decimal TotalPrice => ShelfPrice + SaleTax;
        public Product(string name, decimal shelfPrice)
        {
            Name = name.Trim();
            ShelfPrice = shelfPrice;
            Taxes = new List<TaxTypes>();
        }

        public void AddTax(TaxTypes taxType)
        {
            if (!Taxes.Contains(taxType))
                Taxes.Add(taxType);
        }

        public void SetTaxes(decimal taxes)
        {
            SaleTax = taxes;
        }
    }
}
