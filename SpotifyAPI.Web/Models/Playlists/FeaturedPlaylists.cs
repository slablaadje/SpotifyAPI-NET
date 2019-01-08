using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Paging;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class FeaturedPlaylists : BasicModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("playlists")]
        public ItemPaging<SimplePlaylist> Playlists { get; set; }
    }
}