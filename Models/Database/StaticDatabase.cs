using Activity2.Models.Database.Models;
namespace Activity2.Models.Database
{
    public class StaticDatabase
    {
        public static List<Course> Courses { get; } =
        [
            new Course
            {
                Id = 1,
                CourseCode = "MATH101",
                CourseName = "Basic Math Course",
                Units = 3
            },
            new Course
            {
                Id = 2,
                CourseCode = "SCI101",
                CourseName = "Basic Science Course",
                Units = 4
            },
            new Course
            {
                Id = 3,
                CourseCode = "HIST101",
                CourseName = "Basic History Course",
                Units = 3
            },
            new Course
            {
                Id = 4,
                CourseCode = "ENG101",
                CourseName = "Basic English Course",
                Units = 3
            },
            new Course
            {
                Id = 5,
                CourseCode = "PE101",
                CourseName = "Physical Education 1",
                Units = 2
            }
        ];

        public static List<Enrollment> Enrollments { get; } =
        [
            new Enrollment
            {
                Id = 1,
                StudentId = 1,
                CourseId = 1,
                EnrollmentDate = "2024-08-15"
            },
            new Enrollment
            {
                Id = 2,
                StudentId = 1,
                CourseId = 2,
                EnrollmentDate = "2024-08-16"
            },
            new Enrollment
            {
                Id = 3,
                StudentId = 2,
                CourseId = 3,
                EnrollmentDate = "2024-08-17"
            }
        ];

        public static List<Student> Students { get; } =
        [
            new Student
            {
                Id = 1,
                StudentNumber = "2024-0001",
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DateOfBirth = "2000-01-01"
            },
            new Student
            {
                Id = 2,
                StudentNumber = "2024-0002",
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane.smith@example.com",
                DateOfBirth = "2001-03-15"
            },
            new Student
            {
                Id = 3,
                StudentNumber = "2024-0003",
                FirstName = "Mark",
                LastName = "Reyes",
                Email = "mark.reyes@example.com",
                DateOfBirth = "1999-11-30"
            }
        ];
    }
}