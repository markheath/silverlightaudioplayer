//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;

namespace Silverlight.Samples.Controls {

    // The CrollViewer is a control with one child content element of type
    // Canvas and two ScrollBars which may or may not be visible. The key names used
    // are: "content" for content canvas, "horizontal" for horizontal scrollbar, "vertical"
    // for vertical scrollbar, "corner" for corner shape. Only the "content" element is required.
    // The ScrollViewer has also HorizontalRange and VerticalRange
    // properties of type ValueRange which represent the logical value range
    // for the horizontal and vertical ScrollBars respectively.
    public class ScrollViewer : ControlBase {

        #region Public Methods

        public ScrollViewer()
        {
            //find the bars
            horizontalScrollBar = FindName("horizontal") as ScrollBar;
            if (horizontalScrollBar != null) {
                horizontalScrollBar.Orientation = Orientation.Horizontal;
                horizontalScrollBar.ScrollStep = scrollableWidth;
            }
            verticalScrollBar = FindName("vertical") as ScrollBar;
            if (verticalScrollBar != null) {
                verticalScrollBar.Orientation = Orientation.Vertical;
                verticalScrollBar.ScrollStep = scrollableHeight;
            }

            corner = FindName("corner") as Shape;
            Content = FindName("content") as Canvas;
        }

        #endregion Public Methods

        #region Public Properties

        // Gives access to the content object of the ScrollViewer
        public Canvas Content
        {
            get { return content; }
            set
            {
                if (content != null) {
                    throw new ControlException("The content object has already been set");
                }

                if (value != null) {
                    content = value;
                    Canvas actualControl = ActualControl as Canvas;

                    //must be bellow the ScrollBars and other stuff
                    ((Canvas)ActualControl).Children.Insert(0, value);

                    //set the ScrollBars range to the content sizes
                    if (horizontalScrollBar != null) {
                        horizontalScrollBar.Range = new ValueRange(0, content.Width);
                    }
                    if (verticalScrollBar != null) {
                        verticalScrollBar.Range = new ValueRange(0, content.Height);
                    }
                }
            }
        }

        // Horizontal ScrollBar 
        public ScrollBar HorizontalScrollBar
        {
            get { return horizontalScrollBar; }
        }

        // Vertical ScrollBar 
        public ScrollBar VerticalScrollBar
        {
            get { return verticalScrollBar; }
        }

        // Sets the vertical scroll step for the viewer
        public double ScrollableHeight
        {
            set
            {
                if (value <= 0) {
                    throw new ControlException("ScrollableHeight must be positive");
                }
                scrollableHeight = value;
                if (verticalScrollBar != null) {
                    verticalScrollBar.ScrollStep = value;
                }
            }
            get { return scrollableHeight; }
        }

        // Sets the horizontal scroll step for the viewer
        public double ScrollableWidth
        {
            set
            {
                if (value <= 0) {
                    throw new ControlException("ScrollableWidth must be positive");
                }
                scrollableWidth = value;
                if (horizontalScrollBar != null) {
                    horizontalScrollBar.ScrollStep = value;
                }
            }
            get { return scrollableWidth; }
        }

        #endregion Public Properties

        #region Protected Methods

        //set the scrollbars size and position
        protected override void UpdateLayout()
        {
            base.UpdateLayout();

            //set the horizontal scrollbar at the bottom
            if (horizontalScrollBar != null) {
                horizontalScrollBar.Size = verticalScrollBar != null ?
                                             Width - verticalScrollBar.Height : Width;
                horizontalScrollBar.ViewportSize = Width;
                horizontalScrollBar.SetValue(Canvas.TopProperty, Height - horizontalScrollBar.Height);
            }

            //set the vertical scrollbar on the right side
            if (verticalScrollBar != null) {
                verticalScrollBar.Size = horizontalScrollBar != null ?
                                           Height - horizontalScrollBar.Height : Height;
                verticalScrollBar.ViewportSize = Height;
                verticalScrollBar.SetValue(Canvas.LeftProperty, Width);
            }

            //move the corner too
            if (corner != null) {
                corner.SetValue(Canvas.LeftProperty, Width - corner.Width);
                corner.SetValue(Canvas.TopProperty, Height - corner.Height);
            }

            //update clipping area
            RectangleGeometry clip = new RectangleGeometry();
            clip.Rect = new Rect(0, 0, Width, Height);
            Clip = clip;
        }

        #endregion Protected Methods

        #region Protected Prperties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "ScrollViewer.xaml"; }
        }

        #endregion Protected Prperties

        #region Private Methods

        // Move the content  to reflect the new scroll value
        private void OnHorizontalScroll(object sender, EventArgs args)
        {
            if (content != null) {
                int temp = (int)(horizontalScrollBar.Value / scrollableWidth);
                content.SetValue(Canvas.LeftProperty, -temp * scrollableWidth);
            }
        }

        // Move the content to reflect the new scroll value
        private void OnVerticalScroll(object sender, EventArgs args)
        {
            if (content != null) {
                int temp = (int)(verticalScrollBar.Value / scrollableHeight);
                content.SetValue(Canvas.TopProperty, -temp * scrollableHeight);
            }
        }

        #endregion Private Methods

        #region Data

        private Canvas content = null; //the canvas that holds the content of the SV
        private ScrollBar horizontalScrollBar = null; //horizontal ScrollBar if any
        private ScrollBar verticalScrollBar = null; //vertical ScrollBar if any
        private Shape corner = null; //the corner rectangle
        private double scrollableHeight = 1; //default scrollable height
        private double scrollableWidth = 1;  // default scrollable width

        #endregion Data
    }

}