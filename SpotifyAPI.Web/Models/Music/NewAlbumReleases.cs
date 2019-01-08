using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Paging;

namespace SpotifyAPI.Web.Models.Music
{
    public class NewAlbumReleases : BasicModel
    {
        [JsonProperty("albums")]
        public ItemPaging<SimpleAlbum> Albums { get; set; }
    }
}