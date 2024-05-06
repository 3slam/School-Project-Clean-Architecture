using School.Data.Entities;
using School.Data.Helpers;
using School.Infrastructure.IRepository;
using School.Service.IService;

namespace School.Service.ServiceImp
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository repository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            this.repository = departmentRepository;
        }


        public async Task<string> AddDepartmentAsync(Department department)
        {
            try
            {
                await repository.AddAsync(department);
                return "Added";
            }
            catch (Exception e)
            {
                return e.InnerException.ToString();
            }

        }

        public async Task<string> DeleteAsync(Department entity)
        {
            try
            {
                await repository.DeleteAsync(entity);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public async Task<List<Department>> GetAllDepartment()
        {
            var result = repository.GetTableNoTracking().ToList();
            return result;
        }

        public Department GetById(int id)
        {
            var result = repository.GetById(id);

            return result;
        }



        public IQueryable<Department> GetQueryableOfDepartmentTableWithFiltertionAndOrdering
            (DepartmentOrderByEnum? OrderBy, string? Search)
        {
            var query = repository.GetTableNoTracking();

            if (Search != null)
                query = query.Where(st => st.DeptName.Contains(Search) || st.DeptLocation.Contains(Search));
            if (OrderBy != null)
            {
                switch (OrderBy)
                {
                    case DepartmentOrderByEnum.OrderById:
                        query = query.OrderBy(dp => dp.DepartmentId);
                        break;
                    case DepartmentOrderByEnum.OrderByName:
                        query = query.OrderBy(dp => dp.DeptName);
                        break;

                }

            }
            return query;
        }

        public async Task<bool> IsIdExistAsync(int id)
        {
            return await repository.IsIdExistAsync(id);
        }

        public Task<bool> IsIdExistOnceAsync(int id)
        {
            return repository.IsIdExistOnceAsync(id);
        }
    }
}
