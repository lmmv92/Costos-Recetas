namespace CostosRecetas.Models;

public class IngredienteRecetaCosto
{
    public IngredienteReceta IngredienteReceta { get; set; } = default!;

    public decimal Costo { get; set; }
}
