using Cw3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> students;

        static MockDbService()
        {
            students = new List<Student>
            {
                new Student(1, "Jan", "Kowalski"),
                new Student(2, "Tomasz", "Smyrak"),
                new Student(3, "Martyna", "Cwik"),

            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return students;
        }
    }
}
