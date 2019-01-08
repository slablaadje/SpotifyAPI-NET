using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Artists;
using SpotifyAPI.Web.Models.Music;
using SpotifyAPI.Web.Models.Paging;
using SpotifyAPI.Web.Models.Playlists;

namespace SpotifyAPI.Web.Models
{
    public class SearchItem : BasicModel
    {
        [JsonProperty("artists")]
        public ItemPaging<FullArtist> Artists { get; set; }

        [JsonProperty("albums")]
        public ItemPaging<SimpleAlbum> Albums { get; set; }

        [JsonProperty("tracks")]
        public ItemPaging<FullTrack> Tracks { get; set; }

        [JsonProperty("playlists")]
        public ItemPaging<SimplePlaylist> Playlists { get; set; } 
    }
}