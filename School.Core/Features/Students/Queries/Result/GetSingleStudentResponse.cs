using School.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Core.Features.Students.Queries.Result
{
    public class GetSingleStudentResponse
    {
        public string StudentFname { get; set; }

        public string StudentLname { get; set; }

        public string StudentAddress { get; set; }

        public int? StudentAge { get; set; }

        

    }
}
