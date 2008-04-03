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
    public partial class AnimatedSpeaker : UserControl
    {
        public AnimatedSpeaker()
        {
            InitializeComponent();
        }

        public void StartAnimation()
        {
            animatedSound.Visibility = Visibility.Visible;
            soundAnimation.Begin();
        }

        public void StopAnimation()
        {
            animatedSound.Visibility = Visibility.Collapsed;
            soundAnimation.Stop();
        }
    }
}
