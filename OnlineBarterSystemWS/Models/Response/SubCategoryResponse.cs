using OnlineBarterSystemWS.Generic.Models.Response;

namespace OnlineBarterSystemWS.Models.Response
{
    public class SubCategoryResponse : AEntityResponse
    {
        public string Name { get; set; }
        public CategoryResponse Category { get; set; }
    }
}
