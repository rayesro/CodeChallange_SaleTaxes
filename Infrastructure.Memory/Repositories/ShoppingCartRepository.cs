﻿using Application.Interfaces.Repositories;
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Memory.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private List<ShoppingCartItem> ItemList { get; }

        public ShoppingCartRepository()
        {
            ItemList = new List<ShoppingCartItem>();
        }

        public Task<ShoppingCartItem> AddProductAsync(Product product)
        {
            var currentItem = ItemList.SingleOrDefault(sp => sp.Item.Name == product.Name);
            if (currentItem == null)
                ItemList.Add(new ShoppingCartItem(product));
            else
                currentItem.IncreaseQuantityBy(1);

            return Task.FromResult(currentItem);
        }

        public Task<List<ShoppingCartItem>> GetShoppingCartListAsync()
            => Task.FromResult(ItemList);
    }
}
