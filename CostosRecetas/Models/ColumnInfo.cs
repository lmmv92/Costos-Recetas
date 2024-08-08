using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostosRecetas.Models;
public class ColumnInfo
{
    public int Cid { get; set; }
    public string Name { get; set; } = default!;
    public string Type { get; set; } = default!;
    public int Notnull { get; set; }
    public string Dflt_value { get; set; } = default!;
    public int Pk { get; set; }
}
