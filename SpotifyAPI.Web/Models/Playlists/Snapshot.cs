using Newtonsoft.Json;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class Snapshot : BasicModel
    {
        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; }
    }
}