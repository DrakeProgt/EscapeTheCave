using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public static class Utilities
    {
        /// <summary>
        /// Normalize an input value. 
        /// For exampe use for controlling effects (e.g. blur intensity = return value) with pulse (inValue).
        /// </summary>
        /// <param name="inValue">The input value to be normalized.</param>
        /// <param name="inLower">The smallest possible input value.</param>
        /// <param name="inUpper">The highest possible input value.</param>
        /// <param name="outLower">The smallest possible output value.</param>
        /// <param name="outUpper">The highest possible output value.</param>
        /// <returns>The normalized output value.</returns>
        public static float Norm(float inValue, float inLower, float inUpper, float outLower, float outUpper)
        {
            //pulse (input value) for example is between 40 (inLower) and 200 (inUpper) 
            //and blur intensity (return value) should be between 0.1 (outLower) and 1.0 (outUpper)
            float normValue = outLower + (outUpper - outLower) * ((inValue - inLower) / (inUpper - inLower));
            return normValue;
        }
    }
}
