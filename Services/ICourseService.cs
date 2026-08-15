using Activity2.Models.Database.Models;

namespace Activity2.Services
{
    public interface ICourseService
    {
        IEnumerable<Course> GetAllCourses();
        Course? GetById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
        IEnumerable<Course> Search(string search);
    }
}
