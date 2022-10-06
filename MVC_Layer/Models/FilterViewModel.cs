namespace MVC_Layer.Models
{
    public class FilterViewModel
    {
        
        List<string> Colors { get; set; }
        decimal? MaxPrice { get; set; }
        decimal? MinPrice { get; set; }
    }
}
