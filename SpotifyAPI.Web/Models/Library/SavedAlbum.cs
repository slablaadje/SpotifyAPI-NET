using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Music;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models.Library
{
    public class SavedAlbum
    {
        [JsonProperty("added_at")]
        public DateTime AddedAt { get; set; }

        [JsonProperty("album")]
        public FullAlbum Album { get; set; }
    }
}
