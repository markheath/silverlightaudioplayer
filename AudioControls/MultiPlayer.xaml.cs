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
        public MultiPlayer()
        {
            InitializeComponent();
            mediaElement.CurrentStateChanged += new RoutedEventHandler(mediaElement_CurrentStateChanged);
            mediaElement.DownloadProgressChanged += new RoutedEventHandler(mediaElement_DownloadProgressChanged);
            mediaElement.MediaEnded += new RoutedEventHandler(mediaElement_MediaEnded);
            mediaElement.MediaFailed += mediaElement_MediaFailed;
            mediaElement.MediaOpened += new RoutedEventHandler(mediaElement_MediaOpened);
        }


        public IEnumerable<PlaylistEntry> Playlist
        {
            set { playListBox.ItemsSource = value; }
        }
        
        private void playListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlaylistEntry playlistEntry = (PlaylistEntry)playListBox.SelectedItem;

            SetCurrentEntry(playlistEntry);
        }

        bool wasPlaying = false;
        void SetCurrentEntry(PlaylistEntry playlistEntry)
        {
            wasPlaying = (mediaElement.CurrentState == MediaElementState.Playing);
            mediaElement.Stop();
            mediaElement.Source = new Uri(playlistEntry.Url, UriKind.RelativeOrAbsolute);
            titleScroller.Text = playlistEntry.Title + " (" + playlistEntry.Artist + ")";
            progressBar.Value = mediaElement.DownloadProgress;
        }

        void mediaElement_MediaOpened(object sender, EventArgs e)
        {
            if (wasPlaying)
            {
                mediaElement.Play();
            }
            //audioPositionSlider.Minimum = 0;
            //audioPositionSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            //trackNameTextBlock.Text = FindTrackName();
            //TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
            //timeTextBlock.Text = String.Format("{0:00}:{1:00}",
             //   (int)duration.TotalMinutes,
             //   duration.Seconds);
            //Play();
        }

        void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media Failed {0}", e);
            //trackNameTextBlock.Text = e.ErrorException.Message;
        }

        void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            // apparently does not go into stop state by itself
            //mediaElement.Stop();
        }

        void mediaElement_DownloadProgressChanged(object sender, RoutedEventArgs args)
        {
            progressBar.Value = mediaElement.DownloadProgress;
        }

        void mediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            if (mediaElement.CurrentState == MediaElementState.Playing)
            {
                //positionUpdate.Begin();
            }
            else
            {
                //positionUpdate.Stop();
            }
        }

        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Play();
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            if (playListBox.SelectedIndex < playListBox.Items.Count - 1)
                playListBox.SelectedIndex++;

        }

        private void buttonPrev_Click(object sender, RoutedEventArgs e)
        {
            if (playListBox.SelectedIndex > 0)
                playListBox.SelectedIndex--;

        }

        private void buttonPause_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Pause();
        }

        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            mediaElement.Volume = sliderVolume.Value;
        }

    }


}
