using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cw3.DIOs.Requests
{
    public class EnrollStudentRequest
    {
        [Required(ErrorMessage = "Nalezy podac numer indeksu.")]
        [RegularExpression("^s[0-9]+$")]
        public string IndexNumber { get; set; }
        
        [Required(ErrorMessage = "Nalezy podac imie.")]
        [MaxLength(150)] //varchar 100
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Nalezy podac nazwisko.")]
        [MaxLength(150)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Nalezy podac date urodzenia.")]
        public string BirthDate { get; set; }
        
        [Required(ErrorMessage = "Nalezy podac nazwe studiow.")]
        public string Studies { get; set; }
    }
}
