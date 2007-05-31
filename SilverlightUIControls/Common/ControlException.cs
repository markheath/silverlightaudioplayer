//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;
using System.Windows;

namespace Silverlight.Samples.Controls {

    // Defines custom exception for sample controls
    public class ControlException : Exception {
        #region Public Methods

        // The only ctor for the moment calls the base class
        public ControlException(string message)
            : base(message)
        {
        }

        #endregion Public Methods
    }
}
