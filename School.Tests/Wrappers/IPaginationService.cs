using Microsoft.EntityFrameworkCore;
using School.Core.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Wrappers
{
    public interface IPaginationService <T> 
    {
        public Task<PaginationResponse<T>> ToPaginationListAsync(IQueryable<T> source, int pageNumber, int pageSize);
       
    }
}
