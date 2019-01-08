using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyAPI.Web.Models.Music
{
    public class SeveralAlbums : BasicModel
    {
        [JsonProperty("albums")]
        public List<FullAlbum> Albums { get; set; }
    }
}