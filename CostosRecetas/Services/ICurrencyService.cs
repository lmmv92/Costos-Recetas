using CostosRecetas.Models;
using System.Globalization;

namespace CostosRecetas.Services;

public interface ICurrencyService
{
    List<Currency> GetCurrencies();
    CultureInfo GetCurrentCurrencyCulture();
}
