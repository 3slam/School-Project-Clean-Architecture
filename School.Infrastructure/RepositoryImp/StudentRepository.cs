using Microsoft.EntityFrameworkCore;
using School.Data.Entities;
using School.Infrastructure.Bases;
using School.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Infrastructure.RepositoryImp
{
    public class StudentRepository : BaseRepositoryImp<Student> ,IStudentRepository 
    {
        private ApplicationDatabaseContext _db;
        public StudentRepository(ApplicationDatabaseContext _dbContext) : base(_dbContext) 
        {
         this._db = _dbContext;
        }


         
    }
}
