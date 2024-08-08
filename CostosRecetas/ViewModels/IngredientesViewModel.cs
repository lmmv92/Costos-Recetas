﻿
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CostosRecetas.Models;
using CostosRecetas.Resources;
using CostosRecetas.Services;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace CostosRecetas.ViewModels;

public partial class IngredientesViewModel : ObservableObject
{
    private readonly IDbService _dbService;
    private readonly IAlertService _alertService;

    [ObservableProperty]
    ObservableCollection<Ingrediente> ingredientes;

    [ObservableProperty]
    string textoBuscar = String.Empty;

    [ObservableProperty]
    bool agregarBtnVisible = true;

    public IngredientesViewModel(IDbService dbService, IAlertService alertService) {
        _dbService = dbService;
        _alertService = alertService;
        Ingredientes = [];
    }

    public async Task CargarIngredientes() {
        var ingredientes = await _dbService.GetAllAsync<Ingrediente>();
        Ingredientes = ingredientes.OrderBy(i => i.Nombre).ToObservableCollection();
    }
    
    [RelayCommand]
    public async Task BuscarIngredientes() {
        if (String.IsNullOrEmpty(TextoBuscar)) {
            await CargarIngredientes();
            return;
        }

        Expression<Func<Ingrediente, bool>> expression = x => x.Nombre.ToLower().Contains(TextoBuscar.ToLower());
        var ingredientes = await _dbService.GetFilteredAsync<Ingrediente>(expression);        

        Ingredientes = ingredientes.ToObservableCollection();
    }

    [RelayCommand]
    public async Task AgregarIngrediente() {
        await Shell.Current.GoToAsync(nameof(IngredienteAddEditPage));
    }

    [RelayCommand]
    public async Task EditarIngrediente(Ingrediente ingrediente) {
        TextoBuscar = String.Empty;
        await Shell.Current.GoToAsync(nameof(IngredienteAddEditPage),
            new Dictionary<string, object?> {
                {nameof(Ingrediente), ingrediente },
                {"UnidadSeleccionada", UnidadesMedida.GetUnidad(ingrediente.UnidadMedidaId)},
                {"Titulo", AppResources.EditIngr }
            });
    }

    [RelayCommand]
    public async Task EliminarIngrediente(Ingrediente ingrediente) {
        if (await _alertService.ShowConfirmationAsync(AppResources.Delete, $"{AppResources.IngrDelete} '{ingrediente.Nombre}'?")) {
            await _dbService.Delete(ingrediente);
            await CargarIngredientes();
        }
    }
}
