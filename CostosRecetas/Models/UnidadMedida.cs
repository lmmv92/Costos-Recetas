using CostosRecetas.Resources;

namespace CostosRecetas.Models
{
    public class UnidadMedida
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = default!;
        public string Abreviatura { get; set; } = default!;
    }

    public static class UnidadesMedida
    {
        public static readonly List<UnidadMedida> ListaUnidadesMedida = [
            new UnidadMedida { Id = 1,  Nombre = AppResources.Unit_g,       Abreviatura = AppResources.g },
            new UnidadMedida { Id = 2,  Nombre = AppResources.Unit_mg,      Abreviatura = AppResources.mg },
            new UnidadMedida { Id = 3,  Nombre = AppResources.Unit_L,      Abreviatura = AppResources.L },
            new UnidadMedida { Id = 4,  Nombre = AppResources.Unit_ml,      Abreviatura = AppResources.ml },
            new UnidadMedida { Id = 5,  Nombre = AppResources.Unit_cup,     Abreviatura = AppResources.cup },
            new UnidadMedida { Id = 6,  Nombre = AppResources.Unit_tbsp,    Abreviatura = AppResources.tbsp },
            new UnidadMedida { Id = 7,  Nombre = AppResources.Unit_tsp,    Abreviatura = AppResources.tsp },
            new UnidadMedida { Id = 8,  Nombre = AppResources.Unit_oz,      Abreviatura = AppResources.oz },
            new UnidadMedida { Id = 9,  Nombre = AppResources.Unit_lb,      Abreviatura = AppResources.lb },
            new UnidadMedida { Id = 10, Nombre = AppResources.Unit_unit,    Abreviatura = AppResources.unit }
        ];

        //TODO: Crear otra lista con más unidades para conversión     

        public static List<UnidadMedida> GetUnidadesReceta() => ListaUnidadesMedida;

        public static UnidadMedida? GetUnidad(int? id) => ListaUnidadesMedida.FirstOrDefault(u => u.Id == id);
    }
}
