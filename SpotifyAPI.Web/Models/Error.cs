using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models
{
    public class Error
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
