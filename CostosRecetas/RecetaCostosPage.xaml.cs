using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class RecetaCostosPage : ContentPage
{
	public RecetaCostosViewModel _vm;
	public RecetaCostosPage(RecetaCostosViewModel vm)
	{	
		InitializeComponent();
		BindingContext = vm;
		_vm = vm;
	}

    protected override async void OnAppearing() {
        base.OnAppearing();
		await _vm.CargarIngredientesReceta();
    }
}