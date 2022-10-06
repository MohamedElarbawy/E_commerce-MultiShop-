using System.ComponentModel.DataAnnotations;

namespace MVC_Layer.Models
{
    public class RegisterationViewModel
    {

        [Required (ErrorMessage ="you have to enter email")]
        [EmailAddress (ErrorMessage ="Please Enter A valid Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "you have to enter password")]
        [MinLength(6,ErrorMessage ="Minimum number of charcters is 6")]
        public string Password { get; set; }
        [Required(ErrorMessage = "you have to enter confirmed password")]
        [MinLength(6, ErrorMessage = "Minimum number of charcters is 6")]
        [Compare("Password",ErrorMessage ="Passwords are not Matching")]
        [Display(Name ="Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
}
