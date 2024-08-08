using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CostosRecetas.Models;
using CostosRecetas.Resources;
using CostosRecetas.Services;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace CostosRecetas.ViewModels;

public partial class RecetasViewModel : ObservableObject
{
    private readonly IDbService _dbService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    ObservableCollection<Receta> recetas;

    [ObservableProperty]
    string textoBuscar = String.Empty;

    [ObservableProperty]
    bool agregarBtnVisible = true;

    public RecetasViewModel(IDbService dbService, IAlertService alertService) {
        _dbService = dbService;
        _alertService = alertService;
        Recetas = [];
    }

    public async Task CargarRecetas() {
        var recetas = await _dbService.GetAllAsync<Receta>();
        Recetas = recetas.OrderBy(r => r.Nombre).ToObservableCollection();
    }

    [RelayCommand]
    public async Task BuscarRecetas() {
        if (String.IsNullOrEmpty(TextoBuscar)) {
            await CargarRecetas();
            return;
        }

        Expression<Func<Receta, bool>> expression = x => x.Nombre.ToLower().Contains(TextoBuscar.ToLower());
        var recetas = await _dbService.GetFilteredAsync<Receta>(expression);

        Recetas = recetas.ToObservableCollection();
    }

    [RelayCommand]
    public async Task AgregarReceta() {
        await Shell.Current.GoToAsync(nameof(RecetaAddEditPage));
    }

    [RelayCommand]
    public async Task DetallesReceta(Receta receta) {
        await Shell.Current.GoToAsync(nameof(RecetaDetailsPage),
            new Dictionary<string, object> {
                {nameof(Receta), receta }
            });
    }

    [RelayCommand]
    public async Task EliminarReceta(Receta receta) {
        if (await _alertService.ShowConfirmationAsync(AppResources.Delete, $"{AppResources.RecipeDelete} '{receta.Nombre}'")) {            
            await _dbService.Delete(receta);
            await CargarRecetas();
        }
    }
}