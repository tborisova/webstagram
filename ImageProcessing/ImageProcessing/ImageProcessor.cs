using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;

namespace ImageProcessing
{
    /// <summary>
    /// The ImageProcessor class that can apply different Filters to a Bitmap image.
    /// </summary>
    public class ImageProcessor
    {
        private Bitmap bitmapImage;
        private int Width;
        private int Height;
        private BitmapData bitmapData1;

        /// <summary>
        /// Sets an image to the ImageProcessor via path.
        /// </summary>
        /// <param name="path">The path to the image.</param>
        public void SetImage(string path)
        {
            bitmapImage = new Bitmap(path);
            SetImageParams();
        }

        /// <summary>
        /// Sets an image to the ImageProcessor via a Bitmap.
        /// </summary>
        /// <param name="img">A Bitmap instance.</param>
        public void SetImage(Bitmap img)
        {
            bitmapImage = img;
            SetImageParams();
        }
        /// <summary>
        /// Sets the parameters of the Bitmap image.
        /// </summary>
        private void SetImageParams()
        {
            Width = bitmapImage.Width;
            Height = bitmapImage.Height;
            bitmapData1 = bitmapImage.LockBits(new Rectangle(0, 0,
                                     bitmapImage.Width, bitmapImage.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
        }
        /// <summary>
        /// Returns the Bitmap image of the ImageProcessor.
        /// </summary>
        /// <returns>A Bitmap image.</returns>
        public Bitmap GetImage()
        {
            return bitmapImage;
        }
        /// <summary>
        /// Unlocks the Bitmap image.
        /// </summary>
        public void UnlockImage()
        {
            bitmapImage.UnlockBits(bitmapData1);
        }

        /// <summary>
        /// Applies Grayscale filter to the image.
        /// </summary>
        /// <returns>A Bitmap image.</returns>
        public Bitmap Grayscale()
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                   PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            int a = 0;
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Grayscale algorithm
                        a = (imagePointer1[0] + imagePointer1[1] + imagePointer1[2]) / 3;
                        imagePointer2[0] = (byte)a;
                        imagePointer2[1] = (byte)a;
                        imagePointer2[2] = (byte)a;
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }


        /// <summary>
        /// Applies Invert filter to the image.
        /// </summary>
        /// <returns>A Bitmap image.</returns>
        public Bitmap Invert()
        {
            Bitmap returnMap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Invert algorithm
                        imagePointer2[0] = (byte)(255 - imagePointer1[0]);
                        imagePointer2[1] = (byte)(255 - imagePointer1[1]);
                        imagePointer2[2] = (byte)(255 - imagePointer1[2]);
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }

        /// <summary>
        /// Applies Gamma Filter to the image.
        /// </summary>
        /// <param name="r">The value of the red gamma.</param>
        /// <param name="g">The value of the green gamma.</param>
        /// <param name="b">The value of the blue gamma.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap Gamma(double r, double g, double b)
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                    PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);

            byte[] redGamma = new byte[256];
            byte[] greenGamma = new byte[256];
            byte[] blueGamma = new byte[256];

            // Calculates the new gamma of each color from 0 to 255.
            for (int i = 0; i < 256; ++i)
            {
                redGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / r)) + 0.5));

                greenGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / g)) + 0.5));

                blueGamma[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / b)) + 0.5));
            }

            //int a = 0;
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Gamma algorithm
                        imagePointer2[0] = blueGamma[imagePointer1[0]];
                        imagePointer2[1] = greenGamma[imagePointer1[1]];
                        imagePointer2[2] = redGamma[imagePointer1[2]];
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }

        /// <summary>
        /// Applies Color filter to the image.
        /// </summary>
        /// <param name="r">The value of the red gamma.</param>
        /// <param name="g">The value of the green gamma.</param>
        /// <param name="b">The value of the blue gamma.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap ColorFilter(double r, double g, double b)
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                   PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Color Filter algorithm
                        imagePointer2[0] = (byte)(imagePointer1[0] * b);
                        imagePointer2[1] = (byte)(imagePointer1[1] * g);
                        imagePointer2[2] = (byte)(imagePointer1[2] * r);
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }


        /// <summary>
        /// Applies Sepia filter to the image.
        /// </summary>
        /// <param name="depth">The value of the depth of the Sepia filter</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap Sepia(int depth)
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                    PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Sepia algorithm
                        imagePointer2[0] = (byte)((0.299 * imagePointer1[2]) + (0.587 * imagePointer1[1]) + (0.114 * imagePointer1[0]));
                        if (imagePointer2[0] + depth > 255)
                        {
                            imagePointer2[1] = 255;
                        }
                        else
                        {
                            imagePointer2[1] = (byte)(imagePointer2[0] + depth);
                        }
                        if (imagePointer2[0] + (2 * depth) > 255)
                        {
                            imagePointer2[2] = 255;
                        }
                        else
                        {
                            imagePointer2[2] = (byte)(imagePointer2[0] + (2 * depth));
                        }
                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }

        /// <summary>
        /// Applies Contrast filter to the image.
        /// </summary>
        /// <param name="contrast">The value of the strenght of the contrast.</param>
        /// <param name="fake">Fake or real contrast</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap Contrast(double contrast, bool fake)
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                    PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            if (!fake)
            {
                contrast = (100.0 + contrast) / 100.0;
                contrast *= contrast;
            }
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Contrast algorithm
                        imagePointer2[2] = (byte)(imagePointer1[2] / (byte)(255));
                        imagePointer2[2] -= (byte)(0.5);
                        imagePointer2[2] *= (byte)contrast;
                        imagePointer2[2] += (byte)(0.5);
                        imagePointer2[2] *= (byte)(255);
                        if (imagePointer2[2] > 255)
                        {
                            imagePointer2[2] = 255;
                        }
                        else if (imagePointer2[2] < 0)
                        {
                            imagePointer2[2] = 0;
                        }

                        imagePointer2[1] = (byte)(imagePointer1[1] / (byte)(255));
                        imagePointer2[1] -= (byte)(0.5);
                        imagePointer2[1] *= (byte)contrast;
                        imagePointer2[1] += (byte)(0.5);
                        imagePointer2[1] *= (byte)(255);
                        if (imagePointer2[1] > 255)
                        {
                            imagePointer2[1] = 255;
                        }
                        else if (imagePointer2[1] < 0)
                        {
                            imagePointer2[1] = 0;
                        }

                        imagePointer2[0] = (byte)(imagePointer1[0] / (byte)(255));
                        imagePointer2[0] -= (byte)(0.5);
                        imagePointer2[0] *= (byte)contrast;
                        imagePointer2[0] += (byte)(0.5);
                        imagePointer2[0] *= (byte)(255);
                        if (imagePointer2[0] > 255)
                        {
                            imagePointer2[0] = 255;
                        }
                        else if (imagePointer2[0] < 0)
                        {
                            imagePointer2[0] = 0;
                        }

                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }
        /// <summary>
        /// Applies a custom REAL Contrast filter to the image.
        /// </summary>
        /// <param name="contrast">The value of the strenght of the contrast.</param>
        /// <param name="fake">Whether the filter should be natural or fake.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap RealContrast(double contrast, bool fake)
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                   PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            if (!fake)
            {
                contrast = (100.0 + contrast) / 100.0;
                contrast *= contrast;
            }

            double C;

            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // A custom Contrast algorithm
                        C = imagePointer1[2];
                        C /= 255.0;
                        C -= 0.5;
                        C *= contrast;
                        C += 0.5;
                        C *= 255;
                        if (C > 255)
                        {
                            C = 255;
                        }
                        else if (C < 0)
                        {
                            C = 0;
                        }

                        imagePointer2[2] = (byte)C;

                        C = imagePointer1[1];
                        C /= 255.0;
                        C -= 0.5;
                        C *= contrast;
                        C += 0.5;
                        C *= 255;
                        if (C > 255)
                        {
                            C = 255;
                        }
                        else if (C < 0)
                        {
                            C = 0;
                        }

                        imagePointer2[1] = (byte)C;

                        C = imagePointer1[0];
                        C /= 255.0;
                        C -= 0.5;
                        C *= contrast;
                        C += 0.5;
                        C *= 255;
                        if (C > 255)
                        {
                            C = 255;
                        }
                        else if (C < 0)
                        {
                            C = 0;
                        }

                        imagePointer2[0] = (byte)C;


                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }

        /// <summary>
        /// Applies Brightness filter to the image.
        /// </summary>
        /// <param name="brightness">The value of the strenght of the brightness filter.</param>
        /// <returns>A Bitmap image.</returns>
        public Bitmap Brightness(int brightness)
        {
            Bitmap returnMap = new Bitmap(Width, Height,
                                    PixelFormat.Format32bppArgb);
            BitmapData bitmapData2 = returnMap.LockBits(new Rectangle(0, 0,
                                     returnMap.Width, returnMap.Height),
                                     ImageLockMode.ReadOnly,
                                     PixelFormat.Format32bppArgb);
            unsafe
            {
                byte* imagePointer1 = (byte*)bitmapData1.Scan0;
                byte* imagePointer2 = (byte*)bitmapData2.Scan0;
                for (int i = 0; i < bitmapData1.Height; i++)
                {
                    for (int j = 0; j < bitmapData1.Width; j++)
                    {
                        // Standard Brightness algorithm
                        imagePointer2[0] = (byte)(imagePointer1[0] + brightness);
                        if (imagePointer2[0] > 255)
                        {
                            imagePointer2[0] = 255;
                        }
                        else if (imagePointer2[0] < 0)
                        {
                            imagePointer2[0] = 0;
                        }

                        imagePointer2[1] = (byte)(imagePointer1[1] + brightness);
                        if (imagePointer2[1] > 255)
                        {
                            imagePointer2[1] = 255;
                        }
                        else if (imagePointer2[1] < 0)
                        {
                            imagePointer2[1] = 0;
                        }

                        imagePointer2[2] = (byte)(imagePointer1[2] + brightness);
                        if (imagePointer2[2] > 255)
                        {
                            imagePointer2[2] = 255;
                        }
                        else if (imagePointer2[2] < 0)
                        {
                            imagePointer2[2] = 0;
                        }


                        imagePointer2[3] = imagePointer1[3];
                        //4 bytes per pixel
                        imagePointer1 += 4;
                        imagePointer2 += 4;
                    }
                    //4 bytes per pixel
                    imagePointer1 += bitmapData1.Stride - (bitmapData1.Width * 4);
                    imagePointer2 += bitmapData1.Stride - (bitmapData1.Width * 4);
                }
            }
            returnMap.UnlockBits(bitmapData2);
            return returnMap;
        }
    }
}