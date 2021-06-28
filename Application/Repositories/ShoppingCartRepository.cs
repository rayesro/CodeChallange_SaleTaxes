using Application.Interfaces.Repositories;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        public List<ShoppingCartItem> ItemList { get; }

        public ShoppingCartRepository()
        {
        }

        public Task<ShoppingCartItem> AddProductAsync(Product product)
        {
            var currentItem = ItemList.SingleOrDefault(sp => sp.Item.Name == p.Name);
            if (currentItem == null)
                ItemList.Add(new ShoppingCartItem(product));
            else
                currentItem.IncreaseQuantityBy(1);

            return Task.FromResult(currentItem);
        }
    }
}
