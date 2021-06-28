using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> ItemList { get; }

        public ShoppingCart()
        {
            ItemList = new List<ShoppingCartItem>();
        }

        public void AddProduct(Product p)
        {
            var currentItem = ItemList.SingleOrDefault(sp => sp.Item.Name == p.Name);
            if (currentItem == null)
                ItemList.Add(new ShoppingCartItem(p));
            else
                currentItem.IncreaseQuantityBy(1);
        }
    }
}
