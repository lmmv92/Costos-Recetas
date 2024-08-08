using CostosRecetas.Helpers;
using CostosRecetas.Models;
using SQLite;
using System.Diagnostics;
using System.Linq.Expressions;

namespace CostosRecetas.Services;

public class DbService : IDbService
{
    private readonly string _dbPath;
    private SQLiteAsyncConnection _conn = default!;       
    public string ErrorMessage { get; private set; } = string.Empty;

    public DbService(string dbPath) {         
        _dbPath = dbPath;
    }

    async Task Init() {
        if (_conn != null)
            return;
        
        _conn = new SQLiteAsyncConnection(_dbPath);
        Debug.WriteLine($"Path: {_dbPath}");
        var result = await _conn.CreateTableAsync<Ingrediente>();
        await _conn.CreateTableAsync<Receta>();
        await _conn.ExecuteAsync(Constants.CreateIngredienteRecetaTable);       
        foreach (var column in Constants.IngredientesRecetaColumns) {
            await AddColumnIfNotExistsAsync(Constants.IngredienteRecetaTableName, column.nombre, column.tipo);
        }
        
        if (result == CreateTableResult.Created) {
            Debug.WriteLine("Se creó la tabla Ingrediente");
            await LoadInitialData();
        }
    }

    private async Task AddColumnIfNotExistsAsync(string tableName, string columnName, string columnType) {
        // Verificar si la columna existe
        string checkColumnQuery = $"PRAGMA table_info({tableName});";
        var columns = await _conn.QueryAsync<ColumnInfo>(checkColumnQuery);

        // Agregar la columna si no existe
        if (!columns.Any(c => c.Name == columnName)) {
            string addColumnQuery = $"ALTER TABLE {tableName} ADD COLUMN {columnName} {columnType};";
            await _conn.ExecuteAsync(addColumnQuery);
        }
    }

    private async Task LoadInitialData() {            
        if (await _conn.Table<Ingrediente>().CountAsync() > 0) {
            return;
        }            
        await _conn.InsertAllAsync(Constants.Ingredientes);
        Debug.WriteLine("Se cargaron los ingredientes por defecto");
    }

    public async Task<List<T>> GetAllAsync<T>() where T : new() {
        await Init();
        return await _conn.Table<T>().ToListAsync();
    }

    public async Task<List<T>> GetFilteredAsync<T>(Expression<Func<T, bool>> expression) where T : new() {
        await Init();
        return await _conn.Table<T>().Where(expression).ToListAsync();
    }

    public async Task<int> Add<T>(T entity) {
        int result = 0;
        try {
            await Init();
            result = await _conn.InsertAsync(entity);
        }
        catch (Exception ex) {
            ErrorMessage = string.Format("Error: {0}", ex.Message);
        }
        return result;
    }

    public async Task<int> AddAll<T>(IEnumerable<T> entity) {
        int result = 0;
        try {
            await Init();
            result = await _conn.InsertAllAsync(entity);
        }
        catch (Exception ex) {
            ErrorMessage = string.Format("Error: {0}", ex.Message);
        }
        return result;
    }

    public async Task<int> Update<T>(T entity) {
        int result = 0;
        try {
            await Init();
            result = await _conn.UpdateAsync(entity);
        }
        catch (Exception ex) {
            ErrorMessage = string.Format("Error: {0}", ex.Message);
        }
        return result;
    }
    
    public async Task<int> UpdateAll<T>(IEnumerable<T> entity) {
        int result = 0;
        try {
            await Init();
            result = await _conn.UpdateAllAsync(entity);
        }
        catch (Exception ex) {
            ErrorMessage = string.Format("Error: {0}", ex.Message);
        }
        return result;
    }

    public async Task<int> InsertOrUpdateAll(Receta receta, IEnumerable<IngredienteReceta> entities) {
        int result = 0;
        try {
            await Init();
            
            var existingEntities = await _conn.Table<IngredienteReceta>().Where(ex => ex.RecetaId == receta.RecetaId).ToListAsync();
            var entitiesToDelete = existingEntities.Where(ex => !entities.Any(ne => ne.IngredienteRecetaId == ex.IngredienteRecetaId)).ToList();
            foreach (var entity in entitiesToDelete) {
                result += await _conn.DeleteAsync(entity);
            }

            foreach (var entity in entities) {
                result += (entity.IngredienteRecetaId == 0) ? await _conn.InsertAsync(entity) : await _conn.UpdateAsync(entity);
            }
     
        }
        catch (Exception ex) {
            ErrorMessage = string.Format("Error: {0}", ex.Message);
        }
        return result;
    }


    public async Task<int> Delete<T>(T entity) {
        await Init();
        int result = 0;
        try {
            await Init();
            result = await _conn.DeleteAsync(entity);
        }
        catch (Exception ex) {
            ErrorMessage = string.Format("Error: {0}", ex.Message);
        }
        return result;
    }
}
