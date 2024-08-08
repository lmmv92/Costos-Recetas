using CostosRecetas.Models;
using System.Linq.Expressions;

namespace CostosRecetas.Services;

public interface IDbService
{
    string ErrorMessage { get; }

    Task<List<T>> GetAllAsync<T>() where T : new();
    Task<List<T>> GetFilteredAsync<T>(Expression<Func<T, bool>> expression) where T : new();
    Task<int> Add<T>(T entity);
    Task<int> AddAll<T>(IEnumerable<T> entity);
    Task<int> Update<T>(T entity);
    Task<int> UpdateAll<T>(IEnumerable<T> entity);
    Task<int> InsertOrUpdateAll(Receta receta, IEnumerable<IngredienteReceta> entities);    
    Task<int> Delete<T>(T entity);
}
