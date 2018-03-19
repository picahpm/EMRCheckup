using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.Service
{
    public class WS_SendToDocscanCls : WS_SendToDocscan.ServiceSoapClient
    {
        public WS_SendToDocscanCls()
        {
            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(PrePareData.StaticDataCls.WSDocScanUrl);
        }

        public WS_SendToDocscanCls(string webservice_url)
        {
            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(webservice_url);
        }
    }
}
