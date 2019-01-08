using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SpotifyAPI.Web.Models.Playlists
{
    public class SimplePlaylist : PlaylistBase
    {
        [JsonProperty("tracks")]
        public CollectionInfo TrackInfo { get; set; }
    }
}