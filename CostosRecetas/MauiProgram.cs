using CommunityToolkit.Maui;
using CostosRecetas.Services;
using CostosRecetas.ViewModels;
using Microsoft.Extensions.Logging;
using UraniumUI;

namespace CostosRecetas
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddMaterialIconFonts();
                    fonts.AddMaterialSymbolsFonts();
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "costosrecetas.db3");
            builder.Services.AddSingleton<IDbService>(s => new DbService(dbPath));
            builder.Services.AddSingleton<IAlertService, AlertService>();
            builder.Services.AddSingleton<IUnitConversionService, UnitConversionService>();

            builder.Services.AddTransient<IngredientesPage>();
            builder.Services.AddTransient<IngredientesViewModel>();

            builder.Services.AddTransient<IngredienteAddEditPage>();
            builder.Services.AddTransient<IngredienteAddEditViewModel>();

            builder.Services.AddTransient<RecetasPage>();
            builder.Services.AddTransient<RecetasViewModel>();

            builder.Services.AddTransient<RecetaAddEditPage>();
            builder.Services.AddTransient<RecetaAddEditViewModel>();

            builder.Services.AddTransient<RecetaDetailsPage>();
            builder.Services.AddTransient<RecetaDetailsViewModel>();

            builder.Services.AddTransient<RecetaCostosPage>();
            builder.Services.AddTransient<RecetaCostosViewModel>();

            return builder.Build();
        }
    }
}
