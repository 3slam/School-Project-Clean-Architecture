using School.Data.Entities;
using School.Data.Helpers;

namespace School.Service.IService
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAllDepartment();
        public Department GetById(int id);

        Task<Boolean> IsIdExistAsync(int id);
        Task<Boolean> IsIdExistOnceAsync(int id);
        public Task<string> AddDepartmentAsync(Department department);

        public Task<String> DeleteAsync(Department entity);

        public IQueryable<Department> GetQueryableOfDepartmentTableWithFiltertionAndOrdering(
            DepartmentOrderByEnum? OrderBy,
            string? Search
            );
    }
}
