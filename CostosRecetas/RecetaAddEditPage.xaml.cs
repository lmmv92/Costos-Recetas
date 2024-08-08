using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class RecetaAddEditPage : ContentPage
{
    private readonly RecetaAddEditViewModel _vm;

    public RecetaAddEditPage(RecetaAddEditViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _vm.CargarIngredientesReceta();
    }

    private void Grid_BindingContextChanged(object sender, EventArgs e) {
        if (sender is Grid grid) {
            if (grid.FindByName<Picker>("picker") == null) {
                var picker = new Picker();
                picker.ItemDisplayBinding = new Binding("Abreviatura");
                picker.SetBinding(Picker.ItemsSourceProperty, new Binding("UnidadesMedidaPicker", source: _vm));
                picker.SetBinding(Picker.SelectedItemProperty, new Binding("UnidadMedidaNav", BindingMode.TwoWay));
                picker.SetValue(Grid.ColumnProperty, 3);
                grid.Children.Add(picker);
            }
        }
    }
}