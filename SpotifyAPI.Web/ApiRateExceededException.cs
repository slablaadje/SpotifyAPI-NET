using System;
using System.Collections.Generic;
using System.Text;

namespace SpotifyAPI.Web
{
    public class ApiRateExceededException : Exception
    {
        public ApiRateExceededException()
            : base()
        {

        }
    }
}
