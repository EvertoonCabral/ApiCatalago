namespace ApiCatalago.Repositories
{
    public interface IRepository<T> 
    {
        
        Task<IEnumerable<T>> GetAllAsync();
        
       Task <T> GetByIdAsync(int id);
        
        T Create(T entity);
        
        T Update(T entity);
        
        T Delete(T entity);
    }
}
