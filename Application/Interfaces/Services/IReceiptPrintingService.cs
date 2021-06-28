namespace Application.Interfaces.Services
{
    public interface IReceiptPrintingService<T>
    {
        public T PrintReceipt();
    }
}
