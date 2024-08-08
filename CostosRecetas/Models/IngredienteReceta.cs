using CommunityToolkit.Mvvm.ComponentModel;
using CostosRecetas.Helpers;
using SQLite;

namespace CostosRecetas.Models
{
    [Table(Constants.IngredienteRecetaTableName)]
    public partial class IngredienteReceta
    {        

        [PrimaryKey, AutoIncrement]
        [Column("ingrediente_receta_id")]
        public int IngredienteRecetaId { get; set; }

        [Indexed]
        [Column("ingrediente_id")]
        public int IngredienteId { get; set; }

        [Indexed]
        [Column("receta_id")]
        public int RecetaId { get; set; }

        [Column("numero_item")]
        public int NumeroItem { get; set; }

        [Column("cantidad")]
        public decimal Cantidad { get; set; } = 0;

        [Column("unidad_medida_id")]
        public int? UnidadMedidaId { get; set; }

        [Ignore]
        public Ingrediente IngredienteNav { get; set; } = default!;

        [Ignore]
        public Receta RecetaNav { get; set; } = default!;

        [Ignore]
        public UnidadMedida? UnidadMedidaNav { get; set; } = default!;

        [Ignore]
        public string CantidadUnidad => $"{Cantidad} {UnidadMedidaNav?.Abreviatura ?? "-"}";
    }
}
