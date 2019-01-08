using MoreLinq;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using SpotifyAPI.Web.Models.Artists;
using SpotifyAPI.Web.Models.Categories;
using SpotifyAPI.Web.Models.Music;
using SpotifyAPI.Web.Models.Playlists;
using SpotifyAPI.Web.Models.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SpotifyFixer
{
    public static class SpotifyHelper
    {
        public static SpotifyWebAPI Api;

        public static Dictionary<string, int> PlaylistSongs;
        public static HashSet<FullTrack> AllTracksOfOwnedPlaylists;
        public static Dictionary<SimpleArtist, HashSet<FullTrack>> AllArtistsOfOwnedPlaylists;

        private static StreamWriter OutputFile;

        public static void WriteLine(string value)
        {
            if (CommandLineArguments.OutputToFile)
                OutputFile.WriteLine(value);
            else
                Console.WriteLine(value);
        }

        private static List<Category> categories;

        public static List<Category> Categories
        {
            get
            {
                if (categories == null)
                {
                    categories = Api.GetAllCategories();
                }
                return categories;
            }
        }

        public static void DoIt(SpotifyWebAPI api)
        {
            var playlistsSpotifyLikedTracks = new Dictionary<LazyPlaylist, int>();
            try
            {
                Api = api;

                if (CommandLineArguments.OutputToFile)
                {
                    File.Delete(CommandLineArguments.OutputFileName);
                    OutputFile = File.AppendText(CommandLineArguments.OutputFileName);
                }

                AllTracksOfOwnedPlaylists = new HashSet<FullTrack>();
                AllArtistsOfOwnedPlaylists = new Dictionary<SimpleArtist, HashSet<FullTrack>>();
                PlaylistSongs = new Dictionary<string, int>();

                var profile = api.GetPrivateProfile();

                if (CommandLineArguments.UpdatePlaylist)
                {
                    PlaylistSongs = CommandLineArguments.Expression.Split('+').ToDictionary(
                        e =>
                        {
                            var split = e.Split('*');
                            return split.Count() > 1 ? split[1] : null;
                        },
                        e => int.Parse(e.Split('*')[0]));
                }

                // Get playlists owned by the user
                var playlists = api.GetAllPlaylistsOfUser(profile.Id).Select(p => new LazyPlaylist(p)).ToList();
                var savedTracks = new LazyPlaylist()
                {
                    Id = "savedtracks",
                    Name = "savedtracks",
                    Owner = profile,
                };
                playlists.Add(savedTracks);
                foreach (var playlist in playlists)
                {
                    playlist.TryDeserialize();
                }

                var ownedPlaylists = playlists.Where(p => p.Owner.Equals(profile));

                if (CommandLineArguments.ShowArtistsOfOwnedPlaylists || CommandLineArguments.ShowSpotifyPlaylists)
                {
                    AllTracksOfOwnedPlaylists = new HashSet<FullTrack>(ownedPlaylists.SelectMany(p => p.Tracks.Select(t => t.Track)));
                    Console.WriteLine("Done loading all tracks of owned playlists");

                    foreach (var ownedPlaylist in ownedPlaylists)
                    {
                        ownedPlaylist.Serialize();
                    }
                    Console.WriteLine("Done serializing owned playlists");
                }

                if (CommandLineArguments.ShowArtistsOfOwnedPlaylists)
                {
                    foreach (var track in AllTracksOfOwnedPlaylists)
                    {
                        foreach (var artist in track.Artists)
                        {
                            if (!AllArtistsOfOwnedPlaylists.ContainsKey(artist))
                            {
                                AllArtistsOfOwnedPlaylists[artist] = new HashSet<FullTrack>();
                            }
                            AllArtistsOfOwnedPlaylists[artist].Add(track);
                        }
                    }

                    var artistsOrderedByTrack = AllArtistsOfOwnedPlaylists.OrderByDescending(a => a.Value.Count);

                    foreach (var artist in artistsOrderedByTrack)
                    {
                        WriteLine("Artist:" + artist.Key.Name + " | Songs: " + artist.Value.Count);
                    }
                    Console.WriteLine("Done writing artists");
                }

                if (CommandLineArguments.ShowGenres)
                {
                    var genres = api.GetRecommendationSeedsGenres().Genres;
                    foreach (var genre in genres)
                    {
                        WriteLine("Genre: " + genre);
                    }
                    Console.WriteLine("Done writing genres");
                }

                if (CommandLineArguments.ShowCategories)
                {
                    foreach (var category in Categories)
                    {
                        WriteLine("Category: " + category.Id + " - " + category.Name);
                    }
                    Console.WriteLine("Done writing categories");
                }

                if (CommandLineArguments.ShowSpotifyPlaylists)
                {
                    var playlistsSpotify = api.GetAllPlaylistsOfUser("spotify").ToDictionary(p => p.Uri, p => new LazyPlaylist(p));
                    foreach(var category in Categories)
                    {
                        foreach(var categoryPlaylist in Api.GetAllCategoryPlaylists(category.Id))
                        {
                            if (category.Id == "audiobooks")
                                continue;

                            if (playlistsSpotify.ContainsKey(categoryPlaylist.Uri))
                            {
                                playlistsSpotify[categoryPlaylist.Uri].AddCategory(category);
                            }
                        }
                    }

                    Console.WriteLine("Loaded all playlists by Spotify");

                    var serialized = 0;
                    var deserialized = 0;
                    var outdated = 0;
                    foreach (var kvp in playlistsSpotify)
                    {
                        var playlist = kvp.Value;
                        var result = playlist.TryDeserialize();
                        playlistsSpotifyLikedTracks.Add(playlist, playlist.Tracks.Count(t => AllTracksOfOwnedPlaylists.Contains(t.Track)));
                        if (result == DeserializationResult.UpToDate || result == DeserializationResult.Outdated)
                        {
                            deserialized++;
                        }

                        if (result != DeserializationResult.UpToDate)
                        {
                            playlist.Serialize();
                            serialized++;
                        }
                        else
                        {
                            outdated++;
                        }
                    }
                    Console.WriteLine("Deserialized " + deserialized + " and serialized " + serialized + " playlists by Spotify, " + outdated + " playlists were outdated.");
                }

                //var rec = api.GetRecommendations(genreSeed: seedGenres.Genres.Take(1).ToList(), limit: 100);

                if (CommandLineArguments.UpdatePlaylist)
                {
                    PlaylistBase espressivoPlaylist = playlists.SingleOrDefault(p => p.Name == CommandLineArguments.PlaylistName);

                    // Ensure Espressivo exists
                    if (espressivoPlaylist == null)
                    {
                        espressivoPlaylist = api.CreatePlaylist(profile, CommandLineArguments.PlaylistName);
                        WriteLine("Created playlist " + CommandLineArguments.PlaylistName);
                    }
                    else
                    {
                        var tracks = api.GetAllPlaylistTracks(espressivoPlaylist);
                        api.RemovePlaylistTracks(profile.Id, espressivoPlaylist.Id, tracks.Select(t => new SpotifyUri(t.Track.Uri)).ToList());
                        WriteLine("Removed " + tracks.Count + " tracks from existing playlist " + CommandLineArguments.PlaylistName);
                    }

                    List<FullTrack> espressivoTracks = new List<FullTrack>();
                    foreach (var playlist in ownedPlaylists)
                    {
                        if (!PlaylistSongs.ContainsKey(playlist.Name))
                            continue;

                        var tracks = playlist.Tracks.Shuffle().Take(PlaylistSongs[playlist.Name])
                            .Select(t => t.Track).ToList();

                        var desc = playlist.Name;
                        if (desc == "")
                            desc = "saved tracks";
                        espressivoTracks.AddRange(tracks);
                        espressivoTracks = espressivoTracks.Shuffle().ToList();
                        WriteLine("Added " + tracks.Count + " tracks from " + desc + " to playlist " + CommandLineArguments.PlaylistName);
                    }
                    api.AddPlaylistTracks(profile.Id, espressivoPlaylist.Id, espressivoTracks.Select(t => t.Uri).ToList());
                }
            }
            catch(ApiRateExceededException)
            {
                Console.WriteLine("Api exception. Please try again in a moment.");
            }
            finally
            {
                if (CommandLineArguments.ShowSpotifyPlaylists)
                {
                    foreach (var playlist in playlistsSpotifyLikedTracks.OrderByDescending(p => p.Value).ThenByDescending(p => p.Key.Categories?.Count))
                    {
                        WriteLine("Spotify Playlist: " + playlist.Key.Name
                            + " | Tracks: " + playlist.Key.Tracks.Count
                            + " | TracksLiked: " + playlist.Value
                            + " | Categories: " + (playlist.Key.Categories == null ? "<none>" : string.Join(",", playlist.Key.Categories.Select(p => p.Name)))
                            );
                    }
                }

                if (OutputFile != null)
                {
                    OutputFile.Flush();
                    OutputFile.Close();
                }

                Console.WriteLine("Done!");
            }
        }
    }
}
