using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace SilverlightAudioPlayer
{
    // TODO: support changing width and height
    // TODO: mouse over position changes cursor and colour
    // TODO: support dragging slider position
    public class AudioPositionSlider : Control
    {
        Rectangle downloadProgressRectangle;
        Rectangle positionBarRectangle;
        Rectangle positionIndicatorRectangle;
        Canvas rootElement;
        TimeSpan duration;
        TimeSpan position;
        double downloadPercent;

        public AudioPositionSlider()
        {
            System.IO.Stream s = this.GetType().Assembly.GetManifestResourceStream("SilverlightAudioPlayer.AudioPositionSlider.xaml");
            rootElement = (Canvas)this.InitializeFromXaml(new System.IO.StreamReader(s).ReadToEnd());
            
            downloadProgressRectangle = (Rectangle)rootElement.FindName("downloadProgressRectangle");
            Debug.Assert(downloadProgressRectangle != null);
            positionBarRectangle = (Rectangle)rootElement.FindName("positionBarRectangle");
            Debug.Assert(positionBarRectangle != null);
            positionIndicatorRectangle = (Rectangle)rootElement.FindName("positionIndicatorRectangle");
            Debug.Assert(positionIndicatorRectangle != null);
            positionIndicatorRectangle.MouseEnter += new MouseEventHandler(positionIndicatorRectangle_MouseEnter);
            positionIndicatorRectangle.MouseLeave += new EventHandler(positionIndicatorRectangle_MouseLeave);

            DownloadPercent = 0;
        }


        void positionIndicatorRectangle_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
            //positionIndicatorRectangle.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
        }

        void positionIndicatorRectangle_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        public TimeSpan Duration
        {
            get { return duration; }
            set { duration = value; UpdateSliderPosition(); }
        }

        public TimeSpan Position
        {
            get { return position; }
            set { position = value; UpdateSliderPosition(); }
        }

        public double PositionPercent
        {
            get 
            {
                if(Duration.TotalMilliseconds > 0)
                    return Position.TotalMilliseconds / Duration.TotalMilliseconds;
                return 0;
            }
        }

        public double DownloadPercent
        {
            get { return downloadPercent; }
            set { downloadPercent = value; UpdateDownloadBar(); }
        }

        void UpdateSliderPosition()
        {
            double overhang = (double)downloadProgressRectangle.GetValue(Canvas.LeftProperty);
            double sliderRange = 2 * overhang + downloadProgressRectangle.Width -
                positionIndicatorRectangle.Width;

            positionIndicatorRectangle.SetValue<double>(Canvas.LeftProperty,
                PositionPercent * sliderRange);
        }

        void UpdateDownloadBar()
        {
            downloadProgressRectangle.Width = 
                positionBarRectangle.Width * DownloadPercent;
        }
    }
}
