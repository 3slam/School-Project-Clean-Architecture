using School.Data.Entities;
using School.Data.Helpers;

namespace School.Service.IService
{
    public interface IStudentService
    {
        public Task<List<Student>> GetAllStudents();
        public Student GetById(int id);

        Task<Boolean> IsIdExistAsync(int id);
        public Task<string> AddStudnetAsync(Student student);

        public Task<String> DeleteAsync(Student entity);

        public IQueryable<Student> GetQueryableOfStudentTableWithFiltertionAndOrdering
            (
            StudentOrderByEnum? OrderBy,
            string? Search
            );
    }
}
