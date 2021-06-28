using System;

namespace Core
{
    public class Product
    {
        public decimal ShelfPrice { get; private set; }
       
        public Product( decimal shelfPrice)
        {
            ShelfPrice = shelfPrice;
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
