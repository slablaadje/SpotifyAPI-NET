using System.Collections.Generic;
using System.Net;

namespace SpotifyAPI.Web.Models
{
    public class ResponseInfo
    {
        public Dictionary<string, string> Headers { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public static readonly ResponseInfo Empty = new ResponseInfo();
    }
}