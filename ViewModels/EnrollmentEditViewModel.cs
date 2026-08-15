using System.ComponentModel.DataAnnotations;

namespace Activity2.ViewModels
{
    public class EnrollmentEditViewModel
    {
        public int Id { get; set; }
        [Required] public int StudentId { get; set; }
        [Required] public int CourseId { get; set; }
        [Required] public string EnrollmentDate { get; set; } = string.Empty;
    }
}