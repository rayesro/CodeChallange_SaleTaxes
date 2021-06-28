namespace Core
{
    public class ShoppingCartItem
    {
        public Product Item { get; private set; }
        public int Quantity { get; private set; }
        public ShoppingCartItem(Product p)
        {
            Item = p;
            Quantity = 1;
        }

        public void IncreaseQuantityBy(int qty)
        {
            if(qty > 0)
                Quantity += qty;
        }
    }
}
