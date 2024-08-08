using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CostosRecetas.Models;
using CostosRecetas.Services;
using System.Collections.ObjectModel;

namespace CostosRecetas.ViewModels;

[QueryProperty(nameof(Receta), "Receta")]
public partial class RecetaCostosViewModel : ObservableObject
{
    private readonly IDbService _dbService;
    private readonly IAlertService _alertService;
    private readonly IUnitConversionService _unitConversionService;

    [ObservableProperty]
    Receta receta = new();

    [ObservableProperty]
    ObservableCollection<IngredienteRecetaCosto> ingredientesReceta = [];

    [ObservableProperty]
    decimal costoTotalPrimario = 0;

    public RecetaCostosViewModel(IDbService dbService, IAlertService alertService, IUnitConversionService unitConversionService) {
        _dbService = dbService;
        _alertService = alertService;
        _unitConversionService = unitConversionService;
    }

    public async Task CargarIngredientesReceta() {
        var ingredientes = await _dbService.GetFilteredAsync<IngredienteReceta>(ir => ir.RecetaId == Receta.RecetaId);
        foreach (var ingrediente in ingredientes) {
            var ing = await _dbService.GetFilteredAsync<Ingrediente>(i => i.IngredienteId == ingrediente.IngredienteId);
            ingrediente.IngredienteNav = ing.First();
            ingrediente.UnidadMedidaNav = UnidadesMedida.GetUnidad(ingrediente.UnidadMedidaId);
        }
        IngredientesReceta = CalcularCostoIngredientes(ingredientes).ToObservableCollection();
        CostoTotalPrimario = IngredientesReceta.Sum(x => x.Costo);
    }

    public List<IngredienteRecetaCosto> CalcularCostoIngredientes(IEnumerable<IngredienteReceta> ingredientesReceta) {
        List<IngredienteRecetaCosto> ingredientesCosto = [];
        foreach (var ingredienteReceta in ingredientesReceta) {

            var ingredienteComprado = ingredienteReceta.IngredienteNav;

            //var costo = ingredienteReceta.Cantidad * ingredienteReceta.IngredienteNav.Precio / ingredienteReceta.IngredienteNav.Cantidad ?? 0;
            var cantIngRec = _unitConversionService.ConvertUnit(ingredienteReceta.UnidadMedidaNav, ingredienteComprado.UnidadMedidaNav, ingredienteReceta.Cantidad);
            var costo = cantIngRec * ingredienteComprado.Precio / ingredienteComprado.Cantidad;

            ingredientesCosto.Add(new IngredienteRecetaCosto {
                IngredienteReceta = ingredienteReceta,
                Costo = costo,
            });
        }
        return ingredientesCosto;
    }
}
