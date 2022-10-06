using System.ComponentModel.DataAnnotations;

namespace MVC_Layer.Models
{
    public class LogInViewModel
    {

        [Required(ErrorMessage = "you have to enter email")]
        [EmailAddress(ErrorMessage = "Please Enter A valid Email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "you have to enter password")]
     
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
