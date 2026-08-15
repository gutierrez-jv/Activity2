using Activity2.Models.Database;
using Activity2.Models.Database.Models;
namespace Activity2.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public Student? GetById(int id)
        {
            return StaticDatabase.Students.FirstOrDefault(s => s.Id == id);
        }

        public void AddStudent(Student student)
        {
            student.Id = StaticDatabase.Students.Any() ? StaticDatabase.Students.Max(s => s.Id) + 1 : 1;
            StaticDatabase.Students.Add(student);
        }

        public void DeleteStudent(int id)
        {
            var student = GetById(id);
            if (student is null) return;
            StaticDatabase.Students.Remove(student);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return StaticDatabase.Students;
        }

        public IEnumerable<Student> Search(string search)
        {
            return StaticDatabase.Students.Where(s =>
                s.FirstName.Contains(search) ||
                s.LastName.Contains(search) ||
                s.Email.Contains(search));
        }

        public void UpdateStudent(Student student)
        {
            var existing = GetById(student.Id);
            if (existing is null) return;

            existing.StudentNumber = student.StudentNumber;
            existing.FirstName = student.FirstName;
            existing.LastName = student.LastName;
            existing.Email = student.Email;
            existing.DateOfBirth = student.DateOfBirth;
        }
    }
}