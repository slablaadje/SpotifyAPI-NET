using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Artists;
using SpotifyAPI.Web.Models.Paging;

namespace SpotifyAPI.Web.Models.Library
{
    public class FollowedArtists : BasicModel
    {
        [JsonProperty("artists")]
        public CursorPaging<FullArtist> Artists { get; set; }
    }
}