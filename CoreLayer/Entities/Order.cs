using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class Order
    {
        public Order()
        {
            CartItems=new HashSet<CartItem>();
        }

        [Key]
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
       
        [ForeignKey("UserId")]
        [InverseProperty("Orders")]
        public virtual User OrderUser { get; set; }
        [InverseProperty("CartItemOrder")]
        public virtual ICollection<CartItem> CartItems { get; set; }

    }
}
