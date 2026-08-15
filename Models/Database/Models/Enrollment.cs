namespace Activity2.Models.Database.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string EnrollmentDate { get; set; }
    }
}
