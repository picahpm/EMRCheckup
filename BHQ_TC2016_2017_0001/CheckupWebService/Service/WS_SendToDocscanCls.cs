using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckupWebService.Service
{
    public class WS_SendToDocscanCls : WS_SendToDocscan.ServiceSoapClient
    {
        public WS_SendToDocscanCls()
        {
            try
            {
                base.Endpoint.Address = new System.ServiceModel.EndpointAddress(Class.GetDBConfigCls.GetConfig("WSDocScanUrl"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public WS_SendToDocscanCls(string webservice_url)
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