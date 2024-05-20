using eTickets.Models;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    public interface IEntityBaseRepository<T> where T: class, new()
    {
        //CRUD Operations
        
        //C
        Task AddAsync(T entity);
        
        //R
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByIdAsync(int id);
        
        //U
        Task UpdateAsync(T entity);
        
        //D
        Task DeleteAsync(int id);
    }
}
