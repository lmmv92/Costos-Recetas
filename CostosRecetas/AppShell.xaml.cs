namespace CostosRecetas
{
    public partial class AppShell : Shell
    {
        public AppShell() {
            InitializeComponent();

            Routing.RegisterRoute(nameof(IngredienteAddEditPage), typeof(IngredienteAddEditPage));
            Routing.RegisterRoute(nameof(RecetaAddEditPage), typeof(RecetaAddEditPage));
            Routing.RegisterRoute(nameof(RecetaDetailsPage), typeof(RecetaDetailsPage));
            Routing.RegisterRoute(nameof(RecetaCostosPage), typeof(RecetaCostosPage));
        }
    }
}
