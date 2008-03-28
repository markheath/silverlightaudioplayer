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
using System.Xml.Linq;

namespace AudioControls
{
    public partial class MultiPlayer : UserControl
    {
        string playlistUrl;

        public MultiPlayer()
        {
            InitializeComponent();
        }


        public string PlaylistUrl
        {
            get { return playlistUrl; }
            set 
            { 
                playlistUrl = value;
                WebClient downloader = new WebClient();
                downloader.DownloadStringCompleted += new DownloadStringCompletedEventHandler(downloader_DownloadStringCompleted);
                Uri playlistUri = new Uri(playlistUrl, UriKind.RelativeOrAbsolute);
                downloader.DownloadStringAsync(playlistUri);
            }
        }
        
        void downloader_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                LoadPlaylist(e.Result);
            }
            else
            {
                string s = e.Error.Message;
            }
        }

        void LoadPlaylist(string xml)
        {
            try
            {
                XDocument xmlPlaylist = XDocument.Parse(xml);
                var playlist = from audioFile in xmlPlaylist.Descendants("audiofile")
                               select new AudioFile
                               {
                                   Url = (string)audioFile.Attribute("url"),
                                   Title = (string)audioFile.Attribute("title"),
                                   Artist = (string)audioFile.Attribute("artist")
                               };
                playListBox.ItemsSource = playlist;
            }
            catch (Exception e)
            {
                
            }
        }

    }

    public class AudioFile
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
    }
}
