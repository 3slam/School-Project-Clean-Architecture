namespace School.Core.Features.Departments.Queries.Result
{
    public class GetDepartmentListResponse
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string DepartmentDesc { get; set; }
        public string DepartmentLocation { get; set; }
    }
}
