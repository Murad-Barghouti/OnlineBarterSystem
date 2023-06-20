using OnlineBarterSystemWS.Generic.Models.Response;

namespace OnlineBarterSystemWS.Models.Response
{
    public class ParentCategoryResponse : AEntityResponse
    {
        public string Name { get; set; }
        public List<ChildSubCategoryResponse> SubCategories { get; set; }
    }
}
