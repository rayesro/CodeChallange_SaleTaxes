using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IInputHandlerService<T>
    {
        public Product GetProductFromInput(T input);
    }
}
