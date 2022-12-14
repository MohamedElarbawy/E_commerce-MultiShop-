// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CoreLayer.Entities
{
    public partial class Color
    {
        public Color()
        {
            Products = new HashSet<Product>();
            CartItems = new HashSet<CartItem>();
        }

        
        public int Id { get; set; }
        
        [StringLength(100)]
        public string ColorName { get; set; }

        [InverseProperty("Colors")]
        public virtual ICollection<Product> Products { get; set; }
        [InverseProperty("CartItemColor")]
        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}