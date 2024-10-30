namespace StudentAdminSys.Models
{
    public interface IRepository
    {
        IQueryable<Student> Students { get; }

        void AddStudent(Student student);
        void RemoveStudent(string email);
        void UpdateStudent(Student student);
        Student GetStudentByEmail(string email);

    }
}
