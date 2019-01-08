using Newtonsoft.Json;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Models.Categories;
using SpotifyAPI.Web.Models.Playlists;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpotifyFixer
{
    public class LazyPlaylist : PlaylistBase
    {
        [JsonProperty("track_info")]
        public CollectionInfo TrackInfo;

        [JsonIgnore]
        public List<AddedTrack> Tracks
        {
            get
            {
                if (tracks == null)
                {
                    tracks = SpotifyHelper.Api.GetAllPlaylistTracks(this);
                }

                return tracks;
            }
        }

        public List<Category> Categories;

        public void AddCategory(Category category)
        {
            if (Categories == null)
                Categories = new List<Category>();

            Categories.Add(category);
        }

        public string Group { get; set; }

        [JsonProperty("tracks")]
        private List<AddedTrack> tracks;

        public LazyPlaylist()
        {
            Categories = new List<Category>();
        }

        public LazyPlaylist(SimplePlaylist simplePlaylist)
            : base()
        {
            SetByPlaylistBase(simplePlaylist);
            TrackInfo = simplePlaylist.TrackInfo;
        }

        private void SetByPlaylistBase(PlaylistBase playlist)
        {
            Collaborative = playlist.Collaborative;
            ExternalUrls = playlist.ExternalUrls?.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            Href = playlist.Href;
            Id = playlist.Id;
            Images = playlist.Images?.ToList();
            Name = playlist.Name;
            Owner = playlist.Owner;
            Public = playlist.Public;
            SnapshotId = playlist.SnapshotId;
            Type = playlist.Type;
            Uri = playlist.Uri;
            Error = playlist.Error;
            Info = playlist.Info;
        }

        [JsonIgnore]
        public string FileName => Path.Combine("Data", Id + ".json");

        public void Serialize()
        {
            if (Id == "savedtracks")
                return;

            File.WriteAllText(FileName, JsonConvert.SerializeObject(this, Formatting.Indented));
        }

        public DeserializationResult TryDeserialize()
        {
            if (Id == "savedtracks")
                return DeserializationResult.CantDeserializeSavedTracks;

            if (!File.Exists(FileName))
                return DeserializationResult.NoFile;

            try
            {
                var d = JsonConvert.DeserializeObject<LazyPlaylist>(File.ReadAllText(FileName));
                
                if (SnapshotId != d.SnapshotId)
                {
                    return DeserializationResult.Outdated;
                }

                SetByPlaylistBase(d);
                tracks = d.Tracks;
                TrackInfo = d.TrackInfo;

                return DeserializationResult.UpToDate;
            }
            catch
            {
                return DeserializationResult.Failed;
            }
        }

        public override string ToString()
        {
            return this.Uri + " | " + this.Name;
        }
    }
}
