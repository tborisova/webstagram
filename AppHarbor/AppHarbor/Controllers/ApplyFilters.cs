using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ImageProcessing;
using System.Diagnostics;

namespace CustomApplyFilterExtension
{
    public static class CustomApplyFilters
    {
        public static ImageProcessor imageProcessor = new ImageProcessor();

        public static string ApplyFilters(this Bitmap img)
        {
            imageProcessor.SetImage(img);
            string result = null;

            Stopwatch s = new Stopwatch();
            s.Start();

            // Addes all filterted images as strings to the HTTP Response

            result += BitmapToString(imageProcessor.Invert());
            result += BitmapToString(imageProcessor.Grayscale());
            result += BitmapToString(imageProcessor.Gamma(0.1, 0.5, 0.9)); // combo gamma filter from 0.0 to 1.0
            result += BitmapToString(imageProcessor.Gamma(1.0, 0.0, 0.0)); // red gamma filter
            result += BitmapToString(imageProcessor.Gamma(0.0, 1.0, 0.0)); // green gamma filter
            result += BitmapToString(imageProcessor.Gamma(0.0, 0.0, 1.0)); // blue gamma filter
            //result += BitmapToString(imageProcessor.ColorFilter(1.0, 0.0, 0.0)); // red filter
            //result += BitmapToString(imageProcessor.ColorFilter(0.0, 1.0, 0.0)); // green filter
            //result += BitmapToString(imageProcessor.ColorFilter(0.0, 0.0, 1.0)); // blue filter
            result += BitmapToString(imageProcessor.Sepia(20));
            //result += BitmapToString(imageProcessor.Contrast(50, true));
            result += BitmapToString(imageProcessor.Contrast(50, false));
            result += BitmapToString(imageProcessor.RealContrast(50, true));
            result += BitmapToString(imageProcessor.RealContrast(50, false));
            //imageProcessor.ApplyContrast(50);
            //result += BitmapToString(imageProcessor.GetImage());
            result += BitmapToString(imageProcessor.Brightness(50));
            result += BitmapToString(imageProcessor.Brightness(-50));
            //result += BitmapToString(imageProcessor.MyBlur(13));
            imageProcessor.UnlockImage();

            s.Stop();
            result = s.ElapsedMilliseconds + " " + result;
            return result;
        }

        private static string BitmapToString(Bitmap img)
        {
            string bitmapString = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, ImageFormat.Jpeg);
                byte[] bitmapBytes = memoryStream.GetBuffer();
                bitmapString = Convert.ToBase64String(bitmapBytes,
                    Base64FormattingOptions.InsertLineBreaks);
            }
            bitmapString = "data:image/jpeg;base64," + bitmapString + " ";
            return bitmapString;
        }

    }
}