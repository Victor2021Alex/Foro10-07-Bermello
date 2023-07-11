using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    public class CourseRepository
    {
       
            private readonly SchoolContext _context = new SchoolContext();


            public async Task guardarCurso(Course course)
            {

                _context.Courses.Add(course);
                await _context.SaveChangesAsync();

            }
    }

}
