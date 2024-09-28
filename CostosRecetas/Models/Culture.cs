using CostosRecetas.Helpers;
using System.Globalization;

namespace CostosRecetas.Models;

public static class Culture
{
    public static readonly List<CultureInfo> Cultures = [
        new CultureInfo("en"),
        new CultureInfo("es")
    ];

    public static List<CultureInfo> GetCultureInfos() => Cultures;

    //public static void SetLanguage(string language) {        
    //    var culture = new CultureInfo(language);

    //    Thread.CurrentThread.CurrentCulture = culture;
    //    Thread.CurrentThread.CurrentUICulture = culture;
    //    CultureInfo.DefaultThreadCurrentCulture = culture;
    //    CultureInfo.DefaultThreadCurrentUICulture = culture;
    //}
}