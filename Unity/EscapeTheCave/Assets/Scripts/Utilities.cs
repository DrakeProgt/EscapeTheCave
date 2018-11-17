using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

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

        /// <summary>
        /// Return -1 when the target direction is left, +1 when it is right and 0 if the direction is straight ahead or behind.
        /// </summary>
        public static float GetDirection(Transform startTrans, Transform targetTrans)
        {
            Vector3 heading = targetTrans.position - startTrans.position;
            Vector3 perp = Vector3.Cross(startTrans.forward, heading);
            float dir = Vector3.Dot(perp, startTrans.up);

            if (dir > 0f)
            {
                return 1f;
            }
            else if (dir < 0f)
            {
                return -1f;
            }
            else
            {
                return 0f;
            }
        }

        /// <summary>
        /// Let the XBox Controller vibrate.
        /// </summary>
        /// <param name="leftIntensity">Intensity of the left controller motor.</param>
        /// <param name="rightIntensity">Intensity of the right controller motor.</param>
        /// <param name="duration">How long should the vibration last in seconds?</param>
        public static IEnumerator ControllerVibration(float leftIntensity, float rightIntensity, float duration)
        {
            GamePad.SetVibration(PlayerIndex.One, leftIntensity, rightIntensity);
            yield return new WaitForSecondsRealtime(duration);
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
        }
    }
}
