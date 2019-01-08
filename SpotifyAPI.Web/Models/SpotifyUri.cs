using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models
{
    public class SpotifyUri
    {
        /// <summary>
        ///     Spotify Uri Wrapper
        /// </summary>
        /// <param name="uri">An Spotify-URI</param>
        public SpotifyUri(string uri)
        {
            Uri = uri;
        }

        [JsonProperty("uri")]
        public string Uri { get; set; }
    }
}
