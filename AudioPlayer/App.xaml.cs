using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net;

namespace AudioPlayer
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string url = GetInitParam(e.InitParams, "Url", null);
            if (url == null)
            {
                // alternative key to allow us to pass parameters using WikiPlex (for CodePlex)                
                url = GetInitParam(e.InitParams, "Path", null);
            }
            bool autoPlay = bool.Parse(GetInitParam(e.InitParams, "AutoPlay", "False"));
            if(url != null)
            {
                string artist = GetInitParam(e.InitParams, "Artist", null);
                string title = GetInitParam(e.InitParams, "Title", null);
                Playlist playlist = new Playlist();
                playlist.Add(new PlaylistEntry() { Url = url, Artist = artist, Title = title });
                CreatePlayer(playlist);
            }
            else
            {
                string playlistUrl = GetInitParam(e.InitParams,"Playlist","../playlist.xml");
                BeginLoadPlaylist(playlistUrl);
            }

        }

        private static string GetInitParam(IDictionary<string,string> initParams, string key, string defaultValue)
        {
            string returnValue = null;
            if (!initParams.TryGetValue(key, out returnValue))
            {
                returnValue = defaultValue;
            }
            return returnValue;
        }

        private void BeginLoadPlaylist(string playlist)
        {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(playlist, UriKind.RelativeOrAbsolute));
        }
        
        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Playlist playlist = Playlist.LoadFromXml(e.Result);
                CreatePlayer(playlist);
            }
            else
            {
                this.RootVisual = new Oops();
            }
        }

        private void CreatePlayer(Playlist playlist)
        {
            if (playlist.Count > 1)
            {
                MultiPlayer player = new MultiPlayer();
                player.Playlist = playlist;
                this.RootVisual = player;
            }
            else
            {
                SimplePlayer simplePlayer = new SimplePlayer();
                foreach (PlaylistEntry entry in playlist)
                {
                    simplePlayer.PlaylistEntry = entry;
                    break;
                }
                this.RootVisual = simplePlayer;
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

        }
    }
}
