using NCalc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jace;

namespace ADODesinger.Windows.Helpers
{
    public static class NCalcHelper
    {
        public static float GetFloatFromExperession(string expression)
        {
            if (float.TryParse(expression, System.Globalization.CultureInfo.InvariantCulture, out float result)) return (float)result;
            else
            {
                try
                {
                    CalculationEngine engine = new();
                    engine.AddConstant("pt", 1f / 150f);
                    engine.AddFunction("rad", DegreesToRadians);

                    return (float)engine.Calculate(expression);
                }
                catch { return -1; }
            }
        }
        public static double DegreesToRadians(double degrees)
        {
            return degrees * double.Pi / 180;
        }
    }
}
