using System.Threading;
using System.Threading.Tasks;

namespace HubSpot.NET.Core.Interfaces
{
    public interface ICRUDable
    {
        void Delete(long id);
        Task DeleteAsync(long id, CancellationToken cancellationToken = default);
    }

    public interface ICRUDable<T> : ICRUDable
    {
        T Create(T entity);
        Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);
        T GetById(long id);
        Task<T> GetByIdAsync(long id, CancellationToken cancellationToken = default);
        T Update(T entity);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default);
    }
}
