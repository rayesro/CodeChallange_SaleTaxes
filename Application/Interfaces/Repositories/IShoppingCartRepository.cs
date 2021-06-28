using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories
{
    public interface IShoppingCartRepository
    {
        Task<ShoppingCartItem> AddProductAsync(Product item);

        Task<List<ShoppingCartItem>> GetShoppingCartListAsync();
    }
}
