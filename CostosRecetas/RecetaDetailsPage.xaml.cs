using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class RecetaDetailsPage : ContentPage
{
    public RecetaDetailsViewModel _vm;
	public RecetaDetailsPage(RecetaDetailsViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _vm.CargarReceta();
    }

    protected override void OnDisappearing() {
        base.OnDisappearing();
        DeviceDisplay.Current.KeepScreenOn = false;
    }
}