using Newtonsoft.Json;
using SpotifyAPI.Web.Models.Paging;
using System;
using System.Collections.Generic;

namespace SpotifyAPI.Web.Models.Paging
{
    public class CursorPaging<T> : BasicModel, IPaging
    {
        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("items")]
        public List<T> Items { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("cursors")]
        public Cursor Cursors { get; set; }

        [JsonProperty("total")]
        public int Total { get; set; }

        public int Offset
        {
            get
            {
                return 0;
            }
        }

        public bool HasNextPage()
        {
            return Next != null;
        }
    }
}