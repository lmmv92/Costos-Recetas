using CostosRecetas.Helpers;
using SQLite;

namespace CostosRecetas.Models
{
    [Table(Constants.RecetaTableName)]
    public class Receta
    {
        [PrimaryKey, AutoIncrement]
        [Column("receta_id")]
        public int RecetaId { get; set; }

        [Unique]
        [Column("nombre")]
        public string Nombre { get; set; } = default!;

        [Column("preparacion")]
        public string Preparacion { get; set; } = default!;
    }
}
