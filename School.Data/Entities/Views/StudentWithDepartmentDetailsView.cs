using Microsoft.EntityFrameworkCore;

namespace School.Data.Entities.Views
{
    [Keyless]
    public class StudentWithDepartmentDetailsView
    {
        public int StudentId { get; set; }
        public string StFname { get; set; }
        public string StLname { get; set; }
        public string StAddress { get; set; }
        public int StAge { get; set; }
        public string DeptName { get; set; }
        public string DeptDesc { get; set; }
        public string DeptLocation { get; set; }
        public int DeptManager { get; set; }
        public DateTime ManagerHiredate { get; set; }

    }
}
