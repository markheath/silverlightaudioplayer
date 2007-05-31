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

    // Control orientation - Slider, ScrollBar
    public enum Orientation {
        Horizontal,
        Vertical
    };

    // Implements a Slider control. The slider consists of a track
    // and a moving Thumb. It has a Range and Value properties. The
    // Value property reflects and controls the position of the Thumb.
    public class Slider : ControlBase {

        #region Public Methods

        // Slider ctor - finds required elements 
        public Slider()
        {
            Debug.Assert(Width != 0, "Slider width must be specified");
            thumb = FindName("thumb") as Thumb;
            Debug.Assert(thumb != null, "Thumb not found");
            sliderTrack = FindName("sliderTrack") as FrameworkElement;
            if (sliderTrack != null) {
                size = sliderTrack.Width;
                //set the thumb at the beginning of the sliderTrack
                Thumb.SetValue(Canvas.LeftProperty, (double)(sliderTrack.GetValue(Canvas.LeftProperty)));
            } else {
                size = Width;
            }

            Debug.Assert (size > thumb.Width, "The slider is smaller than the Thumb");
            Thumb.SetValue(Canvas.TopProperty, (Height - Thumb.Height) / 2);

            currentValue = lastValue = range.Min;
            thumb.Move(0);
        }

        #endregion Public Methods

        #region Public Properties

        public Orientation Orientation
        {
            set
            {
                orientation = value;
                //set appropriate render transform
                if (value == Orientation.Horizontal) {
                    RotateTransform transform = new RotateTransform();
                    transform.CenterX = transform.CenterY = transform.Angle = 0;
                    RenderTransform = transform;
                } else {
                    RotateTransform transform = new RotateTransform();
                    transform.CenterX = transform.CenterY = 0;
                    transform.Angle = 90;
                    RenderTransform = transform;
                    Thumb.UpdateRotation(transform);
                }
            }

            get { return orientation; }
        }

        // Value range of the control
        public ValueRange Range
        {
            get { return range; }
            set { UpdateRangeAndValue(value); }
        }

        // Current value of the control corresponding to the Thumb position
        public double Value
        {
            get { return currentValue; }
            set 
            {
                currentValue = value;
                UpdateThumb();
            }
        }

        #endregion Public Properties

        #region Public Events

        // Fired when the Slider value changes
        public event EventHandler ValueChanged;

        #endregion Public Events

        #region Protected Methods

        // Saves current value at start 
        protected void OnDragStart(object sender, object args)
        {
            lastValue = currentValue;
        }

        // Changes the current value based of the move of the Thumb
        protected void OnDragDelta(object sender, double delta)
        {
            currentValue += delta * range.Range / Width;
            UpdateThumb();
        }

        // If the Maximum/Minimum value changes we need to figure out what is 
        // the value of the current position
        protected virtual void UpdateRangeAndValue(ValueRange newRange)
        {
            double currDelta = range.Range == 0 ? 0 : Width / range.Range * (currentValue - range.Min);
            currentValue = lastValue = currDelta / Width * (newRange.Max - newRange.Min) + newRange.Min;
            range = newRange;
        }

        //We need to update MovingSize if the Width chnges
        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            if (sliderTrack != null) {
                double shrink = (double)(sliderTrack.GetValue(Canvas.LeftProperty));
                sliderTrack.Width = size = Width - 2 * shrink;
            } else {
                size = Width;
            }
        }

        #endregion Protected Methods

        #region Protected Properties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "Slider.xaml"; }
        }

        // Some controls need access to the Thumb
        protected Thumb Thumb
        {
            get { return thumb; }
        }

        #endregion Protected Properties

        #region Private Methods

        // If the range or the value is changed - recalculate the new Thumb position
        private void UpdateThumb()
        {
            //if the value range is 0 we can not move
            if (range.Range == 0)
                return;

            double thumbValue = thumb.Width / size * range.Range;
            if (currentValue < range.Min)
                currentValue = range.Min;
            if (currentValue > range.Max - thumbValue)
                currentValue = range.Max - thumbValue;

            double newDelta = size / range.Range * (currentValue - lastValue);
            if (newDelta != 0.0) {
                thumb.Move(newDelta);
                lastValue = currentValue;

                if (ValueChanged != null) {
                    ValueChanged(this, null);
                }
            }
        }

        #endregion Private Methods

        #region Data

        private Thumb thumb = null; //the Thumb child
        private Orientation orientation = Orientation.Horizontal;
        private double lastValue = 0; //current position of the Thumb
        private double currentValue = 0; //the current value
        private ValueRange range = new ValueRange(0, 0); //the slider value range
        private double size = 0;  //the pixel size of the slider (Width - thumb.Width)
        private FrameworkElement sliderTrack = null; //the track that control slider movement

        #endregion Data

    }

}