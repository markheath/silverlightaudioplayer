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

namespace Silverlight.Samples.Controls {

    // Implements a ScrollBar control. It consists of a track, a thumb with grip
    // and two repeat buttons at the ends of the track. The implementation is based
    // on the slider implementation.
    public class ScrollBar : Slider {

        #region Public Methods

        // ScrollBar ctor - initilizes references to elements that will be manipulated 
        public ScrollBar()
        {
            track = FindName("track") as ScrollBarTrack;
            Debug.Assert(track != null, "Scrollbar track is missing");
            up = FindName("left") as RepeatButton;
            down = FindName("right") as RepeatButton;
            Debug.Assert ((up != null) && (down == null), "at least one repeat button is missing");
            if (down != null) {
                MatrixTransform tr = new MatrixTransform();
                tr.Matrix = new Matrix(-1, 0, 0, 1, down.Width, 0);
                down.RenderTransform = tr;
            }

            //set some initial Viewport
            viewSize = Width;

            //register for events
            ValueChanged += new EventHandler(OnValueChanged);
            Thumb.DragStart += new EventHandler(OnDragStart);
            Thumb.DragDelta += new DragDeltaEventHandler(OnDragDelta);
        }

        #endregion Public Methods

        #region Public Properties

        //The size of the SCrollBar is equal to its width. The vartical SCrollBar is just rotated.
        public double Size
        {
            set { Width = track.Width = value; }
            get { return Width; }
        }

        // Sets the size of the viewport for the ScrollBar
        public double ViewportSize
        {
            get { return viewSize; }
            set
            {
                viewSize = value;
                UpdateThumbSize();
            }
        }

        // identifies the one click step for the ScrollBar
        public double ScrollStep
        {
            set
            {
                if (value <= 0) {
                    throw new ControlException("scrolling step must be positive");
                }
                scrollStep = value;
            }
            get { return scrollStep; }
        }

        #endregion Public Properties

        #region Public Events

        // Fired when Range or Value changes
        public event EventHandler ScrollChanged;

        #endregion Public Events

        #region Protected Methods

        // In addition to slider update updates the Thumb size
        // and fires ScrollChanged event
        protected override void UpdateRangeAndValue(ValueRange range)
        {
            base.UpdateRangeAndValue(range);
            UpdateThumbSize();
        }

        //in addition to slider layout sets the thumb size and repeat buttons
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            //set Thumb size and position
            UpdateThumbSize();
            SetRepeatButtonsPosition();
        }

        #endregion Protected Methods

        #region Protected Proiperties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "ScrollBar.xaml"; }
        }

        #endregion Protected Proiperties

        #region Private Methods

        // Highlight the repeat buttons and the Thumb
        private void OnMouseEnter(object sender, MouseEventArgs args)
        {
            up.IsHighlighted = down.IsHighlighted = HighlightType.Outline;
            Thumb.IsHighlighted = HighlightType.Highlight;
        }

        // Highlight the repeat buttons and the Thumb
        private void OnMouseLeave(object sender, EventArgs args)
        {
            up.IsHighlighted = down.IsHighlighted = Thumb.IsHighlighted = HighlightType.None;
        }

        // If the range or viewportsize is changed - update thumb size
        private void UpdateThumbSize()
        {
            if (viewSize == 0) {
                throw new ControlException("ScrollBar viewport size can not be null");
            }
            double slidingSize = Width - up.Width - down.Width;
            if (slidingSize > 0) {
                //hide the thumb if too big
                if (Range.Range <= viewSize) {
                    Thumb.Visibility = Visibility.Hidden;
                    up.Visibility = Visibility.Hidden;
                    down.Visibility = Visibility.Hidden;
                } else {
                    Thumb.Visibility = Visibility.Visible;
                    up.Visibility = Visibility.Visible;
                    down.Visibility = Visibility.Visible;
                    //calculate the correct size
                    if (Thumb is GripThumb) {
                        if (slidingSize < ((GripThumb)Thumb).MinimumSize) {
                            Thumb.Visibility = Visibility.Hidden;
                        } else {
                            ((GripThumb)Thumb).Size = viewSize / Range.Range * slidingSize;
                        }
                    }
                }
            }
        }

        // position the repeat buttons on the ends of the slider track
        private void SetRepeatButtonsPosition()
        {
            up.SetValue(Canvas.LeftProperty, Width - up.Width);
        }

        // If the scroll value changes fire ScrollChanged event
        private void OnValueChanged(object sender, EventArgs args)
        {
            if (ScrollChanged != null) {
                ScrollChanged(this, null);
            }
        }

        // increments the value by one scroll step, updates the scrollbar
        // and notifies the user
        private void OnClickUp(object sender, EventArgs args)
        {
            Value += scrollStep;
        }

        // decrements the value by one scroll step, updates the slider
        // and notifies the user
        private void OnClickDown(object sender, EventArgs args)
        {
            Value -= scrollStep;
        }

        // Fires PageUp or PageDown event
        private void OnPage(object sender, MouseEventArgs args)
        {
            //this makes sence only if the thumb is visible
            if (Thumb.Visibility == Visibility.Visible) {
                double thPos = (double)Thumb.GetValue(Canvas.LeftProperty);
                double mPos = args.GetPosition(this).X;
                if ((down.Width < mPos) && (mPos < thPos)) {
                    Value -= viewSize;
                } else if ((mPos > thPos + Thumb.Width) && (mPos < Width - up.Width)) {
                    Value += viewSize;
                }
            }
        }

        #endregion Private Methods

        #region Data

        private RepeatButton up = null; //up repeat button
        private RepeatButton down = null; //down repeat button
        private double viewSize = 0; //the viewport size
        private double scrollStep = 5; //default scroll step
        private ScrollBarTrack track = null;  // scrollbar track

        #endregion Data
    }

}