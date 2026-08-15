using Activity2.Models.Database.Models;

namespace Activity2.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        Course? GetById(int id);
        IEnumerable<Course> Search(string search);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
    }
}
