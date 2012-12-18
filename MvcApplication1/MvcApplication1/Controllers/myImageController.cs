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
    public class FiltersController : Controller
    {
        // GET: /Filters/

        public ActionResult Index()
        {
            return View();
        }
        private static Bitmap DroppedImage;
        private static String FileName;

        [HttpPost]
        public object GetThumbnails(HttpPostedFileBase fileUpload)
        {
            var stream = fileUpload.InputStream;
            Bitmap img = new Bitmap(stream);
            DroppedImage = img;
            FileName = fileUpload.FileName;
            int height = (int)(img.Height / ((float)img.Width / 500f));
            img = ResizeBitmap(img, 500, height);
            img.Save(string.Format(@"C:\Users\Dimitar\Pictures\test\{0} {1}.jpg",
                Path.GetFileNameWithoutExtension(FileName), DateTime.Now.Ticks));
            return img.ApplyFilters();
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

  
        //public FileResult ApplyFilter1(HttpPostedFileBase filterUpload)
        //{   //invert
        //    var stream = filterUpload.InputStream;
        //    Bitmap img = new Bitmap(stream);
        //    var ip = new ImageProcessor();
        //    ip.SetImage(img);
        //    var result = ip.Invert();
        //    MemoryStream str = new MemoryStream();
        //    result.Save(str, ImageFormat.Png);
        //    str.Close();
        //    return File(str.GetBuffer(),"application/octet-stream", string.Format("{0}_inverted.png", Path.GetFileNameWithoutExtension(filterUpload.FileName)));
        //}

        public FileResult Invert(HttpPostedFileBase filterUploads)
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

        public FileResult Grayscale(HttpPostedFileBase filterUploads)
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

        public FileResult ComboGamma(HttpPostedFileBase filterUploads)
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

        public FileResult RedGamma(HttpPostedFileBase filterUploads)
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

        public FileResult GreenGamma(HttpPostedFileBase filterUploads)
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

        public FileResult BlueGamma(HttpPostedFileBase filterUploads)
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

        public FileResult Sepia(HttpPostedFileBase filterUploads)
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

        public FileResult Contrast(HttpPostedFileBase filterUploads)
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

        public FileResult TrueRealContrast(HttpPostedFileBase filterUploads)
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

        public FileResult FalseRealContrast(HttpPostedFileBase filterUploads)
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

        public FileResult PositiveBrightness(HttpPostedFileBase filterUploads)
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
        public FileResult NegativeBrightness(HttpPostedFileBase filterUploads)
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
