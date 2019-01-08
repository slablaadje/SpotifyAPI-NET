using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web.Models.Paging
{
    public interface IPaging
    {
        string Href { get; }
        int Limit { get; }
        string Next { get; }
        int Offset { get; }
        int Total { get; }
        bool HasNextPage();
    }
}
