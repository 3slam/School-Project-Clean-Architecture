using School.Data.Entities;
using School.Infrastructure.Bases;

namespace School.Infrastructure.IRepository
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<Boolean> IsIdExistOnceAsync(int id);


    }
}
