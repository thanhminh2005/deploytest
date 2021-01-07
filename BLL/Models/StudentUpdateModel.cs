using System;

namespace BLL.Models
{
    public class StudentUpdateModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public int Age { get; set; }
    }
}
