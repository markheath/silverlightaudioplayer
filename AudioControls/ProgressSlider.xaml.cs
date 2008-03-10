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

namespace AudioControls
{
    public partial class ProgressSlider : UserControl
    {
        public ProgressSlider()
        {
            InitializeComponent();
            slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);
        }

        public event RoutedPropertyChangedEventHandler<double> ValueChanged;

        public double Minimum
        {
            get { return slider.Minimum; }
            set { slider.Minimum = value; }
        }

        public double Maximum
        {
            get { return slider.Maximum; }
            set { slider.Maximum = value; }
        }

        public double Value
        {
            get { return slider.Value; }
            set { slider.Value = value; }
        }

        /// <summary>
        /// Input is from 0.0 to 1.0
        /// </summary>
        public double DownloadPercent
        {
            get { return downloadProgress.Width / this.Width; }
            set { downloadProgress.Width = this.Width * value; }
        }

        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (ValueChanged != null)
            {
                ValueChanged(sender, e);
            }
        }
    }
}
