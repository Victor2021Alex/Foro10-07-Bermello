using ConsoleApp1.Models;
using ConsoleApp1.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Net;
using System.Reflection.Metadata;

class Program
{
    static async Task Main(string[] args)
    {
        //agregarEstudiante();
        //consultarEstudiantes();
        //consultarEstudiante();
        //modificarEstudiante();
        //eliminarEstudiante();
        //consultarEstudiantesFunciones();
        //guardarEstudianteYdireccion();
        //guardarEstudianteYdireccionTransaction();
        //consultarDirecciones();
        //consultarDireccion();
        //consultarDireccion2();
        //guardarCurso();
        //guardarEstudianteCurso();
        //consultarAlumnosyCursos();
        await guardarEstudianteAsync();
        await guardarCursoAsync();
        await guardarEstudianteCursoAsync();
        await guardarEstudianteYdireccionAsync();

        //consultarAlumnosyCursos();
    }

    public static async Task guardarEstudianteAsync()
    {

        Console.WriteLine("Guardar Estudiantes desde la clase Repository");
        
        EstudianteRepository estudianteRepository= new EstudianteRepository();
        Student std = new Student();
        std.Name = "Lola";
        std.LastName = "Perez";
        await estudianteRepository.guardarEstudiante(std);
    }

    public static async Task guardarCursoAsync()
    {
        Console.WriteLine("Guardar cursos desde la clase Repository");
        CourseRepository cursoRepository = new CourseRepository();
        Course course = new Course();

        course.CourseName = "Tercero";
        await cursoRepository.guardarCurso(course);
    }
    public static async Task guardarEstudianteCursoAsync()
    {
        Console.WriteLine("Guardar EstudianteCurso desde la clase Repository");
        StudentCourseRepository StudentcursoRepository = new StudentCourseRepository();
        StudentCourse stdCourse = new StudentCourse();

        stdCourse.CourseId = 3;
        stdCourse.StudentId = 3; 

        await StudentcursoRepository.guardarStudentCurso(stdCourse);
    }
    public static async Task guardarEstudianteYdireccionAsync()
    {
        Console.WriteLine("Metodo agregar estudiante y direccion desde la clase Repository");
        StudentAddressRepository StudentDireccionRepository = new StudentAddressRepository();
        Student std = new Student();
        StudentAddress stdAddress = new StudentAddress();

        std.Name = "Ciri";

        stdAddress.Address1 = "direccion 1";
        stdAddress.Address2 = "direccion 2";
        stdAddress.StudentID = std.StudentId;
        stdAddress.City = "gye";
        stdAddress.State = "ecu";
        stdAddress.Student = std;

        await StudentDireccionRepository.guardarStudentAddress(stdAddress);

    }
    public static async Task guardarEstudianteYdireccionTransactionAsync()
    {
        Console.WriteLine("Metodo agregar estudiante y direccion desde la clase Repository");
        SchoolContext context = new SchoolContext();
        StudentAddressRepository StudentDireccionRepository = new StudentAddressRepository();
        Student std = new Student();
        StudentAddress stdAddress = new StudentAddress();
        var dbContextTransaction = context.Database.BeginTransaction();

        try
        {
            std.Name = "Karina";


            stdAddress.Address1 = "direccion 1";
            stdAddress.Address2 = "direccion 2";
            stdAddress.StudentID = std.StudentId;
            stdAddress.City = "gye";
            stdAddress.State = "ecu";

            await StudentDireccionRepository.guardarStudentAddress(stdAddress);
            dbContextTransaction.Commit();
            Console.WriteLine("Datos guardados con exito");


        }
        catch (Exception e)
        {
            dbContextTransaction.Rollback();
            Console.WriteLine("Error " + e.ToString());
        }


    }

    public static void guardarEstudianteYdireccionTransaction()
    {
        Console.WriteLine("Metodo agregar estudiante y direccion");

        SchoolContext context = new SchoolContext();
        Student std = new Student();
        StudentAddress stdAddress = new StudentAddress();
        var dbContextTransaction = context.Database.BeginTransaction();
        
        try
        {
            std.Name = "Karina";
            context.Students.Add(std);
            context.SaveChanges();

            stdAddress.Address1 = "direccion 1";
            stdAddress.Address2 = "direccion 2";
            stdAddress.StudentID = std.StudentId;
            stdAddress.City = "gye";
            stdAddress.State = "ecu";

            context.StudentAddresses.Add(stdAddress);

            context.SaveChanges();
            dbContextTransaction.Commit();
            Console.WriteLine("Datos guardados con exito");


        }
        catch (Exception e)
        {
            dbContextTransaction.Rollback();
            Console.WriteLine("Error "+ e.ToString());
        }
       

    }

    public static void consultarDirecciones()
    {
        Console.WriteLine("Consultar direcciones");
        //Console.WriteLine("Metodo consultar estudiante por Id");
        SchoolContext context = new SchoolContext();
        List<StudentAddress> listaDirecciones;
        listaDirecciones = context.StudentAddresses
            .Include(x=> x.Student)
            .ToList();
        
        foreach (var item in listaDirecciones)
        {
            Console.WriteLine("Codigo:"+ item.Student.StudentId +
                " Nombre: " + item.Student.Name + 
                " Direccion:" + item.Address1);
        }
        

    }

    public static void consultarDireccion()
    {
        Console.WriteLine("Consultar direccion por Id");
        //Console.WriteLine("Metodo consultar estudiante por Id");
        SchoolContext context = new SchoolContext();
        StudentAddress address = new StudentAddress();
        address = context.StudentAddresses
            .Where(x =>x.StudentID==16)
            .Include(x => x.Student)
            .ToList()[0];

        
        Console.WriteLine("Codigo: " + address.Student.StudentId +
                " Nombre: " + address.Student.Name +
                " Direccion: " + address.Address1);


    }

    public static void consultarDireccion2()
    {
        Console.WriteLine("Consultar direccion por Id, metodo 2");
        
        SchoolContext context = new SchoolContext();
        StudentAddress address = new StudentAddress();
        address = context.StudentAddresses
            .Single(x => x.StudentID == 16);
           

        context.Entry(address)
            .Reference(x => x.Student)
            .Load();

        /*
        context.Entry(address)
          .Collection(x => x.Student)
          .Load();
        */

        Console.WriteLine("Codigo: " + address.Student.StudentId +
                " Nombre: " + address.Student.Name +
                " Direccion: " + address.Address1);


    }
    public static void consultarAlumnosyCursos()
    {
        Console.WriteLine("Consultar un Alumnos y sus cursos con Include");

        SchoolContext context = new SchoolContext();
        List< StudentCourse> std;
        std = context.StudentCourses
            .Where(x => x.StudentId == 3)
            .Include(x => x.Course)
            .Include(x => x.Student)
            .ToList();


        Console.WriteLine("Cursos del estudiante " + std[0].StudentId + " " + std[0].Student.Name);

        foreach (var item in std)
        {
            Console.WriteLine("Curso: " + item.CourseId + " " + item.Course.CourseName);
        }


    }

    public static void consultarAlumnosyCursos2()
    {
        Console.WriteLine("Consultar un Alumno y sus cursos");

        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students
            .Single(x => x.StudentId == 3);

        context.Entry(std)
            .Reference(x => x.StudentAddress)
            .Load();

        context.Entry(std)
          .Collection(x => x.StudentCourse )
          .Load();

        for (int i = 0; i < 3; i++)
        {
            context.Entry(std.StudentCourse[i])
              .Reference(x => x.Course)
              .Load();
        }
        

        Console.WriteLine("Cursos del estudiante " + std.StudentId + " " + std.Name);
        
        foreach (var item in std.StudentCourse)
        {
            Console.WriteLine("Curso: " + item.CourseId + " " + item.Course.CourseName);
        }


    }


    public static void consultarEstudiantes()
    {
        Console.WriteLine("Metodo consultar estudiantes");
        SchoolContext context = new SchoolContext();
        List<Student> listEstudiantes= context.Students.ToList() ;

        foreach (var item in listEstudiantes)
        {
            Console.WriteLine("Codigo: " + item.StudentId + " Nombre: " + item.Name);
        }
        
    }

    public static void consultarEstudiante()
    {
        Console.WriteLine("Metodo consultar estudiante por Id");
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students.Find(11);

       Console.WriteLine("Codigo: " + std.StudentId + " Nombre: " + std.Name);
      
    }

    public static void modificarEstudiante()
    {
        Console.WriteLine("Metodo modificar estudiante");
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students.Find(1);

        std.Name = "Anahi";
        context.SaveChanges();
        Console.WriteLine("Codigo: " + std.StudentId + " Nombre: " + std.Name);

    }

    public static void eliminarEstudiante()
    {
        Console.WriteLine("Metodo eliminar estudiante");
        SchoolContext context = new SchoolContext();
        Student std = new Student();
        std = context.Students.Find(5);
        context.Remove(std);
        context.SaveChanges();
        Console.WriteLine("Codigo: " + std.StudentId + " Nombre: " + std.Name);

    }
    public static void consultarEstudiantesFunciones()
    {
        Console.WriteLine("Metodo consultar estudiantes con el uso de funciones");
        SchoolContext context = new SchoolContext();
        List<Student> listEstudiantes;

        Console.WriteLine("Cantidad de registros: " + context.Students.Count());
        Student std = context.Students.First();

        Console.WriteLine("Primer elemento de la tabla:" +  std.StudentId +"-" +std.Name);

        //listEstudiantes = context.Students.Where(s => s.StudentId > 2 && s.Name == "Anita").ToList();

        //listEstudiantes = context.Students.Where(s => s.Name == "Patty" || s.Name == "Anita").ToList();

        listEstudiantes = context.Students.Where(s => s.Name.StartsWith("A")).ToList();

        foreach (var item in listEstudiantes)
        {
            Console.WriteLine("Codigo: " + item.StudentId + " Nombre: " + item.Name);
        }


        /*
        var query = context.Students.GroupBy( s => s.Name) 
        .Select(g => new
        {
            Nombre = g.Key,
            Cantidad = g.Count()
        }). ToList();

        foreach (var result in query)
        {
            Console.WriteLine($"Nombre: {result.Nombre}, Cantidad: {result.Cantidad}");
        }
        */





    }
}