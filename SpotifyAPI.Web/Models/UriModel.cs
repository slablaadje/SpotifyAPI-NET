using Newtonsoft.Json;

namespace SpotifyAPI.Web.Models
{
    public abstract class UriModel : BasicModel
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as UriModel;

            if (other == null)
                return false;

            if (Uri == null)
                return other.Uri == null;

            return Uri.Equals(other.Uri);
        }

        public override int GetHashCode()
        {
            if (Uri == null)
                return 0;

            return Uri.GetHashCode();
        }
    }
}
