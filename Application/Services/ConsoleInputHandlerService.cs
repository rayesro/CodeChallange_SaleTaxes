using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class ConsoleInputHandlerService : IInputHandlerService<string>
    {
        public Product GetProductFromInput(string input)
        {
            string pattern = @"^[0-9](.+)at\s([0-9]{1,4}[.][0-9]{1,4})";
            Regex rg = new Regex(pattern);
            MatchCollection matches = rg.Matches(input);
            if (matches.Count == 0)
                return null;

            return new Product(matches[0].Groups[1].Value.Trim(), Convert.ToDecimal(matches[0].Groups[2].Value));
        }
    }
}
