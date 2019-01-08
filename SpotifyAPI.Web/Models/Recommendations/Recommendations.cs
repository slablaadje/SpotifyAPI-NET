using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Music;

namespace SpotifyAPI.Web.Models.Recommendations
{
    public class Recommendations : BasicModel
    {
        [JsonProperty("seeds")]
        public List<RecommendationSeed> Seeds { get; set; }

        [JsonProperty("tracks")]
        public List<SimpleTrack> Tracks { get; set; }
    }
}