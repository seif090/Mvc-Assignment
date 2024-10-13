using System.ComponentModel.DataAnnotations;

namespace company.web.Models
{
    public class ResetPasswordViewModel
    {
        public string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password must be at least 10 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{10,}$",
              ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Name is Required\"")]
        [Compare("Password", ErrorMessage = "confirm password doesnt match the password")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
