using System;
using System.Data;
using System.Linq;

namespace BKvs2010.APITrakcare
{
    public class GetAllergyCls
    {
        public string GetByHN(string hn)
        {
            try
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    DataTable dt = ws.GetAllergyByHN(hn);

                    string StrAllery = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (StrAllery == "")
                        {
                            StrAllery = dr["AlgEng_Desc"].ToString();
                        }
                        else
                        {
                            StrAllery += Environment.NewLine + dr["AlgEng_Desc"].ToString();
                        }
                    }
                    if (StrAllery == "")
                    {
                        StrAllery = "No Know Allergy";
                    }
                    return StrAllery;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("FunctionDataCls", "GetAllergyByHN(String HN)", ex, false);
                return "";
            }
        }
    }
}
