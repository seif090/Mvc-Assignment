using System.ComponentModel.DataAnnotations;

namespace company.web.Models
{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "email req")]
        [EmailAddress(ErrorMessage = "Invalid format for the email")]
        public string Email { get; set; }
    }
}
