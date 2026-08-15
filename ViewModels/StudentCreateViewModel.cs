using System.ComponentModel.DataAnnotations;

namespace Activity2.ViewModels
{
    public class StudentCreateViewModel
    {
        [Required] public string StudentNumber { get; set; } = string.Empty;
        [Required] public string FirstName { get; set; } = string.Empty;
        [Required] public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required] public string DateOfBirth { get; set; } = string.Empty;
    }
}
