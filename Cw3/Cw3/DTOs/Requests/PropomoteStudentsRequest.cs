using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DTOs.Requests
{
    public class PropomoteStudentsRequest
    {
        [Required(ErrorMessage = "Nalezy podac nazwe studiow.")]
        public string Studies { get; set; }

        [Required(ErrorMessage = "Nalezy podac numer semestru.")]
        public int Semester { get; set; }
    }
}
