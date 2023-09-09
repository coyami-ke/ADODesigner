using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public static class Vector2Extensions
    {
        public static bool IsNan(this Vector2 value)
        {
            if (float.IsNaN(value.X) || float.IsNaN(value.Y)) return true;
            return false;
        }
        public static Vector2 Add(this Vector2 value1, float value2) 
        {
            return new(value1.X + value2, value1.Y + value2);
        }
        public static Vector2 Max(Vector2 value1, Vector2 value2)
        {
            return new(Math.Max(value1.X, value2.X), Math.Max(value1.Y, value2.Y));
        }
    }
}
