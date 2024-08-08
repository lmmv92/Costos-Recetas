using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class IngredienteAddEditPage : ContentPage
{
    public IngredienteAddEditPage(IngredienteAddEditViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
    }
}