using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem.Models
{
    public class RegisterViewModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "Address")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        [RegularExpression(@".*\S+.*",ErrorMessage = "Email cannot be empty or whitespace.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression(@".*\S+.*", ErrorMessage = "Password cannot be empty or whitespace.")]
        public string Password { get; set; }
    }
}
