using Activity2.Models.Database.Models;

namespace Activity2.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<Enrollment> GetAllEnrollments();
        Enrollment? GetById(int id);
        void AddEnrollment(Enrollment enrollment);
        void UpdateEnrollment(Enrollment enrollment);
        void DeleteEnrollment(int id);
        IEnumerable<Enrollment> Search(string search);
    }
}
