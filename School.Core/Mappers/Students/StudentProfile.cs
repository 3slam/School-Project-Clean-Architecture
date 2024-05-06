using AutoMapper;
using School.Core.Features.Students.Commands.Models;
using School.Core.Features.Students.Queries.Result;
using School.Data.Entities;

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
        }

        public void MapStudentToGetStudentListResponse()
        {
            CreateMap<Student, GetStudentListResponse>()
                  .ForMember(desination => desination.StudentFname, opt => opt.MapFrom(source => source.StFname))
                  .ForMember(desination => desination.StudentLname, opt => opt.MapFrom(source => source.StLname))
                  .ForMember(desination => desination.StudentAddress, opt => opt.MapFrom(source => source.StAddress))
                  .ForMember(desination => desination.StudentAge, opt => opt.MapFrom(source => source.StAge))
                  .ForMember(desination => desination.DepartmentName, opt => opt.MapFrom(source => source.Department.DeptName))
                  .ReverseMap();
        }

        public void MapStudentToGetSingleStudentResponse()
        {
            CreateMap<Student, GetSingleStudentResponse>()
                  .ForMember(desination => desination.StudentFname, opt => opt.MapFrom(source => source.StFname))
                  .ForMember(desination => desination.StudentLname, opt => opt.MapFrom(source => source.StLname))
                  .ForMember(desination => desination.StudentAddress, opt => opt.MapFrom(source => source.StAddress))
                  .ForMember(desination => desination.StudentAge, opt => opt.MapFrom(source => source.StAge))
                  .ReverseMap();
        }

        public void MapAddStudentCommandToStudent()
        {
            CreateMap<AddStudentCommand, Student>()
                  .ForMember(desination => desination.StudentId, opt => opt.MapFrom(source => source.Id))
                  .ForMember(desination => desination.StFname, opt => opt.MapFrom(source => source.FirstName))
                  .ForMember(desination => desination.StLname, opt => opt.MapFrom(source => source.LastName))
                 ;
        }


        public void MapDeleteStudentCommandToStudent()
        {
            CreateMap<DeleteStudentCommand, Student>()
                  .ForMember(desination => desination.StudentId, opt => opt.MapFrom(source => source.StudentId));

        }


    }
}
