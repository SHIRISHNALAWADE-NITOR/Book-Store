public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllUsers();
    Task<T> GetById(int id);
    Task<T> Add(T entity);
    Task<T> Update(T entity);
    Task Delete(int id);

    User GetByUsername(string username);
}