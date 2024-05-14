using School.Data.Entities;
using School.Data.Entities.StoreProc;
using School.Data.Entities.Views;
using School.Data.Helpers;
using School.Infrastructure.IRepository;
using School.Service.IService;

namespace School.Service.ServiceImp
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        public async Task<string> AddStudnetAsync(Student student)
        {
            try
            {
                await studentRepository.AddAsync(student);
                return "Added";
            }
            catch (Exception e)
            {
                return e.InnerException.ToString();
            }

        }

        public async Task<string> DeleteAsync(Student entity)
        {
            try
            {
                await studentRepository.DeleteAsync(entity);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.InnerException.Message;
            }
        }

        public async Task<List<Student>> GetAllStudents()
        {
            var result = studentRepository.GetTableNoTracking().ToList();
            return result;
        }

        public Student GetById(int id)
        {
            var result = studentRepository.GetById(id);

            return result;
        }



        public IQueryable<Student> GetQueryableOfStudentTableWithFiltertionAndOrdering
            (StudentOrderByEnum? OrderBy, string? Search)
        {
            var query = studentRepository.GetTableNoTracking();

            if (Search != null)
                query = query.Where(st => st.StFname.Contains(Search) || st.StLname.Contains(Search));
            if (OrderBy != null)
            {
                switch (OrderBy)
                {
                    case StudentOrderByEnum.OrderById:
                        query = query.OrderBy(st => st.StudentId);
                        break;
                    case StudentOrderByEnum.OrderByFirstName:
                        query = query.OrderBy(st => st.StFname);
                        break;
                    case StudentOrderByEnum.OrderBySecondeName:
                        query = query.OrderBy(st => st.StLname);
                        break;
                    case StudentOrderByEnum.OrderByDepartment:
                        query = query.OrderBy(st => st.Department.DeptName);
                        break;
                }

            }
            return query;
        }

        public Task<List<StudentWithDepartmentDetailsView>> GetStudentWithDepartmentDetailsView()
        {
            return studentRepository.GetStudentWithDepartmentDetailsView();
        }

        public async Task<List<GetDepartmentStudentsProc>> GetDepartmentStudentsProc(int depId)
        {

            return await studentRepository.GetDepartmentStudentsProc(depId);
        }

        public async Task<bool> IsIdExistAsync(int id)
        {
            return await studentRepository.IsIdExistAsync(id);
        }
    }
}
