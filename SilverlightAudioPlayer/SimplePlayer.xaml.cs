using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace SilverlightAudioPlayer
{
    [Scriptable]
    public partial class SimplePlayer : Canvas, IAudioPlayer
    {
        private bool showingProgress;

        public void Page_Loaded(object sender, EventArgs args)
        {
            // Required to initialize variables
            InitializeComponent();

            mediaElement.BufferingProgressChanged += new EventHandler(mediaElement_BufferingProgressChanged);
            mediaElement.CurrentStateChanged += new EventHandler(mediaElement_CurrentStateChanged);
            mediaElement.DownloadProgressChanged += new EventHandler(mediaElement_DownloadProgressChanged);
            mediaElement.MediaEnded += new EventHandler(mediaElement_MediaEnded);
            mediaElement.MediaFailed += new ErrorEventHandler(mediaElement_MediaFailed);
            mediaElement.MediaOpened += new EventHandler(mediaElement_MediaOpened);
            positionUpdate.Completed += new EventHandler(positionUpdate_Completed);
            rightCanvas.MouseEnter += new MouseEventHandler(rightSection_MouseEnter);
            rightCanvas.MouseLeave += new EventHandler(rightSection_MouseLeave);
            rightCanvas.MouseLeftButtonDown += new MouseEventHandler(rightSection_MouseLeftButtonDown);
            audioPositionSlider.ValueChanged += new EventHandler(slider2_ValueChanged);
            //tentpeg.mp3, 
            mediaElement_CurrentStateChanged(this, EventArgs.Empty);
            Url = "markheath+youhavealwaysgiven.mp3";
            mediaElement_DownloadProgressChanged(this, EventArgs.Empty);
            WebApplication.Current.RegisterScriptableObject("Player", this);           
        }

        void slider2_ValueChanged(object sender, EventArgs e)
        {
            if (!showingProgress)
            {
                mediaElement.Position = TimeSpan.FromMilliseconds(audioPositionSlider.Value);
            }
        }

        void rightSection_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (mediaElement.CurrentState == "Playing")
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        void rightSection_MouseLeave(object sender, EventArgs e)
        {
            //rightSection.Fill = new SolidColorBrush(Color.FromRgb(200, 200, 200));
        }

        void rightSection_MouseEnter(object sender, MouseEventArgs e)
        {
            // Causes a nasty exception - don't know why
            //rightSection.Fill = new SolidColorBrush(Color.FromRgb(100, 100, 100));
        }

        void positionUpdate_Completed(object sender, EventArgs e)
        {
            ShowProgress();
            positionUpdate.Begin();
        }


        string FindTrackName()
        {
            try
            {
                foreach (MediaAttribute attribute in mediaElement.Attributes)
                {
                    if (attribute.Name == "Title")
                    {
                        return attribute.Value;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                // TODO: why does asking for attributes throw ArgumentNullException
            }
            return mediaElement.Source.ToString();
        }

        void mediaElement_MediaOpened(object sender, EventArgs e)
        {
            audioPositionSlider.Range = new Silverlight.Samples.Controls.ValueRange(0, mediaElement.NaturalDuration.TimeSpan.TotalMilliseconds);
            trackNameTextBlock.Text = FindTrackName();
            TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
            timeTextBlock.Text = String.Format("{0:00}:{1:00}:{2:00}",
                duration.Hours,
                duration.Minutes,
                duration.Seconds);
            //Play();
        }

        void mediaElement_MediaFailed(object sender, ErrorEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media Failed {0}-{1}-{2}", 
                e.ErrorCode,e.ErrorMessage,e.ErrorType);

            trackNameTextBlock.Text = e.ErrorMessage;
        }

        void mediaElement_MediaEnded(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Media Ended");
            // apparently does not go into stop state by itself
            mediaElement.Stop();
        }

        void mediaElement_DownloadProgressChanged(object sender, EventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("Download Progress {0}", mediaElement.DownloadProgress);
            audioPositionSlider.DownloadPercent = mediaElement.DownloadProgress;
        }

        void mediaElement_CurrentStateChanged(object sender, EventArgs e)
        {
            // for some reason this is a string not an enum
            // Buffering, Closed, Error, Opening, Paused, Playing, or Stopped
            if (mediaElement.CurrentState == "Playing")
            {
                animatedSound.Visibility = Visibility.Visible;
                soundAnimation.Begin();
                positionUpdate.Begin();
                pauseIcon.Visibility = Visibility.Visible;
                playIcon.Visibility = Visibility.Collapsed;
            }
            else
            {
                animatedSound.Visibility = Visibility.Collapsed;
                soundAnimation.Stop();
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

        void mediaElement_BufferingProgressChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Buffering Progress Changed {0}", mediaElement.BufferingProgress);
        }

        #region IAudioPlayer Members

        [Scriptable]
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

        [Scriptable]
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

        [Scriptable]
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
