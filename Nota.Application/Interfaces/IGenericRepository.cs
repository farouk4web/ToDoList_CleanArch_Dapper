using Nota.Application.Common;
using Nota.Domain.Entities;

namespace Nota.Application.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<PaginatedListResponse<T>> GetAllAsync(PaginatedRequest request, bool isDeleted = false);
        Task<IEnumerable<T>> GetAllAsync(bool isDeleted = false);
        Task<T> GetByIdAsync(int id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> ToggleDeleteAsync(int id);
    }
}