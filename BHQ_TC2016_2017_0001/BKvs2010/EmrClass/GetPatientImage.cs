using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Net;
using System.Data;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class GetPatientImage
    {
        public byte[] getByPath(string fileName)
        {
            try
            {
                Image imageIn = Image.FromFile(getPath() + fileName);
                MemoryStream ms = new MemoryStream();
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                try
                {
                    Program.MessageError("RetrieveArrivedCls", "imageToByteArray", ex, false);
                    Image imageIn = Properties.Resources.no_image;
                    MemoryStream ms = new MemoryStream();
                    imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    return ms.ToArray();
                    throw;
                }
                catch (Exception ex2)
                {
                    Program.MessageError("RetrieveArrivedCls", "imageToByteArray", ex2, false);
                    return null;
                }
            }
        }
        public string getPath()
        {
            return new GetMasterProjectConfigCls().GetConfigFromDB("PathPatientPhotos");
        }

        public byte[] getByUrl(string fileName)
        {
            string url = getUrl() + fileName;
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            try
            {
                using (HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse())
                {
                    using (Stream stream = httpWebReponse.GetResponseStream())
                    {
                        Image img = Image.FromStream(stream);
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
            catch
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
        public string getUrl()
        {
            return new GetMasterProjectConfigCls().GetConfigFromDB("urlPatientPhotos");
        }

        public byte[] getByWebService(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var img = ws.GetPTImageByHN(hn).AsEnumerable().Select(x => x.Field<byte[]>("docData")).FirstOrDefault();
                    if (img != null)
                    {
                        return img;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream();
                        Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                }
            }
            catch
            {
                MemoryStream ms = new MemoryStream();
                Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }
    }
}
