using CostosRecetas.Models;

namespace CostosRecetas.Services;

public interface IUnitConversionService
{
    decimal ConvertUnit(UnidadMedida? from, UnidadMedida? to, decimal quantity);
}
