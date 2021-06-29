using Application.Interfaces.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Receipts.Queries.GetReceipt
{
    public class GetReceiptForConsoleAppQuery : IRequest<string>
    {
    }

    public class GetReceiptForConsoleAppQueryHandler : IRequestHandler<GetReceiptForConsoleAppQuery, string>
    {
        private readonly IReceiptPrintingService<string> _receiptPrintingService;
        public GetReceiptForConsoleAppQueryHandler(IReceiptPrintingService<string> receiptPrintingService)
        {
            _receiptPrintingService = receiptPrintingService;
        }

        public Task<string> Handle(GetReceiptForConsoleAppQuery request, CancellationToken cancellationToken)
            => Task.FromResult(_receiptPrintingService.PrintReceipt());
    }
}