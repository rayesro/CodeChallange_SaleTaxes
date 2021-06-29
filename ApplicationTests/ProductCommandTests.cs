using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Queries.GetAllProducts;
using Application.Interface.Services;
using Application.Interfaces.Repositories;
using Application.Services;
using Domain.Entities;
using Infrastructure.Memory.Repositories;
using MediatR;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApplicationTests
{
    public class ProductCommandTests
    {
        IShoppingCartRepository _shoppingCartRepository;
        ITaxingService _taxingService;
        [SetUp]
        public void Setup()
        {
            _taxingService = new TaxingService();
            _shoppingCartRepository = new ShoppingCartRepository();
        }

        [Test]
        public async Task Given_ACreateProductCmd_When_HandlingTheCmd_Then_GetASuccededResult()
        {
            CreateProductCommand command = new CreateProductCommand("Book", 12.34m);
            CreateProductCommandHandler handler = 
                new CreateProductCommandHandler(_shoppingCartRepository, _taxingService);

            var result = await handler.Handle(command, new System.Threading.CancellationToken());

            Assert.AreEqual(true, result.Succeeded);
            Assert.AreEqual(1, result.Data);
        }

        [Test]
        public async Task Given_ACreateProductCmd_When_GettingShopItemList_Then_CountMustBe1()
        {
            CreateProductCommand command = new CreateProductCommand("Book", 12.34m);
            CreateProductCommandHandler handler =
                new CreateProductCommandHandler(_shoppingCartRepository, _taxingService);
            await handler.Handle(command, new System.Threading.CancellationToken());
            var cartList = _shoppingCartRepository.GetShoppingCartListAsync().Result;
            Assert.AreEqual(1, cartList.Count);
        }

        [Test]
        public async Task Given_ACreateProductCmd_When_QueryingAllProducts_Then_ShoppingCartItemListCountMustBe1()
        {
            CreateProductCommand command = new CreateProductCommand("Imported Music CD", 12.34m);
            CreateProductCommandHandler handler =
                new CreateProductCommandHandler(_shoppingCartRepository, _taxingService);
            await handler.Handle(command, new System.Threading.CancellationToken());

            var query = new GetAllProductsQuery();
            var queryHandler = new GetAllProductsQueryHandler(_shoppingCartRepository);
            var cartList = await queryHandler.Handle(query, new System.Threading.CancellationToken());

            Assert.Greater(cartList[0].Item.SaleTax, 0);
        }

       
    }
}