using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Receipts.Queries.GetReceipt;
using Application.Interfaces.Services;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Ticketizonator
    {
        private readonly IInputHandlerService<string> _inputHandlerService;
        private readonly IReceiptPrintingService<string> _receiptService;

        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly MediatR.IMediator _mediator;

        List<Product> _productList;
        public Ticketizonator(
            IInputHandlerService<string> inputHandlerService,
            IReceiptPrintingService<string> receiptService,
            IServiceScopeFactory serviceScopeFactory,
            MediatR.IMediator mediator
            )
        {
            _inputHandlerService = inputHandlerService;
            _receiptService = receiptService;
            _mediator = mediator;

            _serviceScopeFactory = serviceScopeFactory;
            _productList = new List<Product>();
        }

        private void TranslateInputIntoProductList(List<string> input)
        {
            foreach (var item in input)
            {
                var product = _inputHandlerService.GetProductFromInput(item);
                if (product == null)
                    continue;
                _productList.Add(product);
            }
        }

        private async Task SendCreateProductCmds()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                foreach (var item in _productList)
                {
                    var cmd = new CreateProductCommand(item.Name, item.ShelfPrice);
                    await _mediator.Send(cmd, new System.Threading.CancellationToken());
                }
            }
        }

        private async Task<string> GetReceipt()
        {
            string receipt = string.Empty;
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                foreach (var item in _productList)
                {
                    var query = new GetReceiptForConsoleAppQuery();
                    receipt = await _mediator.Send(query, new System.Threading.CancellationToken());
                }
            }

            return receipt;
        }

        public async Task<string> Run(List<string> input)
        {
            //translating input data to our models
            TranslateInputIntoProductList(input);
            //processing products and taxes
            await SendCreateProductCmds();
            //printing results
            string receipt = await GetReceipt();
            //string receipt = _receiptService.PrintReceipt();
            return receipt;
        }
    }
}
