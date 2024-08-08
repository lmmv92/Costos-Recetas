using CostosRecetas.Models;
using CostosRecetas.Resources;
using UnitOf;

namespace CostosRecetas.Services;

public class UnitConversionService : IUnitConversionService
{
    private readonly Dictionary<string, Func<double, double>> conversions = [];

    public UnitConversionService()
    {
        var mass = new Mass();
        var volm = new Volume();
        conversions.Add($"{AppResources.g}_to_{AppResources.mg}", quantity => mass.FromGrams(quantity).ToMilligrams());
        conversions.Add($"{AppResources.g}_to_{AppResources.cup}", quantity => quantity / 130);
        conversions.Add($"{AppResources.g}_to_{AppResources.tbsp}", quantity => quantity / 14);
        conversions.Add($"{AppResources.g}_to_{AppResources.tsp}", quantity => quantity / 4);
        conversions.Add($"{AppResources.g}_to_{AppResources.oz}", quantity => mass.FromGrams(quantity).ToOuncesUS());
        conversions.Add($"{AppResources.g}_to_{AppResources.lb}", quantity => mass.FromGrams(quantity).ToPounds());

        conversions.Add($"{AppResources.mg}_to_{AppResources.g}", quantity => mass.FromMilligrams(quantity).ToGrams());
        conversions.Add($"{AppResources.mg}_to_{AppResources.cup}", quantity => quantity / (130 * 1000));
        conversions.Add($"{AppResources.mg}_to_{AppResources.tbsp}", quantity => quantity / (14 * 1000));
        conversions.Add($"{AppResources.mg}_to_{AppResources.tsp}", quantity => quantity / (4 * 1000));
        conversions.Add($"{AppResources.mg}_to_{AppResources.oz}", quantity => mass.FromMilligrams(quantity).ToOuncesUS());
        conversions.Add($"{AppResources.mg}_to_{AppResources.lb}", quantity => mass.FromMilligrams(quantity).ToPounds());

        conversions.Add($"{AppResources.L}_to_{AppResources.ml}", quantity => volm.FromLiters(quantity).ToMilliliters());
        conversions.Add($"{AppResources.L}_to_{AppResources.cup}", quantity => volm.FromLiters(quantity).ToCupsMetric());
        conversions.Add($"{AppResources.L}_to_{AppResources.tbsp}", quantity => volm.FromLiters(quantity).ToTablespoonsMetric());
        conversions.Add($"{AppResources.L}_to_{AppResources.tsp}", quantity => volm.FromLiters(quantity).ToTeaspoonsMetric());
        conversions.Add($"{AppResources.L}_to_{AppResources.oz}", quantity => volm.FromLiters(quantity).ToFluidOuncesUS());

        conversions.Add($"{AppResources.ml}_to_{AppResources.L}", quantity => volm.FromMilliliters(quantity).ToLiters());
        conversions.Add($"{AppResources.ml}_to_{AppResources.cup}", quantity => volm.FromMilliliters(quantity).ToCupsMetric());
        conversions.Add($"{AppResources.ml}_to_{AppResources.tbsp}", quantity => volm.FromMilliliters(quantity).ToTablespoonsMetric());
        conversions.Add($"{AppResources.ml}_to_{AppResources.tsp}", quantity => volm.FromMilliliters(quantity).ToTeaspoonsMetric());
        conversions.Add($"{AppResources.ml}_to_{AppResources.oz}", quantity => volm.FromMilliliters(quantity).ToFluidOuncesUS());
        
        conversions.Add($"{AppResources.cup}_to_{AppResources.g}", quantity => quantity * 130);
        conversions.Add($"{AppResources.cup}_to_{AppResources.mg}", quantity => quantity * (130 * 1000));
        conversions.Add($"{AppResources.cup}_to_{AppResources.L}", quantity => volm.FromCupsMetric(quantity).ToLiters());
        conversions.Add($"{AppResources.cup}_to_{AppResources.ml}", quantity => volm.FromCupsMetric(quantity).ToMilliliters());
        conversions.Add($"{AppResources.cup}_to_{AppResources.tbsp}", quantity => volm.FromCupsMetric(quantity).ToTablespoonsMetric());
        conversions.Add($"{AppResources.cup}_to_{AppResources.tsp}", quantity => volm.FromCupsMetric(quantity).ToTeaspoonsMetric());
        conversions.Add($"{AppResources.cup}_to_{AppResources.oz}", quantity => volm.FromCupsMetric(quantity).ToFluidOuncesUS());        
        
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.g}", quantity => quantity * 14);        
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.mg}", quantity => quantity * (14 * 1000));        
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.L}", quantity => volm.FromTablespoonsMetric(quantity).ToLiters());        
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.ml}", quantity => volm.FromTablespoonsMetric(quantity).ToMilliliters());
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.cup}", quantity => volm.FromTablespoonsMetric(quantity).ToCupsMetric());
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.tsp}", quantity => volm.FromTablespoonsMetric(quantity).ToTeaspoonsMetric());
        conversions.Add($"{AppResources.tbsp}_to_{AppResources.oz}", quantity => volm.FromTablespoonsMetric(quantity).ToFluidOuncesUS());
        
        conversions.Add($"{AppResources.tsp}_to_{AppResources.g}", quantity => quantity * 4);
        conversions.Add($"{AppResources.tsp}_to_{AppResources.mg}", quantity => quantity * (4 * 1000));
        conversions.Add($"{AppResources.tsp}_to_{AppResources.L}", quantity => volm.FromTeaspoonsMetric(quantity).ToLiters());
        conversions.Add($"{AppResources.tsp}_to_{AppResources.ml}", quantity => volm.FromTeaspoonsMetric(quantity).ToMilliliters());
        conversions.Add($"{AppResources.tsp}_to_{AppResources.cup}", quantity => volm.FromTeaspoonsMetric(quantity).ToCupsMetric());
        conversions.Add($"{AppResources.tsp}_to_{AppResources.tbsp}", quantity => volm.FromTeaspoonsMetric(quantity).ToTablespoonsMetric());
        conversions.Add($"{AppResources.tsp}_to_{AppResources.oz}", quantity => volm.FromTeaspoonsMetric(quantity).ToFluidOuncesUS());
        
        conversions.Add($"{AppResources.oz}_to_{AppResources.g}", quantity => mass.FromOuncesUS(quantity).ToGrams());
        conversions.Add($"{AppResources.oz}_to_{AppResources.mg}", quantity => mass.FromOuncesUS(quantity).ToMilligrams());
        conversions.Add($"{AppResources.oz}_to_{AppResources.L}", quantity => volm.FromFluidOuncesUS(quantity).ToLiters());
        conversions.Add($"{AppResources.oz}_to_{AppResources.ml}", quantity => volm.FromFluidOuncesUS(quantity).ToMilliliters());
        conversions.Add($"{AppResources.oz}_to_{AppResources.cup}", quantity => volm.FromFluidOuncesUS(quantity).ToCupsMetric());
        conversions.Add($"{AppResources.oz}_to_{AppResources.tbsp}", quantity => volm.FromFluidOuncesUS(quantity).ToTablespoonsMetric());
        conversions.Add($"{AppResources.oz}_to_{AppResources.tsp}", quantity => volm.FromFluidOuncesUS(quantity).ToTeaspoonsMetric());
        
        conversions.Add($"{AppResources.lb}_to_{AppResources.g}", quantity => mass.FromPounds(quantity).ToGrams());
        conversions.Add($"{AppResources.lb}_to_{AppResources.mg}", quantity => mass.FromPounds(quantity).ToMilligrams());        
    }

    public decimal ConvertUnit(UnidadMedida? fromUnit, UnidadMedida? toUnit, decimal quantity) {
        if (fromUnit != null && toUnit != null) {
            if (fromUnit == toUnit) {
                return quantity;
            }
            var from = fromUnit.Abreviatura;
            var to = toUnit.Abreviatura;

            var conversionKey = $"{from}_to_{to}";

            //if (conversions.ContainsKey(conversionKey)) {
            //    return conversions[conversionKey](quantity);
            //}

            if (conversions.TryGetValue(conversionKey, out var conversionFunc)) {
                return (decimal)conversionFunc(decimal.ToDouble(quantity));
            }
        }

        return -1;
    }
}
