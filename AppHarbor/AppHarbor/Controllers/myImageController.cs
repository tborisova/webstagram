using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Drawing.Imaging;
using System.Xml.Linq;
using CustomApplyFilterExtension;
using ImageProcessing;

namespace MvcApplication1.Controllers
{
    /// <summary>
    /// A Controller for the Click2Download functionality of the app
    /// </summary>
    public class FiltersController : Controller
    {
        // GET: /Filters/

        public ActionResult Index()
        {
            return View();
        }
        // Saves the dropped image as a variable. 
        private static Bitmap DroppedImage;
        private static String FileName;

        /// <summary>
        /// Returns all thumbnail filtered images for priview as a ssv.
        /// </summary>
        /// <param name="fileUpload">The uploaded file in the HTTP Request</param>
        /// <returns>An ssv format string containing all filtered images.</returns>
        [HttpPost]
        public object GetThumbnails(HttpPostedFileBase fileUpload)
        {
            var stream = fileUpload.InputStream;
            Bitmap img = new Bitmap(stream);
            DroppedImage = img;
            FileName = fileUpload.FileName;
            int height = (int)(img.Height / ((float)img.Width / 500f));
            img = ResizeBitmap(img, 500, height);
            //img.Save(string.Format(@"C:\Users\Dimitar\Pictures\test\{0} {1}.jpg",
            //    Path.GetFileNameWithoutExtension(FileName), DateTime.Now.Ticks));
            return img.ApplyFilters();
        }

        /// <summary>
        /// Resizes a Bitmap image.
        /// </summary>
        /// <param name="sourceBMP">The Bitmap image to be resized.</param>
        /// <param name="width">New width.</param>
        /// <param name="height">New height.</param>
        /// <returns>The resized Bitmap image.</returns>
        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

        /// <summary>
        /// Returns the Inverted image.
        /// </summary>
        /// <returns>The Inverted image as a FileResult to be downloaded.</returns>
        public FileResult Invert()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Invert();
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_Invert.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Grayscaled image.
        /// </summary>
        /// <returns>The Grayscaled image as a FileResult to be downloaded.</returns>
        public FileResult Grayscale()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Grayscale();
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_Grayscale.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Combo Gamma image.
        /// </summary>
        /// <returns>The Combo Gamma image as a FileResult to be downloaded.</returns>
        public FileResult ComboGamma()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Gamma(0.1, 0.5, 0.9);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_ComboGamma.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Red Gamma image.
        /// </summary>
        /// <returns>The Red Gamma image as a FileResult to be downloaded.</returns>
        public FileResult RedGamma()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Gamma(1.0, 0.0, 0.0);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_RedGamma.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Green Gamma image.
        /// </summary>
        /// <returns>The Green Gamma image as a FileResult to be downloaded.</returns>
        public FileResult GreenGamma()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Gamma(0.0, 1.0, 0.0);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_GreenGamma.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Blue Gamma image.
        /// </summary>
        /// <returns>The Blue Gamma image as a FileResult to be downloaded.</returns>
        public FileResult BlueGamma()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Gamma(0.0, 0.0, 1.0);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_BlueGamma.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Sepia image.
        /// </summary>
        /// <returns>The Sepia image as a FileResult to be downloaded.</returns>
        public FileResult Sepia()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Sepia(20);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_Sepia.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Contrast image.
        /// </summary>
        /// <returns>The Contrast image as a FileResult to be downloaded.</returns>
        public FileResult Contrast()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Contrast(50, false);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_Contrast.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the True Real Contrast image.
        /// </summary>
        /// <returns>The True Real Contrast image as a FileResult to be downloaded.</returns>
        public FileResult TrueRealContrast()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.RealContrast(50, true);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_RealContrast.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the False Real Contrast image.
        /// </summary>
        /// <returns>The False Real Contrast image as a FileResult to be downloaded.</returns>
        public FileResult FalseRealContrast()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.RealContrast(50, false);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_FalseRealContrast.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Positive Brightness image.
        /// </summary>
        /// <returns>The Positive Brightness image as a FileResult to be downloaded.</returns>
        public FileResult PositiveBrightness()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Brightness(50);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_PositiveBrightness.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }

        /// <summary>
        /// Returns the Negative Brightness image.
        /// </summary>
        /// <returns>The Negative Brightness image as a FileResult to be downloaded.</returns>
        public FileResult NegativeBrightness()
        {
            var ImageProcessor = new ImageProcessor();
            ImageProcessor.SetImage(DroppedImage);
            var resultImg = ImageProcessor.Brightness(-50);
            ImageProcessor.UnlockImage();
            MemoryStream str = new MemoryStream();
            resultImg.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(), "application/octet-stream",
                string.Format("{0}_NegativeBrightness.png",
                Path.GetFileNameWithoutExtension(FileName)));
        }
    }
}
