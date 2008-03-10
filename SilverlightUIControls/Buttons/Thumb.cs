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

namespace Silverlight.Samples.Controls {

    public delegate void DragDeltaEventHandler(object sender, double delta);

    // Implements a Thumb control. The thumb is a shape that gets highlighted when the mouse is 
    // over and translated slightly when the left mouse button is down. It moves with the mouse in 
    // horizontal or vertical direction and sends DragStart, DragEnd, DragDelta events. It also 
    // a Value propertie (set and get) which represents the offset of 
    // the Thumb from its initial position. 
    public class Thumb : ButtonBase {

        #region Public Methods

        // Default Thumb ctor - does nothing
        public Thumb()
        {
            jewel = FindName("jewel") as FrameworkElement;
        }

        // Moves the Thumb by delta units. Saves current position
        public void Move(double delta)
        {
            double left = (double)GetValue(Canvas.LeftProperty);
            SetValue(Canvas.LeftProperty, left + delta);
            startValue += delta;
        }

        #endregion Public Methods

        #region Public Events

        // Fired when the LMB is down on this control
        public event EventHandler DragStart;

        // Fired when the LMB is up on this control
        public event EventHandler DragEnd;

        // Fired when mose moves on this control with pressed LMB
        public event DragDeltaEventHandler DragDelta;

        #endregion Public Events

        #region Protected Methods

        // Stores the parent for future use
        protected override void OnLoaded(object sender, RoutedEventArgs args)
        {
            base.OnLoaded(sender, args);
            parent = Parent as UIElement;
        }

        // When the mouse leaves the root visual we need to reset the drag state 
        protected override void OnRootLeave(object sender, EventArgs args)
        {
            base.OnRootLeave(sender, args);
            drag = false;
        }

        // initializes the start position and fires DragStart event
        protected override void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonDown(sender, args);
            if (MouseOver) {
                startValue = args.GetPosition(parent).X;
                drag = true;
                if (DragStart != null) {
                    DragStart(this, null);
                }
            }
        }

        // resets the drag state and fires DragEnd event
        protected override void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs args)
        {
            base.OnMouseLeftButtonUp(sender, args);
            drag = false;
            if (DragEnd != null) {
                DragEnd(this, null);
            }
        }

        // Moves the Thumb and notifies the listeners
        protected void OnMouseMove(object sender, MouseEventArgs args)
        {
            if (drag) {
                Double delta = args.GetPosition(parent).X - startValue;

                if (DragDelta != null) {
                    DragDelta(this, delta);
                }
            }
        }

        // Skip visuals update while dragging
        protected override void OnMouseLeave(object sender, MouseEventArgs args)
        {
            if (drag) {
                //we do not want to update visuals while dragging to avoid blinking
                MouseOver = false;
            } else {
                base.OnMouseLeave(sender, args);
            }
        }

        //The jewel does not need to be rotated - apply the opposite transformation
        public virtual void UpdateRotation(RotateTransform transform)
        {
            if (jewel != null) {
                RotateTransform newTransform = new RotateTransform();
                newTransform.Angle = -transform.Angle;
                newTransform.CenterX = Width / 2;
                newTransform.CenterY = Height / 2;
                jewel.RenderTransform = newTransform;
            }
        }

        #endregion Protected Methods

        #region Protected Properties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "Thumb.xaml"; }
        }

        #endregion Protected Properties

        #region Private Methods

        private void PulseStoryboardCompleted(object sender, EventArgs e)
        {
            ((Storyboard)sender).Begin();
        }

        #endregion Private Methods

        #region Data

        private double startValue = 0;   //the initial value when drag starts
        private bool drag = false;       //true if in dragging mode
        private UIElement parent = null; //all moves are relative to parent transformation
        private FrameworkElement jewel = null;

        #endregion Data
    }
}
