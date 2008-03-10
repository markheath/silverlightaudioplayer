//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Input;

namespace Silverlight.Samples.Controls {

    // Implements a ListBox control. The ListBox is a Control
    // that has a collection of Items - FrameworkElements. All Items have
    // equal height and are positioned vertically. A vertical ScrollBar 
    // is added if needed
    public class ListBox : ControlBase {

        #region Public Methods

        // Default ListBox ctor - sets the Content Canvas. We need that for scrolling.
        public ListBox()
        {
            content = new Canvas();
            //must be bellow every thing else
            ((Canvas)ActualControl).Children.Insert(0, content);
            scrollBar = FindName("scrollbar") as ScrollBar;
            background = FindName("background") as Rectangle;
            RootLeave += new EventHandler(OnRootLeave);
        }

        //Updates the content children with the current items in the list
        public void UpdateItems()
        {
            //remove all the children and start from empty
            content.Children.Clear();

            //place the items and add a scrollbar if needed
            if (items.Count < 1) {
                return;
            }

            itemHeight = items[0].Height;
            if (itemHeight <= 0) {
                throw new ControlException("items height must be specified");
            }

            //first check if all have the same Height
            foreach (FrameworkElement item in items) {
                if (item.Height != itemHeight) {
                    throw new ControlException("different items height is not supported");
                }
            }

            //attach the items
            double pos = 0;
            foreach (FrameworkElement item in items) {
                item.SetValue(Canvas.LeftProperty, 0);
                item.SetValue(Canvas.TopProperty, pos);
                content.Children.Add(item);
                pos += itemHeight;
            }

            if (scrollBar != null) {
                //check if we need to show the scrollbar
                if (pos > Height) {
                    scrollBar.Visibility = Visibility.Visible;
                    scrollBar.Range = new ValueRange(0, pos);
                    scrollBar.ScrollStep = itemHeight;
                    scrollBar.Orientation = Orientation.Vertical;
                    //change the rectangle width if needed
                    if (background != null) {
                        background.Width = Width - scrollBar.Height;
                    }
                } else {
                    scrollBar.Visibility = Visibility.Collapsed;
                    //change the rectangle width if needed
                    if (background != null) {
                        background.Width = Width;
                    }
                }
            }

            //add the selection rectangle at the end
            selectionHighlight = new Rectangle();
            selectionHighlight.Opacity = 0.75;
            selectionHighlight.Fill = new SolidColorBrush(Color.FromArgb(0xff, 0xbe, 0xe7, 0xfb));
            selectionHighlight.Visibility = Visibility.Collapsed;
            content.Children.Add(selectionHighlight);
        }

        #endregion Public Methods

        #region Public Properties

        // The collection of items for the ListBox
        public ICollection<FrameworkElement> Items
        {
            get { return items; }
        }

        // Currently selected item - null if no selection
        public FrameworkElement SelectedItem
        {
            get { return selectedItem; }
        }

        #endregion Public Properties

        #region Public Events

        // Fired when the selected item changes
        public event EventHandler SelectionChanged;

        #endregion Public Events

        #region Protected Methods

        // CaptureMouse
        protected void OnMouseLeftButtonDown(object sender, MouseEventArgs args)
        {
            if (CheckMousePosition(args)) {
                ActualControl.CaptureMouse();
            }
        }

        // If the mouse is on the ListBox find on which item and select it.
        protected void OnMouseLeftButtonUp(object sender, MouseEventArgs args)
        {
            ActualControl.ReleaseMouseCapture();

            //do thing only if we are outside the ScrollBar
            if (CheckMousePosition(args)) {
                Point pt = args.GetPosition(this);
                if ((items.Count > 0) &&
                    (pt.X < Width) && (pt.Y < Height)) {
                    double contentY = pt.Y - (double)(content.GetValue(Canvas.TopProperty));
                    int itemNumber = (int)(contentY / itemHeight);
                    FrameworkElement newSelection = itemNumber < items.Count ? items[itemNumber] : null;
                    if (selectedItem != newSelection) {
                        Select(newSelection);
                        if (SelectionChanged != null) {
                            SelectionChanged(this, null);
                        }
                    }
                }
            }
        }

        // When the mouse leaves the root visual we loose capture
        protected void OnRootLeave(object sender, EventArgs args)
        {
            ActualControl.ReleaseMouseCapture();
        }

        //Attaches the items to ListBox
        protected override void OnLoaded(object sender, RoutedEventArgs args)
        {
            base.OnLoaded(sender, args);
            UpdateItems();
        }

        //Sets the Scrollbar size and position if any is attached
        protected override void UpdateLayout()
        {
            base.UpdateLayout();
            //change the frame size too
            if (background != null) {
                background.Width = Width;
                if((scrollBar != null) && (scrollBar.Visibility == Visibility.Visible)){
                    background.Width -= scrollBar.Height;
                }
                background.Height = Height;
            }

            //place the ScrollBar if any
            if (scrollBar != null) {
                scrollBar.Size = Height;
                scrollBar.ViewportSize = Height;
                scrollBar.SetValue(Canvas.LeftProperty, Width);
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
            get { return "ListBox.xaml"; }
        }

        #endregion Protected Properties

        #region Private Methods

        // Sets the correct highlight rectangle and saves the selected item
        private void Select(FrameworkElement item)
        {
            if (item == null) {
                selectionHighlight.Visibility = Visibility.Collapsed;
            } else {
                selectionHighlight.SetValue(Canvas.LeftProperty, item.GetValue(Canvas.LeftProperty));
                selectionHighlight.SetValue(Canvas.TopProperty, item.GetValue(Canvas.TopProperty));
                selectionHighlight.Width = Width;
                selectionHighlight.Height = item.Height;
                selectionHighlight.Visibility = Visibility.Visible;
            }
            selectedItem = item;
        }

        //scrool by whole items
        private void OnScrollChanged(object sender, EventArgs args)
        {
            int temp = (int)(scrollBar.Value / itemHeight);
            content.SetValue(Canvas.TopProperty, -temp * itemHeight);
        }

        // Returns true if the mouse is outside of the ScrollBar area
        private bool CheckMousePosition(MouseEventArgs args)
        {
            bool onTheList = true;
            if ((scrollBar != null) && (scrollBar.Visibility == Visibility.Visible)) {
                Point pt = args.GetPosition(scrollBar);
                if ((pt.X < scrollBar.Width) && (pt.Y < scrollBar.Height)) {
                    onTheList = false;
                }
            }

            return onTheList;
        }

        #endregion Private Methods

        #region Data

        private Canvas content;  //contains the items so they can be scrolled if needed
        private double itemHeight; //all items must have equal height to allow correct scrolling
        private List<FrameworkElement> items = new List<FrameworkElement>();
        private FrameworkElement selectedItem = null;
        private Rectangle selectionHighlight;
        private Rectangle background = null;
        private ScrollBar scrollBar = null;

        #endregion Data
    }
}
