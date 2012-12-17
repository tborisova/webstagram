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
namespace MvcApplication1.Controllers
{
    public class myImageController : Controller
    {
        // GET: /myImage/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public object Image(HttpPostedFileBase fileUpload)
        {
            var stream = fileUpload.InputStream;
            Bitmap img = new Bitmap(stream);
            img.Save(string.Format(@"C:\Users\Dimitar\Pictures\test\{0} {1}.jpg", Path.GetFileNameWithoutExtension(fileUpload.FileName), DateTime.Now.Ticks));
            return img.ApplyFilters();
        }
    }
}
