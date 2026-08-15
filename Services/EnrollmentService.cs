using Activity2.Models.Database.Models;
using Activity2.Repositories;

namespace Activity2.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;

        public EnrollmentService(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            var student = _studentRepository.GetById(enrollment.StudentId);
            if (student is null)
                throw new Exception($"Student with ID '{enrollment.StudentId}' does not exist.");

            var course = _courseRepository.GetById(enrollment.CourseId);
            if (course is null)
                throw new Exception($"Course with ID '{enrollment.CourseId}' does not exist.");

            var alreadyEnrolled = _enrollmentRepository.GetAllEnrollments()
                .Any(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId);
            if (alreadyEnrolled)
                throw new Exception($"Student ID '{enrollment.StudentId}' is already enrolled in Course ID '{enrollment.CourseId}'.");

            var currentUnits = _enrollmentRepository.GetAllEnrollments()
                .Where(e => e.StudentId == enrollment.StudentId)
                .Sum(e => _courseRepository.GetById(e.CourseId)?.Units ?? 0);

            if (currentUnits + course.Units > 24)
                throw new Exception($"Enrolling in this course would exceed the 24-unit limit (current: {currentUnits}, course: {course.Units}).");

            if (DateTime.TryParse(enrollment.EnrollmentDate, out var enrollmentDate))
            {
                if (enrollmentDate.Date > DateTime.Today)
                    throw new Exception("Enrollment date cannot be in the future.");
            }
            else
            {
                throw new Exception("Enrollment date is not a valid date.");
            }

            _enrollmentRepository.AddEnrollment(enrollment);
        }

        public void DeleteEnrollment(int id)
        {
            var existing = _enrollmentRepository.GetById(id);
            if (existing is null)
                throw new Exception($"Enrollment with ID '{id}' does not exist.");

            _enrollmentRepository.DeleteEnrollment(id);
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return _enrollmentRepository.GetAllEnrollments();
        }

        public Enrollment? GetById(int id)
        {
            return _enrollmentRepository.GetById(id);
        }

        public IEnumerable<Enrollment> Search(string search)
        {
            return _enrollmentRepository.Search(search);
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            var existing = _enrollmentRepository.GetById(enrollment.Id);
            if (existing is null)
                throw new Exception($"Enrollment with ID '{enrollment.Id}' does not exist.");

            var student = _studentRepository.GetById(enrollment.StudentId);
            if (student is null)
                throw new Exception($"Student with ID '{enrollment.StudentId}' does not exist.");

            var course = _courseRepository.GetById(enrollment.CourseId);
            if (course is null)
                throw new Exception($"Course with ID '{enrollment.CourseId}' does not exist.");

            var duplicate = _enrollmentRepository.GetAllEnrollments()
                .Any(e => e.StudentId == enrollment.StudentId && e.CourseId == enrollment.CourseId && e.Id != enrollment.Id);
            if (duplicate)
                throw new Exception($"Student ID '{enrollment.StudentId}' is already enrolled in Course ID '{enrollment.CourseId}'.");

            var currentUnits = _enrollmentRepository.GetAllEnrollments()
                .Where(e => e.StudentId == enrollment.StudentId && e.Id != enrollment.Id)
                .Sum(e => _courseRepository.GetById(e.CourseId)?.Units ?? 0);

            if (currentUnits + course.Units > 24)
                throw new Exception($"Updating this enrollment would exceed the 24-unit limit (current: {currentUnits}, course: {course.Units}).");

            if (DateTime.TryParse(enrollment.EnrollmentDate, out var enrollmentDate)){
                if (enrollmentDate.Date > DateTime.Today)
                    throw new Exception("Enrollment date cannot be in the future.");
            }
            else throw new Exception("Enrollment date is not a valid date.");

            _enrollmentRepository.UpdateEnrollment(enrollment);
        }
    }
}