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
using System.Windows.Browser;
using System.Net;

namespace AudioPlayer
{
    public partial class SimplePlayer : UserControl
    {
        private Brush rightCanvasBrush = new SolidColorBrush(Color.FromArgb(255, 0xCC, 0xCC, 0xCC));
        private Brush rightCanvasMouseOverBrush = new SolidColorBrush(Color.FromArgb(255, 0x99, 0x99, 0x99));
        private Brush iconBrush = new SolidColorBrush(Color.FromArgb(255, 0x66, 0x66, 0x66));
        private Brush iconMouseOverBrush = new SolidColorBrush(Colors.White);

        public SimplePlayer()
        {
            InitializeComponent();
        }

        private bool showingProgress;

        public void Page_Loaded(object sender, EventArgs args)
        {
            // Required to initialize variables
            InitializeComponent();

            //mediaElement.BufferingProgressChanged += new RoutedEventHandler(mediaElement_BufferingProgressChanged);
            mediaElement.CurrentStateChanged += new RoutedEventHandler(mediaElement_CurrentStateChanged);
            mediaElement.DownloadProgressChanged += new RoutedEventHandler(mediaElement_DownloadProgressChanged);
            mediaElement.MediaEnded += new RoutedEventHandler(mediaElement_MediaEnded);
            mediaElement.MediaFailed += mediaElement_MediaFailed;
            mediaElement.MediaOpened += new RoutedEventHandler(mediaElement_MediaOpened);
            positionUpdate.Completed += new EventHandler(positionUpdate_Completed);
            rightCanvas.MouseEnter += new MouseEventHandler(rightSection_MouseEnter);
            rightCanvas.MouseLeave += new MouseEventHandler(rightSection_MouseLeave);
            rightCanvas.MouseLeftButtonDown += new MouseButtonEventHandler(rightSection_MouseLeftButtonDown);
            audioPositionSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider2_ValueChanged);
            mediaElement_CurrentStateChanged(this, null);
            mediaElement_DownloadProgressChanged(this, null);
            HtmlPage.RegisterScriptableObject("Player", this);

        }

        void slider2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!showingProgress)
            {
                mediaElement.Position = TimeSpan.FromMilliseconds(audioPositionSlider.Value);
            }
        }

        void rightSection_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (mediaElement.CurrentState == MediaElementState.Playing)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        void rightSection_MouseLeave(object sender, MouseEventArgs e)
        {
            rightSection.Fill = rightCanvasBrush;
            playIcon.Fill = iconBrush;
            pauseIcon.Fill = iconBrush;
        }

        void rightSection_MouseEnter(object sender, MouseEventArgs e)
        {
            rightSection.Fill = rightCanvasMouseOverBrush;
            playIcon.Fill = iconMouseOverBrush;
            pauseIcon.Fill = iconMouseOverBrush;
        }

        void positionUpdate_Completed(object sender, EventArgs e)
        {
            ShowProgress();
            positionUpdate.Begin();
        }

        string FindTrackName()
        {
            if(mediaElement.Attributes.ContainsKey("Title"))
                return mediaElement.Attributes["Title"];
            return mediaElement.Source.ToString();
        }

        void mediaElement_MediaOpened(object sender, EventArgs e)
        {
            audioPositionSlider.Minimum = 0;
            audioPositionSlider.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
            //trackNameTextBlock.Text = FindTrackName();
            trackNameTextBlock.Text = playlistEntry.Title + " (" + playlistEntry.Artist + ")";

            TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
            timeTextBlock.Text = String.Format("{0:00}:{1:00}",
                (int) duration.TotalMinutes,
                duration.Seconds);
            //Play();
        }

        void mediaElement_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media Failed {0}",e);
            trackNameTextBlock.Text = e.ErrorException.Message;
        }

        void mediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media Ended");
            // apparently does not go into stop state by itself
            mediaElement.Stop();
        }

        void mediaElement_DownloadProgressChanged(object sender, RoutedEventArgs args)
        {
            //System.Diagnostics.Debug.WriteLine("Download Progress {0}", mediaElement.DownloadProgress);
            audioPositionSlider.DownloadPercent = mediaElement.DownloadProgress;
        }

        void mediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
        {
            // for some reason this is a string not an enum
            // Buffering, Closed, Error, Opening, Paused, Playing, or Stopped
            if (mediaElement.CurrentState == MediaElementState.Playing)
            {
                animatedSpeaker.StartAnimation();
                positionUpdate.Begin();
                pauseIcon.Visibility = Visibility.Visible;
                playIcon.Visibility = Visibility.Collapsed;
            }
            else
            {
                animatedSpeaker.StopAnimation();
                positionUpdate.Stop();
                pauseIcon.Visibility = Visibility.Collapsed;
                playIcon.Visibility = Visibility.Visible;

            }
        }

        private void ShowProgress()
        {
            try
            {
                showingProgress = true;
                audioPositionSlider.Value = mediaElement.Position.TotalMilliseconds;
            }
            finally
            {
                showingProgress = false;
            }
        }

        #region IAudioPlayer Members

        [ScriptableMember]
        public string Url
        {
            get
            {
                return mediaElement.Source.OriginalString;
            }
            set
            {
                try
                {
                    mediaElement.Stop();
                    mediaElement.Source = new Uri(value, UriKind.RelativeOrAbsolute);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error setting source {0}",ex);
                    trackNameTextBlock.Text = ex.Message;
                }
            }
        }

        private PlaylistEntry playlistEntry;

        public PlaylistEntry PlaylistEntry
        {
            get { return playlistEntry; }
            set
            {
                playlistEntry = value;
                Url = value.Url;
            }
        }

        [ScriptableMember]
        public void Play()
        {
            try
            {
                mediaElement.Play();
                expandPlayer.Begin();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Playing {0}", e);
            }
        }

        [ScriptableMember]
        public void Pause()
        {
            try
            {
                mediaElement.Pause();
                collapsePlayer.Begin();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error Pausing {0}", e);
            }
        }

        #endregion
    }
}
