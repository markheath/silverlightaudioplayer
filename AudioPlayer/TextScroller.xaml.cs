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

                daMoveMessage.To = 0 - message.Text.Length * 10 + this.Width;
                daMoveMessage.Duration = new Duration(TimeSpan.FromMilliseconds(message.Text.Length * 200));
                if (daMoveMessage.To < 0)
                {
                    scrollStoryboard.Stop();
                    scrollStoryboard.Begin();
                }

                // will fire a message size changed?
            }
        }

        
    }
}
