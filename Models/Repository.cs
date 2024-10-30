using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace StudentAdminSys.Models
{
    public class Repository : IRepository
    {
        private readonly StudentDbContext context;

        //public static IEnumerable<Student> Students => students;

        public Student GetStudentByEmail(string email)
        {
            return context.Students.FirstOrDefault(s => s.Email == email);
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = context.Students.FirstOrDefault(s => s.Email == student.Email);
            if (existingStudent != null)
            {
                existingStudent.Name = student.Name;
                existingStudent.Education = student.Education;
                existingStudent.SemesterId = student.SemesterId;
                existingStudent.Email = student.Email;

                context.SaveChanges();
            }
        }

        public static void EnsurePopulated(IApplicationBuilder app)
        {
            StudentDbContext context = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<StudentDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        public void AddStudent(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }
        
        public void RemoveStudent(string email)
        {
            var student = context.Students.Find(email);

            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
            }
        }

        public void UpdateStudent(string email)
        {
            var student = context.Students.Find(email);

        }

        public Repository(StudentDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Student> Students => context.Students;

    }
}