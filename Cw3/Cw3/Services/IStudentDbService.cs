using Cw3.DIOs.Requests;
using Cw3.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Services
{
    public interface IStudentDbService
    {
        public string EnrollStudent(EnrollStudentRequest request);
        public string PromoteStudents(PropomoteStudentsRequest request);
    }
}
