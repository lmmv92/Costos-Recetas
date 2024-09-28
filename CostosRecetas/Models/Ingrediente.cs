using CostosRecetas.Helpers;
using CostosRecetas.Resources;
using SQLite;

namespace CostosRecetas.Models
{
    [Table(Constants.IngredienteTableName)]
    public class Ingrediente
    {
        [PrimaryKey, AutoIncrement]
        [Column("ingrediente_id")]
        public int IngredienteId { get; set; }

        [Unique]
        [Column("nombre")]
        public string Nombre { get; set; } = String.Empty;

        [Column("precio")]
        public decimal Precio { get; set; } = 0;

        [Column("cantidad")]
        public decimal Cantidad { get; set; } = 0;

        [Column("unidad_medida_id")]
        public int UnidadMedidaId { get; set; }

        [Ignore]
        public UnidadMedida? UnidadMedidaNav => UnidadesMedida.GetUnidad(UnidadMedidaId);

        [Ignore]
        public string CantidadUnidad => $"{Cantidad} {UnidadMedidaNav?.Abreviatura ?? "-"}";

        [Ignore]
        public string NombreLocalizado => AppResources.ResourceManager.GetString(Nombre) ?? Nombre;
    }
}
