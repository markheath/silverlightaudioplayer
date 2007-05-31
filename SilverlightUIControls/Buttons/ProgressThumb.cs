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

    // Implements a Thumb control. The thumb is a shape that gets highlighted when the mouse is 
    // over and translated slightly when the left mouse button is down. It moves with the mouse in 
    // horizontal or vertical direction and sends DragStart, DragEnd, DragDelta events. It also 
    // a Value propertie (set and get) which represents the offset of 
    // the Thumb from its initial position. 
    public class ProgressThumb : Thumb {

        #region Public Methods

        public ProgressThumb()
        {
        }

        #endregion Public Methods

        // Moves the Thumb and notifies the listeners
        protected void OnMouseMove(object sender, MouseEventArgs args)
        {
            base.OnMouseMove(sender,args);
        }


        #region Protected Properties

        // The resource name used to initialize the actual object
        protected override string ResourceName
        {
            get { return "ProgressThumb.xaml"; }
        }

        #endregion Protected Properties

    }
}
