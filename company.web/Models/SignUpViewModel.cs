using System.ComponentModel.DataAnnotations;

namespace company.web.Models
{
    public class SignUpViewModel
    {

        [Required(ErrorMessage ="first Name is Required")]
        public  string FirstName { get; set; }

        [Required(ErrorMessage = "last Name is Required")]
        public  string LastName { get; set; }

        [Required(ErrorMessage = "Email Name is Required")]

        public  string Email { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Password must be at least 10 characters long.")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[\W_]).{10,}$",
              ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public  string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password Name is Required\"")]
        [Compare("Password", ErrorMessage="confirm password doesnt match the password")]
        public  string ConfirmPassword { get; set; }
        public  bool IsAgree { get; set; }
    }
}
