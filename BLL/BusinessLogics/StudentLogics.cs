using BLL.Models;
using DAL.Entities;
using DAL.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                _unitOfWork.GetRepository<Student>().InsertAsync(student);
                _unitOfWork.CommitAsync();
                check = true;
            }
            return check;
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            bool check = false;
            Student student = await _unitOfWork.GetRepository<Student>().FindByIdAsync(id);
            if (student != null)
            {
                student.Status = false;
                _unitOfWork.GetRepository<Student>().Update(student);
                await _unitOfWork.CommitAsync();
                check = true;
            }
            return check;
        }
        public List<Student> GetStudents()
        {
            List<Student> students = _unitOfWork.GetRepository<Student>().GetAll().ToList();
            return students;
        }

        public Student GetStudentById(Guid Id)
        {
            var student = _unitOfWork.GetRepository<Student>().GetAll().FirstOrDefault(s => s.Id.Equals(Id));
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
                        _unitOfWork.CommitAsync();
                        check = true;
                    }
                };

            }
            return check;
        }

    }

    public interface IStudentLogic {
        public int CountJobs();
        public bool CreateStudent(StudentCreateModel studentCreateModel);
        public Task<bool> DeleteStudent(Guid id);
        public List<Student> GetStudents();
        public Student GetStudentById(Guid Id);
        public bool UpdateStudent(Student student);
    }
}
