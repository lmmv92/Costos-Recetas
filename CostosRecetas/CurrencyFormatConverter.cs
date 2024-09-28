using CostosRecetas.Helpers;
using CostosRecetas.Services;
using System.Globalization;

namespace CostosRecetas;

public class CurrencyFormatConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
        if (value is decimal decimalValue) {
            CurrencyService currencyService = new CurrencyService();
            //CultureInfo currencyCulture = new(Preferences.Get(Constants.Currency, CultureInfo.CurrentCulture.Name));
            CultureInfo currencyCulture = currencyService.GetCurrentCurrencyCulture();
            return decimalValue.ToString("C", currencyCulture);
        }
        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
        throw new NotImplementedException();
    }
}
