using Activity2.Models.Database.Models;
using Activity2.Repositories;

namespace Activity2.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void AddStudent(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
                throw new Exception("First Name and Last Name are required.");

            var uniqueNumber = _studentRepository.GetAllStudents()
                .Any(s => s.StudentNumber == student.StudentNumber);
            if (uniqueNumber) throw new Exception($"Student Number '{student.StudentNumber}' already exists.");

            var uniqueEmail = _studentRepository.GetAllStudents()
                .Any(s => s.Email == student.Email);
            if (uniqueEmail) throw new Exception($"Email '{student.Email}' is already in use.");

            _studentRepository.AddStudent(student);
        }

        public void DeleteStudent(int id)
        {
            var existingStudent = _studentRepository.GetById(id);
            if (existingStudent is null) throw new Exception($"Student with ID '{id}' does not exist.");

            _studentRepository.DeleteStudent(id);
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _studentRepository.GetAllStudents();
        }

        public Student? GetById(int id)
        {
            return _studentRepository.GetById(id);
        }

        public IEnumerable<Student> Search(string search)
        {
            return _studentRepository.Search(search);
        }

        public void UpdateStudent(Student student)
        {
            var existingStudent = _studentRepository.GetById(student.Id);
            if (existingStudent is null) throw new Exception($"Student with ID '{student.Id}' does not exist.");

            if (string.IsNullOrWhiteSpace(student.FirstName) || string.IsNullOrWhiteSpace(student.LastName))
                throw new Exception("First Name and Last Name are required.");

            var uniqueNumber = _studentRepository.GetAllStudents()
                .Any(s => s.StudentNumber == student.StudentNumber && s.Id != student.Id);
            if (uniqueNumber) throw new Exception($"Student Number '{student.StudentNumber}' already exists.");

            var uniqueEmail = _studentRepository.GetAllStudents()
                .Any(s => s.Email == student.Email && s.Id != student.Id);
            if (uniqueEmail) throw new Exception($"Email '{student.Email}' is already in use.");

            _studentRepository.UpdateStudent(student);
        }
    }
}