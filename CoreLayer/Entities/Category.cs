
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace CoreLayer.Entities
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }
       
        [StringLength(100)]
        public string CategoryName { get; set; }
        [StringLength(200)]
        public string? ImgName { get; set; }

        [InverseProperty("ProductCaregory")]
        public virtual ICollection<Product> Products { get; set; }
    }
}