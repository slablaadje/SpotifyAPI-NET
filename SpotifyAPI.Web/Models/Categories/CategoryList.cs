using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Paging;

namespace SpotifyAPI.Web.Models.Categories
{
    public class CategoryList : BasicModel
    {
        [JsonProperty("categories")]
        public ItemPaging<Category> Categories { get; set; }
    }
}