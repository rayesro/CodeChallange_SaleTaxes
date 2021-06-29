using Application.Interface.Services;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Response<int>>
    {
        public string Name { get; }
        public decimal ShelfPrice { get; }

        public CreateProductCommand(string name, decimal shelfPrice)
        {
            Name = name;
            ShelfPrice = shelfPrice;
        }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<int>>
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly ITaxingService _taxingService;

        public CreateProductCommandHandler(IShoppingCartRepository shoppingCartRepository, ITaxingService taxingService)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _taxingService = taxingService;
        }

        public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(request.Name, request.ShelfPrice);
            _taxingService.AssignTaxesTo(product);
            _taxingService.CalculateTaxesFor(product);
            await _shoppingCartRepository.AddProductAsync(product);
            return new Response<int>(1);
        }
    }
}
