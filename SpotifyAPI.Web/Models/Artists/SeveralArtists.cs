using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyAPI.Web.Models.Artists
{
    public class SeveralArtists : BasicModel
    {
        [JsonProperty("artists")]
        public List<FullArtist> Artists { get; set; }
    }
}