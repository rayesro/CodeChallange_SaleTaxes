using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IInputHandlerService<T>
    {
        public Product GetProductFromInput(T input);
    }
}
