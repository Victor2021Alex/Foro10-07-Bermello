using ConsoleApp1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Repositories
{
    public class EstudianteRepository
    {
        private readonly SchoolContext _context= new SchoolContext();

        
        public async Task guardarEstudiante(Student student)
        {
                       
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
 
        }
    }
}
