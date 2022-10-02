using System.ComponentModel.DataAnnotations;

namespace MVC_Layer.Models
{
    public class CategoryViewModel
    {

        [Key]
        public int Id { get; set; }
        //[Required]
        [StringLength(100)]
        public string? CategoryName { get; set; }
        [StringLength(200)]
        public string? ImgName { get; set; }

       
    }
}
