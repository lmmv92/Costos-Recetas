using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CostosRecetas.Models;
using CostosRecetas.Resources;
using CostosRecetas.Services;

namespace CostosRecetas.ViewModels;

[QueryProperty(nameof(Ingrediente), "Ingrediente")]
[QueryProperty(nameof(UnidadSeleccionada), "UnidadSeleccionada")]
[QueryProperty(nameof(Titulo), "Titulo")]
public partial class IngredienteAddEditViewModel : ObservableObject
{
    private readonly IDbService _dbService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    string titulo = AppResources.AddIngr;

    [ObservableProperty]
    Ingrediente ingrediente = new();

    [ObservableProperty]
    List<UnidadMedida> unidadesMedidaPicker = UnidadesMedida.GetUnidadesReceta();

    [ObservableProperty]
    UnidadMedida unidadSeleccionada = default!;

    public IngredienteAddEditViewModel(IDbService dbService, IAlertService alertService) {
        _dbService = dbService;
        _alertService = alertService;       
    }

    [RelayCommand]
    public void NombreTextChanged(string newText) {
        Ingrediente.Nombre = newText;
    }

    [RelayCommand]
    public async Task GuardarIngrediente() {
        if (String.IsNullOrEmpty(Ingrediente.Nombre) || UnidadSeleccionada is null) {
            _alertService.ShowToast(AppResources.MissingData);
            return;
        }
           
        Ingrediente.Nombre = Ingrediente.Nombre.Trim();
        Ingrediente.UnidadMedidaId = UnidadSeleccionada.Id;

        var (result, msg) = Ingrediente.IngredienteId <= 0 ? (await _dbService.Add(Ingrediente), AppResources.IngrAdded) : (await _dbService.Update(Ingrediente), AppResources.IngrUpdtd);            

        var alert = result == 1 ? msg : $"{AppResources.IngrSaveError}. {_dbService.ErrorMessage}";
        _alertService.ShowToast(alert);

        await Shell.Current.GoToAsync("..");
    }
}
