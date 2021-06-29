using Domain.Entities;

namespace Application.Interface.Services
{
    public interface ITaxingService
    {
        void AssignTaxesTo(Product product);
        void CalculateTaxesFor(Product product);
        decimal RoundUpTax(decimal shelfPrice, decimal tax);
    }
}