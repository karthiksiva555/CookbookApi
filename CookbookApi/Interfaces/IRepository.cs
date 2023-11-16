namespace CookbookApi.Interfaces;

public interface IRepository<T>
{
    Task<IList<T>> GetAllAsync();

    Task<T> GetByIdAsync(int id);

    Task AddAsync(T entity);

    Task UpdateAsync(int id, T updatedRecipe);

    Task DeleteAsync(int id);
}