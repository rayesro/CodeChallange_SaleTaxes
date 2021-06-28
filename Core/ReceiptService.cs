using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class ReceiptService
    {
        private readonly TaxingService _taxingService;

        public List<ShoppingCartItem> ShoppingCart { get; }

        public ReceiptService(TaxingService taxingService)
        {
            _taxingService = taxingService;
            ShoppingCart = new List<ShoppingCartItem>();
        }

        public void AddProductToShoppingCart(Product p)
        {
            _taxingService.AssignTaxesTo(p);
            _taxingService.CalculateTaxes(p);
            var currentItem = ShoppingCart.SingleOrDefault(sp => sp.Item.Name == p.Name);
            if (currentItem == null)
                ShoppingCart.Add(new ShoppingCartItem(p));
            else
                currentItem.IncreaseQuantityBy(1);
        }

        public string GetReceipt()
        {
            StringBuilder sb = new StringBuilder();

            decimal total = 0;
            decimal saleTaxes = 0;
            foreach (var cartItem in ShoppingCart)
            {
                var currentTotal = cartItem.Item.TotalPrice * cartItem.Quantity;

                sb.Append($"{cartItem.Item.Name}: {currentTotal:0.00}");
                if (cartItem.Quantity > 1)
                    sb.Append($" ({cartItem.Quantity} @ {cartItem.Item.ShelfPrice + cartItem.Item.SaleTax:0.00})");

                sb.AppendLine();

                total += currentTotal;
                saleTaxes += cartItem.Item.SaleTax * cartItem.Quantity;
            }
            sb.AppendLine($"Sales Taxes: {saleTaxes:0.00}");
            sb.Append($"Total: {total:0.00}");

            return sb.ToString();
        }
    }
}
