using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ImageProcessing;

namespace CustomApplyFilterExtension
{
    public static class CustomApplyFilters
    {
        public static ImageProcessor imageProcessor = new ImageProcessor();

        public static string ApplyFilters(this Bitmap img)
        {
            imageProcessor.SetImage(img);
            string result = null;
            
            //imageProcessor.ApplyInvert();
            //result += BitmapToString(imageProcessor.GetImage());

            //imageProcessor.ApplySepia(20);
            //result += BitmapToString(imageProcessor.GetImage());
            result += BitmapToString(imageProcessor.Invert());
            result += BitmapToString(imageProcessor.Grayscale());
            result += BitmapToString(imageProcessor.Gamma(0.1,0.5,0.9)); // combo gamma filter from 0.0 to 1.0
            result += BitmapToString(imageProcessor.Gamma(1.0, 0.0, 0.0)); // red gamma filter
            result += BitmapToString(imageProcessor.Gamma(0.0, 1.0, 0.0)); // green gamma filter
            result += BitmapToString(imageProcessor.Gamma(0.0, 0.0, 1.0)); // blue gamma filter
            return result;
        }

        private static string BitmapToString(Bitmap img)
        {
            string bitmapString = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                img.Save(memoryStream, ImageFormat.Jpeg);
                byte[] bitmapBytes = memoryStream.GetBuffer();
                bitmapString = Convert.ToBase64String(bitmapBytes, Base64FormattingOptions.InsertLineBreaks);
            }
            bitmapString = "data:image/jpeg;base64," + bitmapString + " ";
            return bitmapString;
        }
    }
}