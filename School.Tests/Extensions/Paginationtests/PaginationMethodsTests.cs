using EntityFrameworkCore.Testing.Common;
using FluentAssertions;
using Moq;
using School.Core.Wrapper;
using School.Data.Entities;
using School.Tests.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace School.Tests.Extensions.Paginationtests
{
    public class PaginationMethodsTests
    {
        private readonly Mock<IPaginationService<Student>> mockPage;

        public PaginationMethodsTests()
        {
            mockPage = new();
        }

        [Fact]
        public async void ToPaginatedListAsync_Should_Return_List()
        {
            // arrange

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
            var listThatImplementIAsyncEnumerable = new AsyncEnumerable<Student>(list).AsQueryable();

            // act
            var paginatedResult = new PaginationResponse<Student>(listThatImplementIAsyncEnumerable.ToList());

            mockPage.Setup(x => x.
                 ToPaginationListAsync(listThatImplementIAsyncEnumerable, 1, 1))
                .ReturnsAsync(paginatedResult);

            var result = await mockPage.Object.ToPaginationListAsync(listThatImplementIAsyncEnumerable, 1, 1);
            // asssert

            result.Data.Should().NotBeNull();

        }
        [Fact]
        public async void Sleep()
        {

            Thread.Sleep(5000);

        }

    }
}
