using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Paging;
using SpotifyAPI.Web.Models.Playlists;

namespace SpotifyAPI.Web.Models.Categories
{
    public class CategoryPlaylist : BasicModel
    {
        [JsonProperty("playlists")]
        public ItemPaging<SimplePlaylist> Playlists { get; set; }
    }
}