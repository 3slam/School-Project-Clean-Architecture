using AutoMapper;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Result;
using School.Data.Entities;
using School.Data.Entities.StoreProc;
using School.Data.Entities.Views;

namespace School.Core.Mappers.Students
{
    public partial class StudentProfile : Profile
    {

        public StudentProfile()
        {

            MapStudentToGetStudentListResponse();
            MapStudentToGetSingleStudentResponse();
            MapAddStudentCommandToStudent();
            MapDeleteStudentCommandToStudent();
            MapGetDepartmentStudentsProcToGetStudentListInDepartmentByDeptIdResponse();
            MapStudentWithDepartmentDetailsViewToStudentWithDepartmentDetailsResponse();
        }

        public void MapGetDepartmentStudentsProcToGetStudentListInDepartmentByDeptIdResponse()
        {
            CreateMap<GetDepartmentStudentsProc, GetStudentListInDepartmentByDeptIdResponse>();
        }

        public void MapStudentWithDepartmentDetailsViewToStudentWithDepartmentDetailsResponse()
        {
            CreateMap<StudentWithDepartmentDetailsView, StudentWithDepartmentDetailsResponse>();
        }


        public void MapStudentToGetStudentListResponse()
        {
            CreateMap<Student, GetStudentListResponse>()
                  .ForMember(desination => desination.StudentFname, opt => opt.MapFrom(source => source.StFname))
                  .ForMember(desination => desination.StudentLname, opt => opt.MapFrom(source => source.StLname))
                  .ForMember(desination => desination.StudentAddress, opt => opt.MapFrom(source => source.StAddress))
                  .ForMember(desination => desination.StudentAge, opt => opt.MapFrom(source => source.StAge))
                   .ForMember(desination => desination.Image, opt => opt.MapFrom(source => source.Image))
                  .ForMember(desination => desination.DepartmentName, opt => opt.MapFrom(source => source.Department.DeptName))
                  .ReverseMap();
        }

        public void MapStudentToGetSingleStudentResponse()
        {
            CreateMap<Student, GetSingleStudentResponse>().
                   ForMember(desination => desination.image, opt => opt.MapFrom(source => source.Image))
                  .ForMember(desination => desination.StudentFname, opt => opt.MapFrom(source => source.StFname))
                  .ForMember(desination => desination.StudentLname, opt => opt.MapFrom(source => source.StLname))
                  .ForMember(desination => desination.StudentAddress, opt => opt.MapFrom(source => source.StAddress))
                  .ForMember(desination => desination.StudentAge, opt => opt.MapFrom(source => source.StAge))
                  .ReverseMap();
        }

        public void MapAddStudentCommandToStudent()
        {
            CreateMap<AddStudentCommand, Student>()

                  .ForMember(desination => desination.StFname, opt => opt.MapFrom(source => source.FirstName))
                  .ForMember(desination => desination.StLname, opt => opt.MapFrom(source => source.LastName))
                  .ForMember(desination => desination.StAddress, opt => opt.MapFrom(source => source.LastName))
                   .ForMember(desination => desination.StAge, opt => opt.MapFrom(source => source.Age))
                                .ForMember(desination => desination.DepartmentId, opt => opt.MapFrom(source => 3))
                  ;
        }


        public void MapDeleteStudentCommandToStudent()
        {
            CreateMap<DeleteStudentCommand, Student>()
                  .ForMember(desination => desination.StudentId, opt => opt.MapFrom(source => source.StudentId));

        }



    }
}
