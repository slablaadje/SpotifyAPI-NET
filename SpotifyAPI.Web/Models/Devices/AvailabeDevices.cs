using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyAPI.Web.Models.Devices
{
    public class AvailabeDevices : BasicModel
    {
        [JsonProperty("devices")]
        public List<Device> Devices { get; set; }
    }
}