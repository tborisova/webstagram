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
            
            imageProcessor.ApplyInvert();
            result += BitmapToString(imageProcessor.GetImage());

            imageProcessor.ApplySepia(20);
            result += BitmapToString(imageProcessor.GetImage());

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