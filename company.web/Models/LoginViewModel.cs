using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace company.web.Models
{
    public class LoginViewModel 
    {


        [Required]
        [EmailAddress(ErrorMessage = "Invalid format for the email")]
        public string Email { get; set; }


        [Required(ErrorMessage ="password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
