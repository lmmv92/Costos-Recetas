using System.Resources;
using System.Threading.Tasks;

namespace CostosRecetas.Services;

public interface IAlertService
{
    Task<bool> ShowConfirmationAsync(string title, string message);
    Task ShowAlertAsync(string title, string message, string accept, string cancel);
    Task ShowSnackbar(string message);
    void ShowToast(string message);
}
