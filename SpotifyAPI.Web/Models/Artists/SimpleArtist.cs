using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SpotifyAPI.Web.Models.Artists
{
    public class SimpleArtist : UriModel
    {
        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}