using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SpotifyAPI.Web.Models.Profiles
{
    public class PrivateProfile : Profile
    {
        [JsonProperty("birthdate")]
        public string Birthdate { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }
    }
}