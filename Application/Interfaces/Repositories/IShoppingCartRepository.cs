using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        public List<ShoppingCartItem> ItemList { get; }
        Task<ShoppingCartItem> AddProductAsync(Product item);
    }
}
