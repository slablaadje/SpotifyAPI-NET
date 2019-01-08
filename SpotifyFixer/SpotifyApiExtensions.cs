using SpotifyAPI.Web;
using SpotifyAPI.Web.Models.Categories;
using SpotifyAPI.Web.Models.Paging;
using SpotifyAPI.Web.Models.Playlists;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpotifyFixer
{
    public static class SpotifyApiExtensions
    {


        public static List<SimplePlaylist> GetAllCategoryPlaylists(this SpotifyWebAPI api, string categoryId)
        {
            var result = new List<SimplePlaylist>();

            int offset = 0;
            List<SimplePlaylist> batch;
            do
            {
                var page = api.GetCategoryPlaylists(categoryId, offset: offset);
                batch = page.Playlists.Items;
                result.AddRange(batch);
                offset += batch.Count;

                if (batch.Any())
                {
                    Console.WriteLine("Loaded playlists " + offset + " out of " + page.Playlists.Total);
                }

            } while (batch.Any());
            Console.WriteLine("Finished loading playlists " + result.Count);

            return result;
        }


        public static List<Category> GetAllCategories(this SpotifyWebAPI api)
        {
            var result = new List<Category>();

            int offset = 0;
            List<Category> categories;
            do
            {
                var page = api.GetCategories(offset: offset);
                categories = page.Categories.Items;
                result.AddRange(categories);
                offset += categories.Count;

                if (categories.Any())
                {
                    Console.WriteLine("Loaded categories " + offset + " out of " + page.Categories.Total);
                }

            } while (categories.Any());
            Console.WriteLine("Finished loading categories " + result.Count);

            return result;
        }

        public static List<SimplePlaylist> GetAllPlaylistsOfUser(this SpotifyWebAPI api, string userId)
        {
            var result = new List<SimplePlaylist>();

            int offset = 0;
            List<SimplePlaylist> playlists;
            do
            {
                var page = api.GetUserPlaylists(userId, offset: offset);
                playlists = page.Items;
                result.AddRange(playlists);
                offset += playlists.Count;

                if (playlists.Any())
                {
                    Console.WriteLine("Loaded playlists " + offset + " out of " + page.Total);
                }

            } while (playlists.Any());
            Console.WriteLine("Finished loading playlists " + result.Count);

            return result;
        }

        public static List<AddedTrack> GetAllPlaylistTracks(this SpotifyWebAPI api, PlaylistBase playlist, string fields = "", string market = "")
        {
            var result = new List<AddedTrack>();

            int offset = 0;
            List<AddedTrack> tracks;
            do
            {
                IPaging page;
                if (playlist.Id == "savedtracks")
                {
                    var savedTracks = api.GetSavedTracks(offset: offset, market: market);
                    tracks = savedTracks.Items;
                    page = savedTracks;
                }
                else
                {
                    var playlistTracks = api.GetPlaylistTracks(playlist.Id, fields, offset: offset, market: market);
                    tracks = playlistTracks.Items.Cast<AddedTrack>().ToList();
                    page = playlistTracks;
                }
                result.AddRange(tracks);
                offset += tracks.Count;

                if (tracks.Any() && offset % 500 == 0)
                {
                    Console.WriteLine("Loaded tracks: " + offset + " out of " + page.Total);
                }
            } while (tracks.Any());

            Console.WriteLine("Finished loading tracks " + result.Count);

            return result;
        }
    }
}
