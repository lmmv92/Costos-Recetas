using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostosRecetas.Models;

public class Currency
{
    public string Name { get; set; } = string.Empty;
    public string CurrencySymbol { get; set; } = string.Empty;
    public CultureInfo Culture { get; set; } = default!;

    public override string ToString() {
        return $"{Culture.NativeName} {Name} ({CurrencySymbol})";
    }
}
