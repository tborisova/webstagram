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
            img.Save(string.Format(@"C:\Users\Dimitar\Pictures\test\{0} {1}.jpg", Path.GetFileNameWithoutExtension(FileName), DateTime.Now.Ticks));
            return img.ApplyFilters();
        }

        private Bitmap ResizeBitmap(Bitmap sourceBMP, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
                g.DrawImage(sourceBMP, 0, 0, width, height);
            return result;
        }

  
        public FileResult ApplyFilter1(HttpPostedFileBase filterUpload)
        {   //invert
            var stream = filterUpload.InputStream;
            Bitmap img = new Bitmap(stream);
            var ip = new ImageProcessor();
            ip.SetImage(img);
            var result = ip.Invert();
            MemoryStream str = new MemoryStream();
            result.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(),"application/octet-stream", string.Format("{0}_inverted.png", Path.GetFileNameWithoutExtension(filterUpload.FileName)));
        }

        public FileResult Invert(HttpPostedFileBase filterUploads) 
        {   //invert
            var ip = new ImageProcessor();
            ip.SetImage(DroppedImage);
            var result = ip.Invert();
            ip.UnlockImage();
            MemoryStream str = new MemoryStream();
            result.Save(str, ImageFormat.Png);
            str.Close();
            return File(str.GetBuffer(),"application/octet-stream", string.Format("{0}_inverted.png", Path.GetFileNameWithoutExtension(FileName)));
        }

    }
}
