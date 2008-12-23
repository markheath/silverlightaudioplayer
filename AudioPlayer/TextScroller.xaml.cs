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
using System.Diagnostics;

namespace AudioPlayer
{
    public partial class TextScroller : UserControl
    {
        public TextScroller()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(TextScroller_SizeChanged);
        }

        void TextScroller_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RectangleGeometry rect = new RectangleGeometry();
            rect.Rect = new Rect(0, 0, e.NewSize.Width, e.NewSize.Height);
            LayoutRoot.Clip = rect;
            
        }

        string text;

        public string Text
        {
            get 
            { 
                return text; 
            }
            set 
            { 
                text = value;
                message.Text = value;
                //Debug.Assert(message.ActualWidth > 0, "Is it working now?");
                double messageWidth = message.ActualWidth; // didn't use to work, so estimated with message.Text.Length * 10
                daMoveMessage.From = this.Width;
                daMoveMessage.To = 0 - messageWidth; // +this.Width;
                daMoveMessage.Duration = CalculateAnimationDuration(this.Width - messageWidth);
                if (daMoveMessage.To < 0)
                {
                    scrollStoryboard.Stop();
                    scrollStoryboard.Begin();
                }

                // will fire a message size changed?
            }
        }

        private Duration CalculateAnimationDuration(double distanceTravelled)
        {
            return new Duration(TimeSpan.FromMilliseconds(distanceTravelled * 50));
        }

        
    }
}
