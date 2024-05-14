using School.Data.Entities;
using School.Data.Entities.StoreProc;
using School.Data.Entities.Views;
using School.Infrastructure.Bases;

namespace School.Infrastructure.IRepository
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        public Task<List<GetDepartmentStudentsProc>> GetDepartmentStudentsProc(int depId);

        public Task<List<StudentWithDepartmentDetailsView>> GetStudentWithDepartmentDetailsView();
    }
}
