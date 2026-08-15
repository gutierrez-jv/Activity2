using Activity2.Models.Database;
using Activity2.Models.Database.Models;

namespace Activity2.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        public void AddCourse(Course course)
        {
            course.Id = StaticDatabase.Courses.Any() ? StaticDatabase.Courses.Max(c => c.Id) + 1 : 1;
            StaticDatabase.Courses.Add(course);
        }

        public void DeleteCourse(int id)
        {
            var course = GetById(id);
            if (course is null) return;
            StaticDatabase.Courses.Remove(course);
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return StaticDatabase.Courses;
        }

        public Course? GetById(int id)
        {
            return StaticDatabase.Courses.FirstOrDefault(c => c.Id == id);
        }

        public void UpdateCourse(Course course)
        {
            var existingCourse = GetById(course.Id);
            if (existingCourse is null) return;
            existingCourse.CourseCode = course.CourseCode;
            existingCourse.CourseName = course.CourseName;
            existingCourse.Units = course.Units;
        }

        public IEnumerable<Course> Search(string search)
        {
            return StaticDatabase.Courses.Where(c =>
                c.CourseCode.Contains(search) ||
                c.CourseName.Contains(search));
        }
    }
}
