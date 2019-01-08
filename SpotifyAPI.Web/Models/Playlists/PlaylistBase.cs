using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class PlaylistBase : UriModel
    {
        [JsonProperty("collaborative")]
        public Boolean Collaborative { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("owner")]
        public Profile Owner { get; set; }

        [JsonProperty("public")]
        public Boolean Public { get; set; }

        [JsonProperty("snapshot_id")]
        public string SnapshotId { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
