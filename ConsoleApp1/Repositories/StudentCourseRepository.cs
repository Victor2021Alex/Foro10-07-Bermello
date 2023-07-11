using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    public class StudentCourseRepository
    {

        private readonly SchoolContext _context = new SchoolContext();


        public async Task guardarStudentCurso(StudentCourse studentCourse)
        {

            _context.StudentCourses.Add(studentCourse);
            await _context.SaveChangesAsync();

        }
    }
}
