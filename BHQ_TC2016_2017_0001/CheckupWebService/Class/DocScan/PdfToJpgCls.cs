using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Ghostscript.NET;
using Ghostscript.NET.Rasterizer;

namespace CheckupWebService.Class.DocScan
{
    public partial class PdfToJpgCls
    {
        public PdfToJpgCls()
        {

        }

        private void Export(string inputPDFFile, string outputImagesPath)
        {
            try
            {
                GhostscriptVersionInfo _lastInstalledVersion = null;
                GhostscriptRasterizer _rasterizer = null;
                int desired_x_dpi = 96;
                int desired_y_dpi = 96;

                _lastInstalledVersion =
                    GhostscriptVersionInfo.GetLastInstalledVersion(
                            GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                            GhostscriptLicense.GPL);

                _rasterizer = new GhostscriptRasterizer();

                _rasterizer.Open(inputPDFFile, _lastInstalledVersion, false);

                for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
                {
                    string pageFilePath = Path.Combine(outputImagesPath, "Page-" + pageNumber.ToString("00") + ".jpg");

                    Image img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                    img.Save(pageFilePath, ImageFormat.Jpeg);
                }
                _rasterizer.Dispose();
            }
            catch
            {

            }
        }

        public List<ImageBinary> Export(Stream streamPDF)
        {
            try
            {
                GhostscriptVersionInfo _lastInstalledVersion = null;
                GhostscriptRasterizer _rasterizer = null;
                int desired_x_dpi = 96;
                int desired_y_dpi = 96;

                _lastInstalledVersion =
                    GhostscriptVersionInfo.GetLastInstalledVersion(
                            GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                            GhostscriptLicense.GPL);

                _rasterizer = new GhostscriptRasterizer();

                _rasterizer.Open(streamPDF, _lastInstalledVersion, true);

                List<ImageBinary> imgBi = new List<ImageBinary>();
                for (int pageNumber = 1; pageNumber <= _rasterizer.PageCount; pageNumber++)
                {
                    Image img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                    imgBi.Add(new ImageBinary { page_no = pageNumber, img = imageToByteArray(img) });
                }
                _rasterizer.Dispose();
                return imgBi;
            }
            catch
            {
                return new List<ImageBinary>();
            }
        }
        public List<ImageBinary> Export(Stream streamPDF, List<int> listPage)
        {
            try
            {
                GhostscriptVersionInfo _lastInstalledVersion = null;
                GhostscriptRasterizer _rasterizer = null;
                int desired_x_dpi = 300;
                int desired_y_dpi = 300;

                _lastInstalledVersion =
                    GhostscriptVersionInfo.GetLastInstalledVersion(
                            GhostscriptLicense.GPL | GhostscriptLicense.AFPL,
                            GhostscriptLicense.GPL);

                _rasterizer = new GhostscriptRasterizer();

                _rasterizer.Open(streamPDF, _lastInstalledVersion, true);

                List<ImageBinary> imgBi = new List<ImageBinary>();
                foreach (int pageNumber in listPage)
                {

                    Image img = _rasterizer.GetPage(desired_x_dpi, desired_y_dpi, pageNumber);
                    imgBi.Add(new ImageBinary { page_no = pageNumber, img = imageToByteArray(img) });
                }
                _rasterizer.Dispose();
                return imgBi;
            }
            catch
            {
                return new List<ImageBinary>();
            }
        }

        public class ImageBinary
        {
            public int page_no { get; set; }
            public byte[] img { get; set; }
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }
    }
}