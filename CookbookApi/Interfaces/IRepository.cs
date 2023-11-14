namespace CookbookApi.Interfaces;

public interface IRepository<T>
{
    T GetById(int id);
    IList<T> GetAll();
    void Add(T entity);
    void Update(int id, T entity);
    void Delete(int id);
}