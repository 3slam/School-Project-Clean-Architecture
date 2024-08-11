using School.Core.Wrapper;
using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Wrappers
{
    public class PaginationService : IPaginationService<Student>
    {
        public async Task<PaginationResponse<Student>> ToPaginationListAsync(IQueryable<Student> source, int pageNumber, int pageSize)
        {
            return await source.ToPaginationListAsync(pageNumber, pageSize);
        }
    }
}
