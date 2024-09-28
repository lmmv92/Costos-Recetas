using CostosRecetas.Models;
using CostosRecetas.Resources;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CostosRecetas.Helpers
{
    public static class Constants
    {
        public const string IngredienteTableName = "ingrediente";
        public const string RecetaTableName = "receta";
        public const string IngredienteRecetaTableName = "ingrediente_receta";

        public const string CreateIngredienteRecetaTable =
            $"CREATE TABLE IF NOT EXISTS {IngredienteRecetaTableName} (" +
                $"ingrediente_receta_id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                $"ingrediente_id INTEGER, receta_id INTEGER," +
                $"FOREIGN KEY (ingrediente_id) REFERENCES ingrediente (ingrediente_id) ON DELETE CASCADE, " +
                $"FOREIGN KEY (receta_id) REFERENCES receta (receta_id) ON DELETE CASCADE, " +
                $"UNIQUE (ingrediente_id, receta_id)" +
            $");";

        public static readonly List<(string nombre, string tipo)> IngredientesRecetaColumns = [
            ("numero_item", "INTEGER"),
            ("cantidad","REAL"),
            ("unidad_medida_id","INTEGER"),
        ];


        public readonly static List<Ingrediente> Ingredientes = [
            new() {Nombre = nameof(AppResources.Flour),             Cantidad = 1000,    UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Sugar),             Cantidad = 1000,    UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Butter),            Cantidad = 500,     UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Eggs),              Cantidad = 15,      UnidadMedidaId = 10 },
            new() {Nombre = nameof(AppResources.Milk),              Cantidad = 1,       UnidadMedidaId = 3 },
            new() {Nombre = nameof(AppResources.CondensedMilk),     Cantidad = 1,       UnidadMedidaId = 10 },
            new() {Nombre = nameof(AppResources.EvaporatedMilk),    Cantidad = 1,       UnidadMedidaId = 10 },
            new() {Nombre = nameof(AppResources.BakingPowder),      Cantidad = 230,     UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.BakingSoda),        Cantidad = 454,     UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Vanilla),           Cantidad = 120,     UnidadMedidaId = 4 },
            new() {Nombre = nameof(AppResources.Salt),              Cantidad = 500,     UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Cocoa),             Cantidad = 500,     UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Yeast),             Cantidad = 230,     UnidadMedidaId = 1 },
            new() {Nombre = nameof(AppResources.Oil),               Cantidad = 500,     UnidadMedidaId = 4 }
        ];

        public const string KeepScreenOn = "keep_screen_on";
        public const string Language = "language";
        public const string Currency = "currency";
    }
}
