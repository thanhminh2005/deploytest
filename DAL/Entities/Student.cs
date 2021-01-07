using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace DAL.Entities
{
    public class Student
    {
        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
        [Required]
        public DateTime Createdate { get; set; }
        [Required]
        public bool Status { get; set; }

    }
}
