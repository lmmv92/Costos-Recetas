using CommunityToolkit.Mvvm.ComponentModel;
using CostosRecetas.Helpers;
using CostosRecetas.Models;
using CostosRecetas.Services;
using LocalizationResourceManager.Maui;
using System.Globalization;


namespace CostosRecetas.ViewModels;

public partial class SettingsViewModel : ObservableObject
{
    ILocalizationResourceManager _localizationResourceManager;
    ICurrencyService _currencyService;

    [ObservableProperty]
    bool toggleScreenOn = Preferences.Default.Get(Constants.KeepScreenOn, false);

    [ObservableProperty]
    List<CultureInfo> culturesPicker = Models.Culture.GetCultureInfos();

    [ObservableProperty]
    CultureInfo cultureSelected = CultureInfo.CurrentCulture;
    
    [ObservableProperty]
    List<Currency> currencyPicker = default!;

    [ObservableProperty]
    Currency currencySelected = default!;

    public SettingsViewModel(ILocalizationResourceManager localizationResourceManager, ICurrencyService currencyService) {
        _localizationResourceManager = localizationResourceManager;
        _currencyService = currencyService;

        CurrencyPicker = _currencyService.GetCurrencies();
        CurrencySelected = CurrencyPicker.First(c => c.Culture.Equals(_currencyService.GetCurrentCurrencyCulture()));        
    }

    partial void OnToggleScreenOnChanged(bool value) {
        Preferences.Default.Set(Constants.KeepScreenOn, ToggleScreenOn);
    }

    partial void OnCultureSelectedChanged(CultureInfo value) {
        _localizationResourceManager.CurrentCulture = new CultureInfo(CultureSelected.Name);
    }
    
    partial void OnCurrencySelectedChanged(Currency value) {
        Preferences.Set(Constants.Currency, CurrencySelected.Culture.Name);    
    }
}
