using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.Service
{
    public class WS_CheckupCls : WS_PathWay.WS_GetDataFromPathWaySoapClient
    {
        public WS_CheckupCls()
        {
            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(PrePareData.StaticDataCls.WSPathWay);
        }

        public WS_CheckupCls(string webservice_url)
        {
            base.Endpoint.Address = new System.ServiceModel.EndpointAddress(webservice_url);
        }
    }
}
