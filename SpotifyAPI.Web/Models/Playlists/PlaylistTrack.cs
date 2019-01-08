using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Profiles;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class PlaylistTrack : AddedTrack
    {
        [JsonProperty("added_by")]
        public Profile AddedBy { get; set; }

        [JsonProperty("is_local")]
        public bool IsLocal { get; set; }
    }
}
