using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
    }
}