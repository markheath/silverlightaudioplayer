//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Silverlight.Samples.Controls
{

    // Implements a Thumb control with grip. The grip shows up when the size of the control is
    // bigger than the minimum size  and dissaperas if it is smaller.
    public class GripThumb : Thumb
    {

        #region Public Methods

        // Default GripThumb ctor
        public GripThumb()
        {
            //find the grip shape if any - finds the grip and initializes brushes
            grip = FindName("grip") as Shape;
            gripaccent = FindName("gripaccent") as Shape;
            //find the outline
            jewel = FindName("jewel") as FrameworkElement;
            //find the highlight
            highlight = FindName("highlight") as Shape;

            MouseMove += new MouseEventHandler(OnMouseMove);
        }

        #endregion Public Methods

        #region Public Properties

        // Sets the current size of the gripthumb - the size corresponds to
        // the thumb width. If the gripthumb is vertical it is rotated.
        public double Size
        {
            set { Width = value; }
            get { return Width; }
        }

        //the minimal thumb size needed to show the grip
        public double MinimumSize
        {
            set { minSize = value; }
            get { return minSize; }
        }

        #endregion Public Properties

        #region Protected Methods

        //in addition to thumb layout takes care of the grip
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            grip.Width = Width;
            gripaccent.Width = Width - 6;
            //set grip position
            jewel.SetValue(Canvas.LeftProperty, 0.5 * (Width - jewel.Width));
        }

        //The jewel does not need to be rotated - apply the opposite transformation
        public override void UpdateRotation(RotateTransform transform)
        {
            if (jewel != null) {
                RotateTransform newTransform = new RotateTransform();
                newTransform.Angle = -transform.Angle;
                newTransform.CenterX = jewel.Width / 2;
                newTransform.CenterY = jewel.Height / 2;
                jewel.RenderTransform = newTransform;
            }
        }

        #endregion Protected Methods

        #region Protected Properties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "GripThumb.xaml"; }
        }

        #endregion Protected Properties

        #region Private Methods

        private void PulseStoryboardCompleted(object sender, EventArgs e)
        {
            ((Storyboard)sender).Begin();
        }

        #endregion Private Methods

        #region Data

        Shape grip = null; //the grip object
        Shape gripaccent = null;
        FrameworkElement jewel = null; //the outline object
        Shape highlight = null; //the highlight object
        double minSize = 10; //default minimum  size

        #endregion Data
    }
}
