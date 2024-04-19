using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.ImageProcessing
{
    public struct RgbColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;

        public RgbColor(byte r, byte g, byte b, byte a)
        {
            A = a;
            R = r;
            G = g;
            B = b;
        }
    }
    public class RgbImage
    { 
        public RgbImage(int width, int height)
        {
            Colors = new RgbColor[height][];
            for (int i = 0; i < height; i++)
            {
                Colors[i] = new RgbColor[width];
            }
        }

        public int Height { get { return Colors.Length; } }
        public int Width { get { return Colors[0].Length; } }

        public RgbColor[][] Colors { get; set; }
        public RgbColor GetPixel(int x, int y)
        {
            return Colors[x][y];
        }
        public void SetPixel(int x, int y, RgbColor color)
        {
            this.Colors[x][y] = color;
        }
    }
}
