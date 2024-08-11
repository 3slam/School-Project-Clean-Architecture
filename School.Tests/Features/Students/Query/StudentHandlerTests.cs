using AutoMapper;
using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Moq;
using School.Core.Features.Students.Queries.Handler;
using School.Core.Features.Students.Queries.Models;
using School.Core.Features.Students.Queries.Result;
using School.Core.Mappers.Students;
using School.Core.Wrapper;
using School.Data.Entities;
using School.Data.Helpers;
using School.Data.Resourses;
using School.Service.IService;
using School.Tests.Constants;
using School.Tests.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

[assembly : CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]
namespace School.Tests.Features.Students.Query
{
    public class StudentHandlerTests
    {
        private readonly Mock<IStudentService> studentService ;
        private readonly Mock<IStringLocalizer<SharedResourses>> localizer;
        private readonly StudentHandler handler;
    
        private readonly IMapper mapper;
        private readonly StudentProfile studentProfile;
       public StudentHandlerTests()
        {
          
            studentProfile = new();
            studentService = new();
            localizer = new();
            var config = new MapperConfiguration(m => m.AddProfile(studentProfile));
            mapper = new Mapper(config);
            handler = new StudentHandler(localizer.Object, studentService.Object, mapper);
        }

       [Fact]
        public async void GetStudentListQuery_ShouldBe_NotNull_NotEmpty()
        {
            // arrange
            var list = new List<Student>()
            {
             new Student
            {
                StudentId = 1,
                StFname = "John",
                StLname = "Doe",
                StAddress = "123 Maple Street",
                Image = "john_doe.jpg",
                StAge = 20,
                DepartmentId = 101,

            },

              new Student
            {
                StudentId = 2,
                StFname = "Jane",
                StLname = "Smith",
                StAddress = "456 Oak Avenue",
                StAge = 22,
                DepartmentId = 102,
                Department = new Department { DepartmentId = 102 }
            },

               new Student
            {
                StudentId = 3,
                StFname = "Alice",
                StLname = "Johnson",
                StAddress = "789 Pine Road",
                Image = "alice_johnson.jpg",
                DepartmentId = 103,

            }
            };
            studentService.Setup(x => x.GetAllStudents()).Returns(Task.FromResult(list));
            var q = new GetStudentListQuery();
            // act
            var result =await handler.Handle(q, default);
            // asssert
            result.Data.Should().NotBeNullOrEmpty();
            result.Data.Should().BeOfType<List<GetStudentListResponse>>();

        }

        [Theory]
        [InlineData(5)]
        public async void GetStudentByIdQuery_WhenIDNotExist_ShouldBeReturnNotFound(int id)
        {
            // arrange
            var list = new List<Student>()
            {
             new Student
            {
                StudentId = 1,
                StFname = "John",
                StLname = "Doe",
                StAddress = "123 Maple Street",
                Image = "john_doe.jpg",
                StAge = 20,
                DepartmentId = 101,

            },

              new Student
            {
                StudentId = 2,
                StFname = "Jane",
                StLname = "Smith",
                StAddress = "456 Oak Avenue",
                StAge = 22,
                DepartmentId = 102,
                Department = new Department { DepartmentId = 102 }
            },

               new Student
            {
                StudentId = 3,
                StFname = "Alice",
                StLname = "Johnson",
                StAddress = "789 Pine Road",
                Image = "alice_johnson.jpg",
                DepartmentId = 103,

            }
            };
            studentService.Setup(x => x.GetById(id)).Returns(list.FirstOrDefault(s => s.StudentId == id));
            var q = new GetStudentByIdQuery(id);
            // act
            var result = await handler.Handle(q, default);
            // asssert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Theory]
        [ClassData(typeof(StudentIdTestsValues))]
        public async void GetStudentByIdQuery_WhenIdIsExist_ShouldBeReturnSuccess(int id)
        {
            // arrange
            var list = new List<Student>()
            {
             new Student
            {
                StudentId = 1,
                StFname = "John",
                StLname = "Doe",
                StAddress = "123 Maple Street",
                Image = "john_doe.jpg",
                StAge = 20,
                DepartmentId = 101,

            },

              new Student
            {
                StudentId = 2,
                StFname = "Jane",
                StLname = "Smith",
                StAddress = "456 Oak Avenue",
                StAge = 22,
                DepartmentId = 102,
                Department = new Department { DepartmentId = 102 }
            },

               new Student
            {
                StudentId = 3,
                StFname = "Alice",
                StLname = "Johnson",
                StAddress = "789 Pine Road",
                Image = "alice_johnson.jpg",
                DepartmentId = 103,

            }
            };
            studentService.Setup(x => x.GetById(id)).Returns(list.FirstOrDefault(s => s.StudentId == id));
            var q = new GetStudentByIdQuery(id);
            // act
            var result = await handler.Handle(q, default);
            // asssert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async void GetStudentListPaginationQuery_ShouldBe_NotNull_NotEmpty()
        {

            var list = new List<Student>()
            {
             new Student
                  {
                    StudentId = 2,
                    StFname = "Jane",
                    StLname = "Smith",
                    StAddress = "456 Oak Avenue",
                    StAge = 22,
                    DepartmentId = 102,
                   Department = new Department
                   {
                       DepartmentId = 102
                    }
                 }
            };
            var listThatImplementIAsyncEnumerable = new AsyncEnumerable<Student>(list);

            var q = new GetPaginationStudentListQuery();
            // arrange
            studentService.Setup(x => x.GetQueryableOfStudentTableWithFiltertionAndOrdering
            (
                StudentOrderByEnum.OrderById, "")).Returns(listThatImplementIAsyncEnumerable.AsQueryable());

            var studentHandler = new StudentHandler(
                localizer.Object,
                studentService.Object,
                mapper);

            // act
            var result = await studentHandler.Handle(q, default);

            // asssert

            result.Data.Should().NotBeNull();
          }


        

    }

   
}
