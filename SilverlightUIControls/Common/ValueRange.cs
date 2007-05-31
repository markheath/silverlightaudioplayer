//------------------------------------------------------------
//  Copyright(c) Microsoft Corporation. All rights reserved.
//------------------------------------------------------------

using System;

namespace Silverlight.Samples.Controls {

    // Represents the slider and ScrollBarr vlue range 
    public sealed class ValueRange {

        #region Public memebers

        // ctor Creates the range from min and max value
        public ValueRange(double minimum, double maximum)
        {
            if (minimum > maximum) {
                throw new ControlException("negative range is not supported");
            }
            min = minimum;
            max = maximum;
        }

        // Min value getter
        public double Min
        {
            get { return min; }
        }

        // Max value getter
        public double Max
        {
            get { return max; }
        }

        // Calculates the range
        public double Range
        {
            get { return Max - Min; }
        }

        #endregion Public memebers

        #region Private members

        private double min;  //range min value
        private double max;  //range max value

        #endregion Private members

    }

}
