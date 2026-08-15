using Activity2.Models.Database.Models;

namespace Activity2.Repositories
{
    public interface IEnrollmentRepository
    {
        IEnumerable<Enrollment> GetAllEnrollments();
        Enrollment? GetById(int id);
        void AddEnrollment(Enrollment enrollment);
        void UpdateEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int id);
        IEnumerable<Enrollment> Search(string search);
    }
}
