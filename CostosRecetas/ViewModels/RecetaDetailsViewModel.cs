using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CostosRecetas.Models;
using CostosRecetas.Resources;
using CostosRecetas.Services;
using System.Collections.ObjectModel;
using UraniumUI.Icons.MaterialSymbols;

namespace CostosRecetas.ViewModels;

[QueryProperty(nameof(Receta), "Receta")]
public partial class RecetaDetailsViewModel : ObservableObject
{
    private readonly IDbService _dbService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    Receta receta = new();

    [ObservableProperty]
    bool agregarBtnVisible = true;

    [ObservableProperty]
    bool mainFab = false;

    [ObservableProperty]
    string mainFabIcon = MaterialSharp.Add;

    [ObservableProperty]
    ObservableCollection<IngredienteReceta> ingredientesSeleccionados = [];

    public RecetaDetailsViewModel(IDbService dbService, IAlertService alertService) {
        _dbService = dbService;
        _alertService = alertService;
    }

    public async Task CargarReceta() {
        var receta = await _dbService.GetFilteredAsync<Receta>(r => r.RecetaId == Receta.RecetaId);
        Receta = receta.First();
        await CargarIngredientesReceta();
    }

    public async Task CargarIngredientesReceta() {
        var ingredientes = await _dbService.GetFilteredAsync<IngredienteReceta>(ir => ir.RecetaId == Receta.RecetaId);
        foreach (var ingrediente in ingredientes) {
            var ing = await _dbService.GetFilteredAsync<Ingrediente>(i => i.IngredienteId == ingrediente.IngredienteId);
            ingrediente.IngredienteNav = ing.First();
            ingrediente.UnidadMedidaNav = UnidadesMedida.GetUnidad(ingrediente.UnidadMedidaId);
        }
        IngredientesSeleccionados = ingredientes.OrderBy(i => i.NumeroItem).ToObservableCollection();
    }

    [RelayCommand]
    public void ToggleFab() {
        MainFab = !MainFab;
        MainFabIcon = MainFab ? MaterialSharp.Close : MaterialSharp.Add;
    }

    [RelayCommand]
    public async Task EditarReceta() {
        ToggleFab();
        await Shell.Current.GoToAsync(nameof(RecetaAddEditPage),
            new Dictionary<string, object> {
                {nameof(Receta), Receta},
                {"Titulo", AppResources.EditRecipe }
            });
    }

    [RelayCommand]
    public async Task VerCostosReceta() {
        ToggleFab();
        await Shell.Current.GoToAsync(nameof(RecetaCostosPage),
            new Dictionary<string, object> {
                {nameof(Receta), Receta}
            });
    }
}
