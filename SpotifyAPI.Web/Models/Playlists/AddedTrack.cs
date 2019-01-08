using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Music;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class AddedTrack
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("track")]
        public FullTrack Track { get; set; }

        public override string ToString()
        {
            return string.Join(", ", Track.Artists.Select(a => a.Name)) + " - " + Track.Name;
        }
    }
}
