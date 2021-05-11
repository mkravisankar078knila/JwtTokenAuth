using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace JwtToken.Models
{
    public class StudentList
    {
        [Key]
        public int Code { get; set; }
        public string StudentNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
    }
}


