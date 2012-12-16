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
        //
        // GET: /myImage/

        public ActionResult Index()
        {
           // it doesnt even have Page
            // INITIALIZING SEARCH SESSION...
            return View();
        }

        [HttpPost]
        public object Image(HttpPostedFileBase fileUpload)
        {
           
            var stream = fileUpload.InputStream;
            Bitmap img = new Bitmap(stream);
            img.Save(string.Format(@"C:\Users\Dimitar\Pictures\test\{0} {1}.jpg", Path.GetFileNameWithoutExtension(fileUpload.FileName), DateTime.Now.Ticks));
            //var writer = System.IO.File.OpenWrite(string.Format(@"C:\Users\Dimitar\Pictures\test\{0} {1}.jpg", Path.GetFileNameWithoutExtension(fileUpload.FileName), DateTime.Now.Millisecond));
            //byte[] buffer = new byte[2048];
            //while (stream.Read(buffer, 0, buffer.Length) != 0)
            //{
            //    writer.Write(buffer, 0, buffer.Length);
            //}
           
            //writer.Flush();
            //writer.Close();
            
           // FileContentResult[] responses = new FileContentResult[10];
           // Response.AppendHeader("img1", BitmapToString(img));
            //oh i dunno too tired
            // vij kak otgore na twa ima httpPost
            // dali nqma httpGETi da go vikame ot tuk oh nz :D
         //   Response.AppendHeader("img2", BitmapToString(img));

          //  String result = BitmapToString(img);
          //  result += BitmapToString(img);
            //XDocument imageXML = new XDocument(
            //    new XDeclaration("1.0", "utf-8", "yes"),
            //    new XElement("Images",
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "1")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "2")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "3")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "4")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "5")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "6")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "7")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "8")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "9")),
            //                        new XElement("Image", BitmapToString(img), new XAttribute("Filter", "0"))
            //                )
            //);
            //imageXML.Save(@"C:\Users\Dimitar\Pictures\test\mi.xml");

            //img.ApplyFilters();

            //string result = BitmapToString(img);
            //result += " ";
            //result += BitmapToString(img);
            return img.ApplyFilters();
        }
    }
}
