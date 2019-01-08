using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models
{
    public class CollectionInfo
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
