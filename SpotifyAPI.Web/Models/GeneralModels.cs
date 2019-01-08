using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyAPI.Web.Models
{
    public class DeleteTrackUri
    {
        /// <summary>
        ///     Delete-Track Wrapper
        /// </summary>
        /// <param name="uri">An Spotify-URI</param>
        /// <param name="positions">Optional positions</param>
        public DeleteTrackUri(string uri, params int[] positions)
        {
            Positions = positions.ToList();
            Uri = uri;
        }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("positions")]
        public List<int> Positions { get; set; }

        public bool ShouldSerializePositions()
        {
            return (Positions.Count > 0);
        }
    }
}