using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightAudioPlayer
{
    public partial class SimplePlayer : Canvas
    {
        public void Page_Loaded(object o, EventArgs e)
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
            rightSection.MouseEnter += new MouseEventHandler(rightSection_MouseEnter);
            rightSection.MouseLeave += new EventHandler(rightSection_MouseLeave);
            rightSection.MouseLeftButtonDown += new MouseEventHandler(rightSection_MouseLeftButtonDown);
            try
            {
                mediaElement.Source = new Uri("markheath+youhavealwaysgiven.mp3",UriKind.Relative);

                //mediaElement.Source = new Uri("http://www.wordandspirit.co.uk/music/tentpeg.mp3");
            }
            catch (Exception ex)
            {
                trackNameTextBlock.Text = ex.Message;
            }
            mediaElement_DownloadProgressChanged(this, EventArgs.Empty);
        }

        void rightSection_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            if (mediaElement.CurrentState == "Playing")
            {
                mediaElement.Pause();
                collapsePlayer.Begin();
            }
            else
            {
                mediaElement.Play();
                expandPlayer.Begin();
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
            audioPositionSlider.Duration = mediaElement.NaturalDuration.TimeSpan;
            trackNameTextBlock.Text = FindTrackName();
            TimeSpan duration = mediaElement.NaturalDuration.TimeSpan;
            timeTextBlock.Text = String.Format("{0:00}:{1:00}:{2:00}",
                duration.Hours,
                duration.Minutes,
                duration.Seconds);
            mediaElement.Play();
        }

        void mediaElement_MediaFailed(object sender, ErrorEventArgs e)
        {
            trackNameTextBlock.Text = e.ErrorMessage;
        }

        void mediaElement_MediaEnded(object sender, EventArgs e)
        {
            // TODO
        }

        void mediaElement_DownloadProgressChanged(object sender, EventArgs e)
        {
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
            }
            else
            {
                animatedSound.Visibility = Visibility.Collapsed;
                soundAnimation.Stop();
                positionUpdate.Stop();
            }
        }

        private void ShowProgress()
        {
            audioPositionSlider.Position = mediaElement.Position;
        }

        void mediaElement_BufferingProgressChanged(object sender, EventArgs e)
        {
        }
    }
}
