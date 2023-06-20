﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.Animations
{
    public static class EasingFunctions
    {
        public static double InSine(double t)
        {
            return 1 - Math.Cos((t * Math.PI) / 2);
        }

        public static double OutSine(double t)
        {
            return Math.Sin((t * Math.PI) / 2);
        }

        public static double InOutSine(double t)
        {
            return -(Math.Cos(Math.PI * t) - 1) / 2;
        }
        public static double InQuad(double t)
        {
            return t * t;
        }
        public static double OutQuad(double t)
        {
            return 1 - (1 - t) * (1 - t);
        }
        public static double InOutQuad(double t)
        {
            if (t < 0.5)
                return 2 * t * t;
            else
                return 1 - 2 * (1 - t) * (1 - t);
        }
    }
}
