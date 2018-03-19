using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace CheckupBO.Service
{
    public class WS_TrakcareCls : WS_Trakcare.WS_GetDataBytrakSoapClient
    {
        public WS_TrakcareCls()
        {
            try
            {
                base.Endpoint.Address = new System.ServiceModel.EndpointAddress(Class.GetDBConfigCls.GetConfig("WSTrakcareUrl"));
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
