
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Entities
{
    public partial class Product
    {
        public Product()
        {
                Colors= new HashSet<Color>();
        }


        [Key]
        public int Id { get; set; }
   
        [StringLength(100)]
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
  
        [StringLength(2000)]
        public string? ProductDescription { get; set; }    
          
        public int? ProductCaregoryId { get; set; }
        [NotMapped]
        public IFormFile ImgUrl { get; set; }

        public string?  ImgName { get; set; }
     
        public bool IsActive { get; set; }

        [ForeignKey("ProductCaregoryId")]
        [InverseProperty("Products")]
        public virtual Category ProductCaregory { get; set; }
    
        [InverseProperty("Products")]
        public virtual ICollection<Color> Colors { get; set; }
        [InverseProperty("CartItemProduct")]
        public virtual CartItem ProductCartItem { get; set; }
    }
}