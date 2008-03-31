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
            string url = null;
            e.InitParams.TryGetValue("Playlist", out playlist);
            e.InitParams.TryGetValue("Url", out url);
            if (playlist != null)
            {
                MultiPlayer player = new MultiPlayer();
                player.PlaylistUrl = playlist;
                this.RootVisual = player;
            }
            else
            {
                // Load the main control
                SimplePlayer simplePlayer = new SimplePlayer(); // new Page();
                this.RootVisual = simplePlayer;
                if(url != null)
                {
                    simplePlayer.Url = url;
                }
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
