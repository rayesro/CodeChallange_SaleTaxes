using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<List<ShoppingCartItem>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ShoppingCartItem>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public GetAllProductsQueryHandler(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

     
        public async Task<List<ShoppingCartItem>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            => await _shoppingCartRepository.GetShoppingCartListAsync();
            
        
    }
}
