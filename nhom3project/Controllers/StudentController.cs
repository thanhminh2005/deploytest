using BLL.BusinessLogics;
using BLL.Models;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nhom3project.Controllers
{
    [Route("api/students")]
    public class StudentController : Controller
    {

        private readonly IStudentLogic _studentLogic;

        public StudentController (StudentLogic studentLogic)
        {
            _studentLogic = studentLogic;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Student> students = _studentLogic.GetStudents();
            return Ok(students);
        }

        [HttpGet]
        public IActionResult GetOne(Guid Id)
        {
            Student student = _studentLogic.GetStudentById(Id);
            return Ok(student);
        }
        [HttpPost]
        public IActionResult Post([FromBody]StudentCreateModel studentCreateModel)
        {
            if(studentCreateModel == null)
            {
                return BadRequest("Error");
            }
            bool check = _studentLogic.CreateStudent(studentCreateModel);
            if (!check)
            {
                return BadRequest("Can not create new student");
            }
            return Ok("Success");
        }

        [HttpPut]
        public IActionResult Update([FromForm]StudentUpdateModel studentUpdateModel)
        {
            Student student = _studentLogic.GetStudentById(studentUpdateModel.Id);
            if (student == null)
            {
                return NoContent();
            }
            student.Name = studentUpdateModel.Name;
            student.Age = studentUpdateModel.Age;
            student.Class = studentUpdateModel.Class;
            _studentLogic.UpdateStudent(student);
            return Ok("Student Info Updated");
        }

        [HttpPut]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var check = await _studentLogic.DeleteStudent(Id);
            if (!check)
            {
                return BadRequest("Error: Remove failed");
            }
            return Ok("Student Removed");
        }
    }
}
