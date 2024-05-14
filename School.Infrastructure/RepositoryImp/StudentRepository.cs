using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Data.Entities.StoreProc;
using School.Data.Entities.Views;
using School.Infrastructure.Bases;
using School.Infrastructure.IRepository;
using StoredProcedureEFCore;

namespace School.Infrastructure.RepositoryImp
{
    public class StudentRepository : BaseRepositoryImp<Student>, IStudentRepository
    {
        private ApplicationDatabaseContext _db;
        public StudentRepository(ApplicationDatabaseContext _dbContext) : base(_dbContext)
        {
            this._db = _dbContext;
        }

        public async Task<List<GetDepartmentStudentsProc>> GetDepartmentStudentsProc(int depId)
        {

            var list = new List<GetDepartmentStudentsProc>();

            await _db.LoadStoredProc("GetDepartmentStudentsProc")
                 .AddParam("DepartmentID", depId)
                 .ExecAsync(async r => list = await r.ToListAsync<GetDepartmentStudentsProc>());

            return list;
        }

        public async Task<List<StudentWithDepartmentDetailsView>> GetStudentWithDepartmentDetailsView()
        {
            return _db.StudentWithDepartmentDetailsViews.ToList();
        }
    }
}
