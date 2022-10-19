using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Entities
{
    public class UserData
    {
        public UserData()
        {
            Orders= new HashSet<Order>();
        }
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Countery { get; set; }
        public string? City { get; set; }

        [InverseProperty("OrderUser")]
        public virtual ICollection<Order> Orders { get; set;}

    }
}
