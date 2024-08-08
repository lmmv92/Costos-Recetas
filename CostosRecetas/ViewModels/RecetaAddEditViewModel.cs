using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CostosRecetas.Models;
using CostosRecetas.Resources;
using CostosRecetas.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CostosRecetas.ViewModels;

[QueryProperty(nameof(Receta), "Receta")]
[QueryProperty(nameof(Titulo), "Titulo")]
public partial class RecetaAddEditViewModel : ObservableObject
{
    private readonly IDbService _dbService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    string titulo = AppResources.AddRecipe;

    [ObservableProperty]
    Receta receta = new();

    [ObservableProperty]
    List<UnidadMedida> unidadesMedidaPicker;

    [ObservableProperty]
    ObservableCollection<IngredienteReceta> ingredientesSeleccionados = [];

    [ObservableProperty]
    ObservableCollection<Ingrediente> ingredientesBusqueda = [];

    IngredienteReceta itemToMove;

    [ObservableProperty]
    string textoBuscar = String.Empty;

    public RecetaAddEditViewModel(IDbService dbService, IAlertService alertService) {
        _dbService = dbService;
        _alertService = alertService;
        UnidadesMedidaPicker = UnidadesMedida.GetUnidadesReceta();
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
    public async Task BuscarIngredientes() {
        if (String.IsNullOrEmpty(TextoBuscar)) {
            IngredientesBusqueda.Clear();
            return;
        }

        Expression<Func<Ingrediente, bool>> expression = x => x.Nombre.ToLower().Contains(TextoBuscar.ToLower());
        var ingredientes = await _dbService.GetFilteredAsync<Ingrediente>(expression);
        ingredientes = ingredientes.Where(bus => !IngredientesSeleccionados.Any(sel => sel.IngredienteId == bus.IngredienteId)).ToList();

        IngredientesBusqueda = ingredientes.ToObservableCollection();
    }

    [RelayCommand]
    public void AgregarIngredienteReceta(Ingrediente ingrediente) {
        IngredientesBusqueda.Clear();
        TextoBuscar = String.Empty;
        var ingredienteReceta = new IngredienteReceta {
            IngredienteId = ingrediente.IngredienteId,
            NumeroItem = IngredientesSeleccionados.Count + 1,
            IngredienteNav = ingrediente
        };
        IngredientesSeleccionados.Add(ingredienteReceta);
    }

    [RelayCommand]
    public void EliminarIngredienteReceta(IngredienteReceta ingrediente) {
        IngredientesSeleccionados.Remove(ingrediente);
        OrdenarIngredientes();
    }

    [RelayCommand]
    public async Task GuardarReceta() {
        if (String.IsNullOrEmpty(Receta.Nombre))
            return;

        Receta.Nombre = Receta.Nombre.Trim();

        var (result, msg) = Receta.RecetaId <= 0 ? (await _dbService.Add(Receta), AppResources.RecipeAdded) : (await _dbService.Update(Receta), AppResources.RecipeUpdtd);

        if (result != 1) {
            msg = ($"{AppResources.RecipeSaveError}. {_dbService.ErrorMessage}");
        }
        else {
            foreach (var ingredienteReceta in IngredientesSeleccionados) {
                ingredienteReceta.RecetaId = Receta.RecetaId;
                ingredienteReceta.UnidadMedidaId = ingredienteReceta.UnidadMedidaNav?.Id ?? null;
            }

            await _dbService.InsertOrUpdateAll(Receta, IngredientesSeleccionados);
        }

        _alertService.ShowToast(msg);
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    public void DragItem(IngredienteReceta ingrediente) {
        Debug.WriteLine($"Dragging {ingrediente.IngredienteNav.Nombre}");
        itemToMove = ingrediente;
    }

    [RelayCommand]
    public void DropItem(IngredienteReceta ingrediente) {
        Debug.WriteLine($"Drop into {ingrediente.IngredienteNav.Nombre}");
        if (ingrediente == itemToMove) return;

        var index = IngredientesSeleccionados.IndexOf(ingrediente);
        IngredientesSeleccionados.Remove(itemToMove);
        IngredientesSeleccionados.Insert(index, itemToMove);

        OrdenarIngredientes();
    }

    private void OrdenarIngredientes() {
        for (int i = 0; i < IngredientesSeleccionados.Count; i++) {
            IngredientesSeleccionados[i].NumeroItem = i + 1;
        }
    }
}
