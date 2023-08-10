using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ADODesigner.Animations
{
    /// <summary>
    /// Math Functions for working with animations.
    /// </summary>
    public static class MathFunctions
    {
        /// <summary>
        /// Normalizes a number from 0 to 1.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Normalized number</returns>
        public static float Normalize(float value, float minValue, float maxValue)
        {
            float result = (value - minValue) / (maxValue - minValue);
            if (float.IsInfinity(result)) return 1;
            else return result;
        }
        /// <summary>
        /// Normalizes a <see cref="Vector2"/> from 0 to 1.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Normalized <see cref="Vector2"/></returns>
        public static Vector2 Normalize(Vector2 value, Vector2 minValue, Vector2 maxValue)
        {
            Vector2 result = (value - minValue) / (maxValue - minValue);
            if (result.IsInfinity()) return new(1, 1);
            else return result;
        }
    }
}
