using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Normalize(float value, float minValue, float maxValue)
        {
            if (minValue == maxValue)
            {
                return 1f;
            }

            float min = Math.Min(minValue, maxValue);
            float max = Math.Max(minValue, maxValue);

            float result = (value - min) / (max - min);

            if (result < 0)
            {
                result = 1 + Math.Abs(result);
            }

            return result;
        }
        /// <summary>
        /// Normalizes a <see cref="Vector2"/> from 0 to 1.
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="minValue">Minimum value</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Normalized <see cref="Vector2"/></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Normalize(Vector2 value, Vector2 minValue, Vector2 maxValue)
        {
            Vector2 result;
            float x = Normalize(value.X, minValue.X, maxValue.X);
            float y = Normalize(value.Y, minValue.Y, maxValue.Y);
            result = new Vector2(x, y);
            return result;
        }
    }
}
