using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.Service
{
    public class WS_TrakcareCls : WS_Trakcare.WS_GetDataBytrakSoapClient
    {
        public WS_TrakcareCls()
        {
            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(PrePareData.StaticDataCls.WSTrakcareUrl);
        }

        public WS_TrakcareCls(string webservice_url)
        {
            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(webservice_url);
        }
    }
}
