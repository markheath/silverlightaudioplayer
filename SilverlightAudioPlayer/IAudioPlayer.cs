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
    public interface IAudioPlayer
    {
        string Url { get; set; }
        void Play();
        void Pause();
    }
}
