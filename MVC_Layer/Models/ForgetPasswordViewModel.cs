using System.ComponentModel.DataAnnotations;

namespace MVC_Layer.Models

{
    public class ForgetPasswordViewModel
    {

        [Required(ErrorMessage = "you have to enter email")]
        [EmailAddress(ErrorMessage = "Please Enter A valid Email address")]
        public string Email { get; set; }
    }
}
