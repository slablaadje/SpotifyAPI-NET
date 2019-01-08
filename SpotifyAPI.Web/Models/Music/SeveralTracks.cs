using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyAPI.Web.Models.Music
{
    public class SeveralTracks : BasicModel
    {
        [JsonProperty("tracks")]
        public List<FullTrack> Tracks { get; set; }
    }
}