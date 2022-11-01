using System.ComponentModel.DataAnnotations;

namespace MVC_Layer.Models
{
    public class UserDataViewModel
    {


        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string? Email { get; set; }
        [Required]
        [ MaxLength(15,ErrorMessage ="This is not a valid number")]
        public string MobileNumber { get; set; }
        [Required]
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Countery { get; set; }
        public string? City { get; set; }
        public string? itemsInJson { get; set; }

    }
}
