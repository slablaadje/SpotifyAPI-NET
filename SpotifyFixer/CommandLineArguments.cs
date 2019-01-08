using System;

namespace SpotifyFixer
{
    public static class CommandLineArguments
    {
        public static bool UpdatePlaylist;
        public static string PlaylistName;
        public static string Expression;
        public static UpdatePlaylistMode UpdatePlaylistMode;

        public static bool OutputToFile;
        public static string OutputFileName;

        public static bool ShowArtistsOfOwnedPlaylists;
        public static bool ShowGenres;
        public static bool ShowSpotifyPlaylists;
        public static bool ShowCategories;
        public static bool ShowDetailedCategories;
    }

    public enum UpdatePlaylistMode
    {
        Random,
        Split
    }

    public static class CliHelper
    {
        public static void ParseArguments(string[] args)
        {
            int i = 0;
            while (i < args.Length)
            {
                switch (args[i])
                {
                    case "-u":
                        CommandLineArguments.UpdatePlaylist = true;
                        i++;
                        CommandLineArguments.PlaylistName = args[i];
                        break;
                    case "-m":
                        i++;
                        CommandLineArguments.UpdatePlaylistMode = (UpdatePlaylistMode)Enum.Parse(typeof(UpdatePlaylistMode), args[i], true);
                        break;
                    case "-e":
                        i++;
                        CommandLineArguments.Expression = args[i];
                        break;
                    case "-g":
                        CommandLineArguments.ShowGenres = true;
                        break;
                    case "-c":
                        CommandLineArguments.ShowCategories = true;
                        break;
                    case "-C":
                        CommandLineArguments.ShowCategories = true;
                        CommandLineArguments.ShowDetailedCategories = true;
                        break;
                    case "-a":
                        CommandLineArguments.ShowArtistsOfOwnedPlaylists = true;
                        break;
                    case "-o":
                        CommandLineArguments.OutputToFile = true;
                        i++;
                        CommandLineArguments.OutputFileName = args[i];
                        break;
                    case "-s":
                        CommandLineArguments.ShowSpotifyPlaylists = true;
                        break;
                }
                i++;
            }
        }
    }
}
