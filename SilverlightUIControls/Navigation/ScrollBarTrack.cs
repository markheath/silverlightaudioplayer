//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Input;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace Silverlight.Samples.Controls {

    // Base class for all Sample Controls - overrides Width, Height and
    // mouse events to use the implementation root
    public class ScrollBarTrack : ControlBase {

        #region Public Methods

        public ScrollBarTrack()
        {
            leftButton = FindName("leftButton") as FrameworkElement;
            rightButton = FindName("rightButton") as FrameworkElement;
            trackBase = FindName("trackBase") as FrameworkElement;
            if (leftButton != null) {
                MatrixTransform tr = new MatrixTransform();
                tr.Matrix = new Matrix(-1, 0, 0, 1, leftButton.Width, 0);
                leftButton.RenderTransform = tr;
            }
        }

        #endregion Public Methods

        #region Protected Methods

        //Reposition the buttons
        protected override void UpdateLayout() 
        {
            if (trackBase != null) {
                trackBase.Width = Width;
            }
            if ((rightButton != null) && (Width > rightButton.Width)) {
                rightButton.SetValue(Canvas.LeftProperty, Width - rightButton.Width);
                rightButton.Visibility = Visibility.Visible;
            } else {
                rightButton.Visibility = Visibility.Hidden;
            }
        }

        #endregion Protected Methods

        #region Protected Proiperties

        // The resource name used to initialize the actual object
        protected override string ResourceName { get { return "ScrollBarTrack.xaml"; } }

        #endregion Protected Proiperties

        #region Data

        private FrameworkElement leftButton = null;
        private FrameworkElement rightButton = null;
        private FrameworkElement trackBase = null; 

        #endregion Data
    }
}
