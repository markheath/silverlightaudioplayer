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
using AudioControls;
using System.Net;

namespace TestHarness
{
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }

        private void Application_Startup(object sender, StartupEventArgs e)        
        {
            string playlist = null;
            if (!e.InitParams.TryGetValue("Playlist", out playlist))
            {
                playlist = "..\\playlist.xml";
            }

            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadStringAsync(new Uri(playlist, UriKind.RelativeOrAbsolute));
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                Playlist playlist = Playlist.LoadFromXml(e.Result);
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
            else
            {
                this.RootVisual = new Oops();
            }
        }

        private void Application_Exit(object sender, EventArgs e)
        {

        }
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {

        }
    }
}
