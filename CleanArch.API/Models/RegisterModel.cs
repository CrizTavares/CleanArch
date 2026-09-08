using System.ComponentModel.DataAnnotations;

namespace CleanArch.API.Models
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The password do not match.")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}
