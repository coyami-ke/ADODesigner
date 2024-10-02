using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ADODesigner.Windows.Helpers
{
    public static class HexColorConverter
    {
        private static readonly Regex RegexHexRGB = new(@"^#?([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})([a-fA-F0-9]{2})?$", RegexOptions.Compiled);

        public static Color HexToColor(string hex)
        {
            Match match = RegexHexRGB.Match(hex);
            if (match.Success)
            {
                byte a = 255; // Default alpha value
                byte r = HexToByte(match.Groups[1].Value);
                byte g = HexToByte(match.Groups[2].Value);
                byte b = HexToByte(match.Groups[3].Value);

                // If alpha component is present, parse it
                if (match.Groups[4].Success && !string.IsNullOrEmpty(match.Groups[4].Value))
                {
                    a = HexToByte(match.Groups[4].Value);
                }

                return Color.FromArgb(a, r, g, b);
            }
            return Color.FromRgb(255, 255, 255); // Default color if parsing fails
        }
        public static string ColorToHex(Color color)
        {
            return color.R.ToString("X02", CultureInfo.InvariantCulture) + color.G.ToString("X02", CultureInfo.InvariantCulture) + color.B.ToString("X02", CultureInfo.InvariantCulture) + color.A.ToString("X02", CultureInfo.InvariantCulture);
        }
        private static byte HexToByte(string hex)
        {
            if (string.IsNullOrEmpty(hex) || hex.Length != 2)
            {
                throw new ArgumentException("Hex string must be exactly 2 characters long.");
            }

            try
            {
                return byte.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid hex string format.");
            }
            catch (OverflowException)
            {
                throw new ArgumentException("Hex string represents a value that is too large.");
            }
        }
    }
}
