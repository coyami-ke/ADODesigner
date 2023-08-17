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
    }
}
