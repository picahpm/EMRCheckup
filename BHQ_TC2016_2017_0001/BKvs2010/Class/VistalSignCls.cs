using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKvs2010.Class
{
    public partial class VistalSignCls
    {
        public void RetrieveVistalSignBackground(int tpr_id)
        {
            try
            {
                using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                {
                    ws.retrieveVitalSignBackground(tpr_id);
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ClsBasicMeasurement", "ws.retrieveVitalSignBackground", ex, false);
            }
        }
    }
}
