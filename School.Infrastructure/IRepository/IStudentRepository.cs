using School.Data.Entities;
using School.Infrastructure.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.IRepository
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
       
    }
}
