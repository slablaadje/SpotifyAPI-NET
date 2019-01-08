using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web
{
    public class HttpException : Exception
    {
        public HttpException(int code)
            : base("Http error code " + code)
        {

        }
    }
}
