using CostosRecetas.Helpers;
using CostosRecetas.Models;
using LocalizationResourceManager.Maui;
using System.Globalization;

namespace CostosRecetas;

public partial class App : Application
{
    
    public App() {
        InitializeComponent();

        MainPage = new AppShell();
    }


}
