using Activity2.Models.Database;
using Activity2.Models.Database.Models;

namespace Activity2.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        public void AddEnrollment(Enrollment enrollment)
        {
            enrollment.Id = StaticDatabase.Enrollments.Any() ? StaticDatabase.Enrollments.Max(e => e.Id) + 1 : 1;
            StaticDatabase.Enrollments.Add(enrollment);
        }

        public void DeleteEnrollment(int id)
        {
            var enrollment = GetById(id);
            if (enrollment is null) return;
            StaticDatabase.Enrollments.Remove(enrollment);
        }

        public IEnumerable<Enrollment> GetAllEnrollments()
        {
            return StaticDatabase.Enrollments;
        }

        public Enrollment? GetById(int id)
        {
            return StaticDatabase.Enrollments.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<Enrollment> Search(string search)
        {
            return StaticDatabase.Enrollments.Where(e =>
               e.StudentId.ToString().Contains(search) ||
               e.CourseId.ToString().Contains(search) ||
               e.EnrollmentDate.Contains(search));
        }

        public void UpdateEnrollment(Enrollment enrollment)
        {
            var existingEnrollment = GetById(enrollment.Id);
            if (existingEnrollment is null) return;
            existingEnrollment.StudentId = enrollment.StudentId;
            existingEnrollment.CourseId = enrollment.CourseId;
            existingEnrollment.EnrollmentDate = enrollment.EnrollmentDate;
        }
    }
}
