
using CostosRecetas.Helpers;
using CostosRecetas.Models;
using System.Globalization;

namespace CostosRecetas.Services;

public class CurrencyService : ICurrencyService
{
    public List<Currency> GetCurrencies() {
        var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);

        var currencyInfos = cultures.Select(culture => {
            var regionInfo = new RegionInfo(culture.Name);
            return new Currency {
                Name = regionInfo.ISOCurrencySymbol,
                CurrencySymbol = regionInfo.CurrencySymbol,
                Culture = culture,
            };
        }).OrderBy(c => c.Culture.NativeName).ToList();

        return currencyInfos;
    }

    public CultureInfo GetCurrentCurrencyCulture() {
        return new CultureInfo(Preferences.Get(Constants.Currency, CultureInfo.CurrentCulture.Name));
    }
}
