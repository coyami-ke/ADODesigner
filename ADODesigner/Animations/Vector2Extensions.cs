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
        public static bool IsInfinity(this Vector2 value)
        {
            if (float.IsInfinity(value.X) || float.IsInfinity(value.Y)) return false;
            return true;
        }
    }
}
