using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {

        public Student(int id, string fName, string lName)
        {
            this.idStudent = id;
            this.firstName = fName;
            this.lastName = lName;
        }
        public int idStudent { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string IndexNumber { get; set; }
    }
}
