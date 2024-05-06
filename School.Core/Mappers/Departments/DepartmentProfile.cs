using AutoMapper;
using School.Core.Features.Departments.Commnads.Models;
using School.Core.Features.Departments.Queries.Result;
using School.Data.Entities;

namespace School.Core.Mappers.Departments
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            MapDepartmentToGetDepartmentListResponse();
            MapDepartmentToGetSingleDepartmentResponse();
            MapAddDepartmentCommandToDepartment();
            MapDeleteDepartmentCommandToDepartment();
        }

        public void MapDepartmentToGetDepartmentListResponse()
        {
            CreateMap<Department, GetDepartmentListResponse>()
                  .ForMember(desination => desination.DepartmentName, opt => opt.MapFrom(source => source.DeptName))
                  .ForMember(desination => desination.DepartmentDesc, opt => opt.MapFrom(source => source.DeptDesc))
                  .ForMember(desination => desination.DepartmentLocation, opt => opt.MapFrom(source => source.DeptLocation))
                  .ReverseMap();
        }

        public void MapDepartmentToGetSingleDepartmentResponse()
        {
            CreateMap<Department, GetSingleDepartmentResponse>()
                .ForMember(desination => desination.DepartmentName, opt => opt.MapFrom(source => source.DeptName))
                  .ForMember(desination => desination.DepartmentDesc, opt => opt.MapFrom(source => source.DeptDesc))
                  .ForMember(desination => desination.DepartmentLocation, opt => opt.MapFrom(source => source.DeptLocation))
                  .ReverseMap();
        }

        public void MapAddDepartmentCommandToDepartment()
        {
            CreateMap<AddDepartmentCommand, Department>()
                  .ForMember(desination => desination.DeptName, opt => opt.MapFrom(source => source.DeptName))
                  .ForMember(desination => desination.DeptDesc, opt => opt.MapFrom(source => source.DeptDesc))
                  .ForMember(desination => desination.DeptLocation, opt => opt.MapFrom(source => source.DeptLocation)
                     )
                 ;
        }


        public void MapDeleteDepartmentCommandToDepartment()
        {
            CreateMap<DeleteDepartmentCommand, Department>()
                  .ForMember(desination => desination.DepartmentId, opt => opt.MapFrom(source => source.DepartmentId));

        }


    }
}
