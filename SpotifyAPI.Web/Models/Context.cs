using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models
{
    public class Context
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
