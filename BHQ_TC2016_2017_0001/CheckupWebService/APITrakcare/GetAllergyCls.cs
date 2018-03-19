using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CheckupWebService.APITrakcare
{
    public class GetAllergyCls
    {
        public string ByGetAllergyByHN(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var result = ws.GetAllergyByHN(hn).AsEnumerable();
                    var strs = result.Select(x => x.Field<string>("AlgEng_Desc")).Where(x => x != null && x.Trim() != "").ToList();
                    return string.Join(Environment.NewLine, strs);
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("GetAllergyCls", "ByGetAllergyByHN", ex.Message);
                throw ex;
            }
        }
    }
}