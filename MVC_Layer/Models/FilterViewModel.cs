using Microsoft.AspNetCore.Mvc;

namespace MVC_Layer.Models
{
    public class FilterViewModel
    {

        [FromQuery(Name="color")]
       public List<string> Colors { get; set; }
       
        public decimal? MaxPrice { get; set; }
        public decimal? MinPrice { get; set; }



       
    }
}
