using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Constants
{
    // adding Parm using classData becouse its strong typed.
    public class StudentIdTestsValues : TheoryData<int>
    {
        public StudentIdTestsValues()
        {
            for (int i = 1; i <= 3; i++) { Add(i); }
        }
    }
}
