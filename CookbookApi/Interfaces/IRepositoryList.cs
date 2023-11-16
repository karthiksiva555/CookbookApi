namespace CookbookApi.Interfaces;

public interface IRepositoryList<T>
{
    T GetById(int id);
    IList<T> GetAll();
    void Add(T entity);
    void Update(int id, T entity);
    void Delete(int id);
}