//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Input;

namespace Silverlight.Samples.Controls {

    // Implements a RepeatButton control. This is a button that sends Click event
    // repeatedly while the MouseButton is pressed. Uses Annimation StoryBoard for 
    // triggering repeated events
    public class RepeatButton : Button {

        #region Public Methods

        // Default RepeatButton ctor - initialize brushes and find the storyboard
        public RepeatButton()
        {
            /*((SolidColorBrush)NormalBrush).Color = Color.FromArgb(0xFF, 0xB0, 0xE0, 0xE6);
            ((SolidColorBrush)HighlightBrush).Color = Color.FromArgb(0xFF, 0xAD, 0xD8, 0xE6);*/

            //Annimation StoryBoard is used for triggering repeating events
            storyboard = FindName("storyboard") as Storyboard;
        }

        #endregion Public Methods

        #region Protected Methods

        // Sets the ButtonPressed flag to false and starts the story board for repeat
        protected override void OnMouseLeftButtonDown(object sender, MouseEventArgs args)
        {
            base.OnMouseLeftButtonDown(sender, args);
            //start timer
            if (storyboard != null) {
                startCount = 0;
                storyboard.Begin();
            }
        }

        // Sets the ButtonPressed flag to false and stops the storyboard
        protected override void OnMouseLeftButtonUp(object sender, MouseEventArgs args)
        {
            //release timer
            if (storyboard != null) {
                storyboard.Stop();
            }

            base.OnMouseLeftButtonUp(sender, args);
        }

        #endregion Protected Methods

        #region Protected Proiperties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "RepeatButton.xaml"; }
        }

        #endregion Protected Proiperties

        #region Private Methods

        // Storyboard callback - used to implement repeating events
        private void OnRepeat(object sender, EventArgs args)
        {
            //we have some delay in the beginning
            if (startCount++ >= maxStartCount) {
                RaiseClick();
            }
            storyboard.Begin();
        }

        #endregion Private Methods

        #region Data

        private Storyboard storyboard;  //used to generate click messages when holding LBD
        private int startCount = 0;     // wait some time before the first event
        private const int maxStartCount = 5; //maximum number of start loops for Click event

        #endregion Data
    }
}
