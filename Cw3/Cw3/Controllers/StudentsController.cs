using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cw3.Controllers
{
    [Route(template: "api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private readonly IDbService dbService;

        public StudentsController(IDbService db)
        {
            dbService = db;
        }

        [HttpGet]
        public string GetStudents(string orderBy)
        {
            return $"Kowalski, Malewski, Andrzejewski, Leontiev sortowanie={orderBy}";
        }

        [HttpGet("{id}")]
        public IActionResult GetStudents(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski");
            } else if (id == 2)
            {
                return Ok("Majewski");
            }
            return NotFound("Nie znaleziono studenta!");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut]
        public IActionResult ModifyStudents(int id)
        {
            if(id == 1)
            {
                return Ok("Kowalski zmieniony na Majewski");
            }else if(id == 2)
            {
                return Ok("Majewski zmieniony na Kowalski");
            }

            return NotFound("Nie znaleziono studenta!");
        }

        [HttpDelete]
        public IActionResult DeleteStudents(int id)
        {
            if (id == 1)
            {
                return Ok("Kowalski usuniety");
            }
            else if (id == 2)
            {
                return Ok("Majewski usuniety");
            }

            return NotFound("Nie znaleziono studenta!");
        }


    }
}