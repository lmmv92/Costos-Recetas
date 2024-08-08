using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class RecetasPage : ContentPage
{
    private readonly RecetasViewModel _vm;
    public RecetasPage(RecetasViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }
    protected override async void OnAppearing() {
        base.OnAppearing();
        await _vm.CargarRecetas();
    }

    private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e) {

    }
}