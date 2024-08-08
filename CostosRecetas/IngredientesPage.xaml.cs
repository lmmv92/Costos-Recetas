using CostosRecetas.ViewModels;

namespace CostosRecetas;

public partial class IngredientesPage : ContentPage
{
    private readonly IngredientesViewModel _vm;

    private double _lastScrollY;
    private double _itemHeight = 60;
    private double _collectionViewHeight;

    public IngredientesPage(IngredientesViewModel vm) {
        InitializeComponent();
        BindingContext = vm;
        _vm = vm;
    }

    protected override async void OnAppearing() {
        base.OnAppearing();
        await _vm.CargarIngredientes();
    }

    private void CollectionView_Scrolled(object sender, ItemsViewScrolledEventArgs e) {
        //var viewmodel = BindingContext as IngredientesViewModel;
        //if (viewmodel == null) return;

        //var currentScrollY = e.VerticalOffset;
        //if (currentScrollY > _lastScrollY) {
        //    viewmodel.AgregarBtnVisible = false;
        //}
        //else if (currentScrollY < _lastScrollY) {
        //    viewmodel.AgregarBtnVisible= true;
        //}

        //_lastScrollY = currentScrollY;
    }

}
