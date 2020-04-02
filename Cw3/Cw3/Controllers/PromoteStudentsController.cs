using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DTOs.Requests;
using Cw3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route("api/promote")]
    [ApiController]
    public class PromoteStudentsController : ControllerBase
    {
        IStudentDbService _service;

        public PromoteStudentsController(IStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult PromoteStudents(PropomoteStudentsRequest request)
        {
            _service.PromoteStudents(request);
            var response = _service.PromoteStudents(request);

            return Ok(response);
        }

    }
}