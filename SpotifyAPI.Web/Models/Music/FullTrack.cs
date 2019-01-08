using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Artists;
using System;
using System.Collections.Generic;

namespace SpotifyAPI.Web.Models.Music
{
    public class FullTrack : UriModel
    {
        [JsonProperty("album")]
        public SimpleAlbum Album { get; set; }

        [JsonProperty("artists")]
        public List<SimpleArtist> Artists { get; set; }

        [JsonProperty("available_markets")]
        public List<string> AvailableMarkets { get; set; }

        [JsonProperty("disc_number")]
        public int DiscNumber { get; set; }

        [JsonProperty("duration_ms")]
        public int DurationMs { get; set; }

        [JsonProperty("explicit")]
        public Boolean Explicit { get; set; }

        [JsonProperty("external_ids")]
        public Dictionary<string, string> ExternalIds { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("popularity")]
        public int Popularity { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("track_number")]
        public int TrackNumber { get; set; }

        [JsonProperty("restrictions")]
        public Dictionary<string, string> Restrictions { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///     Only filled when the "market"-parameter was supplied!
        /// </summary>
        [JsonProperty("is_playable")]
        public Boolean? IsPlayable { get; set; }

        /// <summary>
        ///     Only filled when the "market"-parameter was supplied!
        /// </summary>
        [JsonProperty("linked_from")]
        public LinkedFrom LinkedFrom { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as FullTrack;

            if (other == null)
                return false;

            return Uri.Equals(other.Uri);
        }

        public override int GetHashCode()
        {
            return Uri.GetHashCode();
        }
    }
}