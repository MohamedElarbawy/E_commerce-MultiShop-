using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class CartItem
    {
       
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice{ get; set; }
        public string? Size { get; set; }
        public int? ColorId { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("ProductId")]
        [InverseProperty("ProductCartItem")]
        public virtual Product CartItemProduct { get; set; }

        [ForeignKey("OrderId")]
        [InverseProperty("CartItems")]
        public virtual Order CartItemOrder { get; set; }
        [ForeignKey("ColorId")]
        [InverseProperty("CartItems")]
        public virtual Color CartItemColor { get; set; }
    }
}
