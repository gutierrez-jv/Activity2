using Activity2.Models.Database.Models;
using Activity2.Repositories;
namespace Activity2.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public void AddCourse(Course course)
        {
            if (string.IsNullOrWhiteSpace(course.CourseCode) || string.IsNullOrWhiteSpace(course.CourseName))
                throw new Exception("Course Code and Course Name are required.");

            var uniqueCode = _courseRepository.GetAllCourses()
                .Any(c => c.CourseCode == course.CourseCode);
            if (uniqueCode) throw new Exception($"Course Code '{course.CourseCode}' already exists.");

            _courseRepository.AddCourse(course);
        }

        public void DeleteCourse(int id)
        {
            var existingCourse = _courseRepository.GetById(id);
            if (existingCourse is null) throw new Exception($"Course with ID '{id}' does not exist.");
            _courseRepository.DeleteCourse(id);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }

        public Course? GetById(int id)
        {
            return _courseRepository.GetById(id);
        }

        public IEnumerable<Course> Search(string search)
        {
            return _courseRepository.Search(search);
        }

        public void UpdateCourse(Course course)
        {
            var existingCourse = _courseRepository.GetById(course.Id);
            if (existingCourse is null) throw new Exception($"Course with ID '{course.Id}' does not exist.");

            if (string.IsNullOrWhiteSpace(course.CourseCode) || string.IsNullOrWhiteSpace(course.CourseName))
                throw new Exception("Course Code and Course Name are required.");

            var uniqueCode = _courseRepository.GetAllCourses()
                .Any(c => c.CourseCode == course.CourseCode && c.Id != course.Id);
            if (uniqueCode) throw new Exception($"Course Code '{course.CourseCode}' already exists.");

            _courseRepository.UpdateCourse(course);
        }
    }
}