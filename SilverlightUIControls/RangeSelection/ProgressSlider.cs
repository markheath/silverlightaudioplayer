//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Diagnostics;

namespace Silverlight.Samples.Controls
{

    // Implements a Slider control. The slider consists of a track
    // and a moving Thumb. It has a Range and Value properties. The
    // Value property reflects and controls the position of the Thumb.
    public class ProgressSlider : Slider
    {
        Rectangle positionBarRectangle;
        Rectangle downloadProgressRectangle;
        #region Public Methods

        // Slider ctor - finds required elements 
        public ProgressSlider()
        {
            positionBarRectangle = FindName("positionBarRectangle") as Rectangle;
            downloadProgressRectangle = FindName("downloadProgressRectangle") as Rectangle;

        }

        #endregion Public Methods

        #region Public Properties


        // Current value of the control corresponding to the Thumb position
        public double DownloadPercent
        {
            get { return downloadPercent; }
            set
            {
                downloadPercent = value;
                UpdateDownloadPosition();
            }
        }

        private void UpdateDownloadPosition()
        {
            if (downloadProgressRectangle != null)
            {
                downloadProgressRectangle.Width = positionBarRectangle.Width * DownloadPercent;
            }
        }

        #endregion Public Properties



        //We need to update MovingSize if the Width chnges
        protected override void UpdateLayout()
        {
            if (positionBarRectangle != null)
            {
                positionBarRectangle.Width = Width - 10;
                positionBarRectangle.Height = Height - 10;
            }
            if (downloadProgressRectangle != null)
            {
                downloadProgressRectangle.Width = Width - 10;
                downloadProgressRectangle.Height = Height - 10;
            }
            Thumb.Height = Height;
            base.UpdateLayout();
        }


        #region Protected Properties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "ProgressSlider.xaml"; }
        }

        #endregion Protected Properties

        protected void OnDragStart(object sender, object args)
        {
            base.OnDragStart(sender, args);
        }

        // Changes the current value based of the move of the Thumb
        protected void OnDragDelta(object sender, double delta)
        {
            base.OnDragDelta(sender, delta);
        }


        #region Data

        private double downloadPercent = 0; //the download value

        #endregion Data

    }

}
