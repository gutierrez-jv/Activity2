using Activity2.Models.Database.Models;

namespace Activity2.Services
{
    public interface IStudentService
    {
        IEnumerable<Student> GetAllStudents();
        Student? GetById(int id);
        void AddStudent(Student student);
        void UpdateStudent(Student student);
        void DeleteStudent(int id);
        IEnumerable<Student> Search(string search);
    }
}
