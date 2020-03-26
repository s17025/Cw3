using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.Models
{
    public class Student
    {
       
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DataUrodzenia { get; set; }
        public string NameStudies { get; set; }
        public int NumSem { get; set; }
            

        public Student()
        {
        }
    }
}
