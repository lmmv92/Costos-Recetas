
using CommunityToolkit.Maui.Alerts;
using CostosRecetas.Resources;

namespace CostosRecetas.Services;

class AlertService : IAlertService
{
    public Task<bool> ShowConfirmationAsync(string title, string message) {
        return Shell.Current.DisplayAlert(title, message, AppResources.AlertAccept, AppResources.AlertCancel);
    }

    public Task ShowAlertAsync(string title, string message, string accept, string cancel) {
        return Shell.Current.DisplayAlert(title, message, accept, cancel);
    }

    public Task ShowSnackbar(string message) {
        return Shell.Current.DisplaySnackbar(message);
    }

    public void ShowToast(string message) {
        Toast.Make(message, CommunityToolkit.Maui.Core.ToastDuration.Short, 14).Show();
    }
}
