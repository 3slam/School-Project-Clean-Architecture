using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using School.Data.Entities;
using School.Core.Features.Students.Queries.Result;
using School.Core.Bases;

namespace School.Core.Features.Students.Queries.Models
{
    public class GetStudentByIdQuery(int StudentId) : IRequest<Response<GetSingleStudentResponse>>
    {
        public int StudentId = StudentId;

    }
}
