using Microsoft.EntityFrameworkCore.Storage;

namespace School.Infrastructure.Bases
{
    public interface IBaseRepository<T> where T : class
    {
        Task DeleteRangeAsync(ICollection<T> entities);
        T GetById(int id);

        Task<Boolean> IsIdExistAsync(int id);

        Task SaveChangesAsync();


        IQueryable<T> GetTableNoTracking();
        IQueryable<T> GetTableAsTracking();
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(ICollection<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(ICollection<T> entities);
        Task DeleteAsync(T entity);


        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync(IDbContextTransaction transaction);
        Task RollBackAsync(IDbContextTransaction transaction);
    }
}
