using Repositories.Models;

namespace Repositories.Contracts
{
    public interface IRepository <T>
    {
        Task Create(T obj);
        Task<IEnumerable<T>> GetAll();
        void Update(T entity);
        void Delete(int id);
        Task<T> GetById(int? id);
    }
}