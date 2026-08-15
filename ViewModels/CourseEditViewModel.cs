using System.ComponentModel.DataAnnotations;

namespace Activity2.ViewModels
{
    public class CourseEditViewModel
    {
        public int Id { get; set; }
        [Required] public string CourseCode { get; set; } = string.Empty;
        [Required] public string CourseName { get; set; } = string.Empty;
        [Required, Range(1, 24, ErrorMessage = "Units must be between 1 and 24.")]
        public int Units { get; set; }
    }
}