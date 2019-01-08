using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models.Paging
{
    public class Cursor
    {
        [JsonProperty("after")]
        public string After { get; set; }

        [JsonProperty("before")]
        public string Before { get; set; }
    }
}
