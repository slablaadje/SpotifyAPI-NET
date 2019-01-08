using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MoreLinq;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyFixer;

namespace SpotifyFixerCore
{
    public static class Program
    {
        private static readonly string _clientId = "daca7f1117144c8bbd1fa7fe0605e659"; //"";
        private static readonly string _secretId = "1357158baa434fab947405709481f9cc"; //"";
        private static readonly string serverUrl = "http://localhost:4002";
        private static bool authReceived;

        private static readonly string defaultPlaylistName = "..Espressivo";
        private static readonly string defaultPlaylistExpression = "50*+50*..Favorites";

        private static readonly string tokenFilename = "token.json";
        
        static void Main(string[] args)
        {
            CliHelper.ParseArguments(args);

            if (!args.Any())
            {
                CommandLineArguments.UpdatePlaylist = true;
                CommandLineArguments.PlaylistName = defaultPlaylistName;
                CommandLineArguments.Expression = defaultPlaylistExpression;
            }

            if (File.Exists(Path.Combine("Data", tokenFilename)))
            {
                try
                {
                    var token = JsonConvert.DeserializeObject<Token>(File.ReadAllText(Path.Combine("Data", tokenFilename)));
                    DoIt(token);
                    return;
                }
                catch (HttpException)
                {
                }
            }

            var scope = Scope.PlaylistReadPrivate | Scope.PlaylistReadCollaborative
                        | Scope.PlaylistModifyPrivate
                        | Scope.UserFollowRead | Scope.UserLibraryRead
                        | Scope.Streaming;

            var auth = new AuthorizationCodeAuth(_clientId, _secretId, serverUrl, serverUrl, scope);
            auth.AuthReceived += AuthOnAuthReceived;
            auth.Start();
            auth.OpenBrowser();

            while (!authReceived)
            {
                Thread.Sleep(500);
            }

            auth.Stop(0);
        }

        private static void DoIt(Token token)
        {
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };

            SpotifyHelper.DoIt(api);
        }

        private static async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        {
            var auth = (AuthorizationCodeAuth)sender;

            Token token = await auth.ExchangeCode(payload.Code);

            File.WriteAllText(Path.Combine("Data", tokenFilename), JsonConvert.SerializeObject(token));

            DoIt(token);
            
            auth.Stop();
            authReceived = true;
        }
    }
}