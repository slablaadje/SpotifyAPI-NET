using Newtonsoft.Json;
using System.Net;

namespace SpotifyAPI.Web.Models
{
    public class BasicModel
    {
        [JsonProperty("error")]
        public Error Error { get; set; }

        [JsonProperty("info")]
        public ResponseInfo Info;

        public bool HasError() => Error != null;
    }
}