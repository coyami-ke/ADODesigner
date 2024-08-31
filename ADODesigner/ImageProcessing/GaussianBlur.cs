using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADODesigner.ImageProcessing
{
    public static class GaussianBlur
    {
        public static RgbImage ApplyGaussianBlur(RgbImage image, int radius)
        {
            RgbImage blurredImage = new RgbImage(image.Width, image.Height);

            double[,] kernel = GenerateGaussianKernel(radius);

            int kernelSize = kernel.GetLength(0);
            int kernelOffset = kernelSize / 2;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    double r = 0, g = 0, b = 0, a = 0, weightSum = 0;

                    for (int i = 0; i < kernelSize; i++)
                    {
                        for (int j = 0; j < kernelSize; j++)
                        {
                            int offsetX = x + i - kernelOffset;
                            int offsetY = y + j - kernelOffset;

                            if (offsetX >= 0 && offsetX < image.Width && offsetY >= 0 && offsetY < image.Height)
                            {
                                RgbColor pixel = image.GetPixel(offsetX, offsetY);
                                double weight = kernel[i, j];
                                r += pixel.R * weight;
                                g += pixel.G * weight;
                                b += pixel.B * weight;
                                a = pixel.A;
                                weightSum += weight;
                            }
                        }
                    }

                    r /= weightSum;
                    g /= weightSum;
                    b /= weightSum;
                    a /= weightSum;

                    RgbColor color = new() { R = (byte)r, G = (byte)g, B = (byte)b, A = (byte)a };

                    blurredImage.SetPixel(x, y, color);
                }
            }

            return blurredImage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radius">Radius</param>
        /// <returns>Gaussian Kernel</returns>
        public static double[,] GenerateGaussianKernel(int radius)
        {
            int kernelSize = 2 * radius + 1;
            double[,] kernel = new double[kernelSize, kernelSize];
            double sigma = radius / 3.0;

            double constant = 1.0 / (2 * Math.PI * sigma * sigma);
            double sum = 0;

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    double exponent = -(x * x + y * y) / (2 * sigma * sigma);
                    kernel[x + radius, y + radius] = constant * Math.Exp(exponent);
                    sum += kernel[x + radius, y + radius];
                }
            }

            for (int x = 0; x < kernelSize; x++)
            {
                for (int y = 0; y < kernelSize; y++)
                {
                    kernel[x, y] /= sum;
                }
            }

            return kernel;
        }
    }
}
