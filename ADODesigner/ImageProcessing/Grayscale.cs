using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.ImageProcessing
{
    public static class Grayscale
    {
        public static RgbImage ApplyGrayscale(RgbImage image)
        {
            RgbImage grayscaleImage = new RgbImage(image.Width, image.Height);

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    RgbColor originalColor = image.GetPixel(x, y);
                    byte grayValue = (byte)(0.299 * originalColor.R + 0.587 * originalColor.G + 0.114 * originalColor.B);
                    RgbColor grayscaleColor = new RgbColor() { R = grayValue, G = grayValue, B = grayValue, A = originalColor.A };
                    grayscaleImage.SetPixel(x, y, grayscaleColor);
                }
            }
            return grayscaleImage;
        }
    }
}
