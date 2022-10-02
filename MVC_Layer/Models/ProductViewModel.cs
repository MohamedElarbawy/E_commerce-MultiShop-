using System.ComponentModel.DataAnnotations;

namespace MVC_Layer.Models
{
    public class ProductViewModel
    {
        [Key]
        public int? Id { get; set; }
        //[Required]
        [StringLength(100)]
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
        //[Required]
        [StringLength(1000)]
        public string? ProductDescription { get; set; }
        [StringLength(50)]
        public string? ProductSize { get; set; }
        public int? ProductColorId { get; set; }
        public int? ProductCaregoryId { get; set; }
        [StringLength(200)]
        public string? ImgName { get; set; }
        public bool IsActive { get; set; }

    }
}
