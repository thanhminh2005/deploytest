using BLL.Models;
using DAL.Entities;
using DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL.BusinessLogics
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IUnitOfWork _unitOfWork;
        public StudentLogic(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CountJobs()
        {
            int count = _unitOfWork.GetRepository<Student>().GetAll().Where(s => s.Status == true).Count();
            return count;
        }

        public bool CreateStudent(StudentCreateModel studentCreateModel)
        {
            bool check = false;
            if (studentCreateModel != null)
            {
                Student student = new Student
                {
                    Id = Guid.NewGuid(),
                    Name = studentCreateModel.Name,
                    Class = studentCreateModel.Class,
                    Age = studentCreateModel.Age,
                    Createdate = DateTime.UtcNow,
                    Status = true
                };
                _unitOfWork.GetRepository<Student>().Insert(student);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }

        public bool DeleteStudent(Guid id)
        {
            bool check = false;
            Student student = _unitOfWork.GetRepository<Student>().FindById(id);
            if (student != null)
            {
                if (student.Status == false)
                {
                    student.Status = true;
                }
                else
                {
                    student.Status = false;
                }
                _unitOfWork.GetRepository<Student>().Update(student);
                _unitOfWork.Commit();
                check = true;
            }
            return check;
        }
        public List<Student> GetStudents()
        {
            List<Student> students = _unitOfWork.GetRepository<Student>().GetAll().Where(s => s.Status == true).ToList();
            return students;
        }

        public Student GetStudentById(Guid Id)
        {
            var student = _unitOfWork.GetRepository<Student>().GetAll().FirstOrDefault(s => s.Id.Equals(Id) && s.Status == true);
            return student;
        }

        public bool UpdateStudent(Student student)
        {
            bool check = false;
            if (student != null)
            {
                var _student = _unitOfWork.GetRepository<Student>().GetAll().FirstOrDefault(s => student.Id.Equals(s.Id));
                {
                    if (_student != null)
                    {
                        _student.Name = student.Name;
                        _student.Age = student.Age;
                        _student.Class = student.Class;
                        _unitOfWork.GetRepository<Student>().Update(_student);
                        _unitOfWork.Commit();
                        check = true;
                    }
                };

            }
            return check;
        }

    }

    public interface IStudentLogic
    {
        public int CountJobs();
        public bool CreateStudent(StudentCreateModel studentCreateModel);
        public bool DeleteStudent(Guid id);
        public List<Student> GetStudents();
        public Student GetStudentById(Guid Id);
        public bool UpdateStudent(Student student);
    }
}
