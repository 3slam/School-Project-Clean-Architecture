using School.Data.Entities;
using School.Infrastructure.Bases;
using School.Infrastructure.IRepository;

namespace School.Infrastructure.RepositoryImp
{
    public class DepartmentRepository : BaseRepositoryImp<Department>, IDepartmentRepository
    {
        private ApplicationDatabaseContext _db;
        public DepartmentRepository(ApplicationDatabaseContext _dbContext) : base(_dbContext)
        {
            _db = _dbContext;
        }



        public async Task<bool> IsIdExistOnceAsync(int id)
        {
            var count = _db.Departments.Where(d => d.DepartmentId == id).Count();
            if (count == 1) return true;
            else return false;
        }
    }
}
