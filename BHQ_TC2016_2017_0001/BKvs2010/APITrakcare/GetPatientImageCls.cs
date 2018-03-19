using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Imaging;

namespace BKvs2010.APITrakcare
{
    class GetPatientImageCls
    {
        public byte[] GetImageByWS(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var img = ws.GetPTImageByHN(hn).AsEnumerable().Select(x => x.Field<byte[]>("docData")).FirstOrDefault();
                    if (img == null)
                    {
                        return GetDefaultImage();
                    }
                    else
                    {
                        return img;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("GetPatientImageCls", "GetImageByWS(string hn)", ex, false);
                return GetDefaultImage();
            }
        }

        public byte[] GetDefaultImage()
        {
            MemoryStream ms = new MemoryStream();
            Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            return ms.ToArray();
        }
    }
}
