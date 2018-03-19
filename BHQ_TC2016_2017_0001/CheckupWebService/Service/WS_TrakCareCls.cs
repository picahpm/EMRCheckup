using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CheckupWebService.Service
{
    public partial class WS_TrakcareCls : WS_Trakcare.WS_GetDataBytrakSoapClient
    {
        public WS_TrakcareCls()
        {
            try
            {
                base.Endpoint.Address = new System.ServiceModel.EndpointAddress(Class.GetDBConfigCls.GetConfig("WSTrakcareUrl"));
                //base.Endpoint.Address = new System.ServiceModel.EndpointAddress("http://10.88.10.77/wsbhq2016/WS_GetDataBytrak.asmx");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public WS_TrakcareCls(string webservice_url)
        {
            try
            {
                base.Endpoint.Address = new System.ServiceModel.EndpointAddress(webservice_url);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
