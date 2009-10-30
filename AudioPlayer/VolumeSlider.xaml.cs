using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace AudioPlayer
{
    public partial class VolumeSlider : UserControl
    {
        private bool dragging;

        public VolumeSlider()
        {
            InitializeComponent();
            this.MouseLeftButtonDown += new MouseButtonEventHandler(LayoutRoot_MouseLeftButtonDown);
            this.MouseMove += new MouseEventHandler(LayoutRoot_MouseMove);
            this.MouseLeftButtonUp += new MouseButtonEventHandler(LayoutRoot_MouseLeftButtonUp);
            this.SizeChanged += new SizeChangedEventHandler(VolumeSlider_SizeChanged);
            Volume = 1.0;
        }

        void VolumeSlider_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetMargin();
        }

        public static readonly DependencyProperty VolumeProperty = DependencyProperty.Register(
            "Volume", typeof(double), typeof(VolumeSlider), new PropertyMetadata(OnVolumeChanged));

        public event EventHandler<VolumeChangedEventArgs> VolumeChanged;

        public double Volume
        {
            get
            {
                return (double)this.GetValue(VolumeProperty);
            }
            set
            {
                this.SetValue(VolumeProperty, value);
            }
        }

        private static void OnVolumeChanged(object sender, DependencyPropertyChangedEventArgs args)
        {
            double newVolume = (double)args.NewValue;

            if (newVolume < 0.0)
                newVolume = 0.0;
            if (newVolume > 1.0)
                newVolume = 1.0;

            VolumeSlider slider = (VolumeSlider)sender;
            slider.SetMargin();

            slider.FireVolumeChanged(new VolumeChangedEventArgs(newVolume));
        }

        private void SetMargin()
        {
            double rightMargin = (1.0 - Volume) * ActualWidth;
            indicatorRectangle.Margin = new Thickness(0, 0, rightMargin, 0); 
        }

        void LayoutRoot_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            dragging = false;
        }

        void  LayoutRoot_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point position = e.GetPosition(this);
                SetVolumeFromXPos(position.X);
            }
        }

        void LayoutRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {            
            Point position = e.GetPosition(this);
            if (CaptureMouse())
            {
                dragging = true;
                SetVolumeFromXPos(position.X);
            }
        }

        private void SetVolumeFromXPos(double x)
        {
            double percent = x / this.ActualWidth;
            if (percent < 0.0)
                percent = 0.0;
            if (percent > 1.0)
                percent = 1.0;
            Volume = percent;
        }

        protected virtual void FireVolumeChanged(VolumeChangedEventArgs e)
        {
            if (VolumeChanged != null) { VolumeChanged(this, e); }
        }

    }

    public class VolumeChangedEventArgs : EventArgs
    {
        public double Volume { get; private set; }
        public VolumeChangedEventArgs(double volume)
        {
            this.Volume = volume;
        }
    }
}
