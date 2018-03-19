using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CheckupWebService.APITrakcare
{
    public class GetPatientImageCls
    {
        public byte[] ByGetPTImageByHN(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var img = ws.GetPTImageByHN(hn).AsEnumerable().Select(x => x.Field<byte[]>("docData")).FirstOrDefault();
                    if (img == null)
                    {
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        Properties.Resources.no_image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        return ms.ToArray();
                    }
                    return img;
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetPatientImageCls", "ByGetPTImageByHN", ex.Message);
                throw ex;
            }
        }
    }
}