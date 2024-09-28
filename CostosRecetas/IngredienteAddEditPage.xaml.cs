using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class IngredienteAddEditPage : ContentPage
{
    private readonly IngredienteAddEditViewModel _vm;

    public IngredienteAddEditPage(IngredienteAddEditViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }
}