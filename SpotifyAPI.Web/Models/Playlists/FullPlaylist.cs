using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Paging;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class FullPlaylist : PlaylistBase
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("followers")]
        public CollectionInfo Followers { get; set; }

        [JsonProperty("tracks")]
        public ItemPaging<PlaylistTrack> Tracks { get; set; }
    }
}