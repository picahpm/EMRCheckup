using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.LabClass
{
    public class MappingLocationCls
    {
        public List<MappingLocationResult> Mapping()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    List<MappingLocationResult> results = cdc.mst_hpc_sites.Where(x => x.mhs_other_clinic == false)
                                                             .Select(x => new MappingLocationResult
                                                             {
                                                                 mainid = x.mhs_id,
                                                                 maincode = x.mhs_code,
                                                                 maindesc = x.mhs_ename,
                                                                 subcode = x.mhs_code
                                                             }).ToList();

                    var hpc2 = results.Where(x => x.subcode == "01HPC2").FirstOrDefault();
                    if (hpc2 != null)
                    {
                        results.Add(new MappingLocationResult { mainid = hpc2.mainid, maincode = hpc2.maincode, maindesc = hpc2.maindesc, subcode = "01HPMC" });
                        results.Add(new MappingLocationResult { mainid = hpc2.mainid, maincode = hpc2.maincode, maindesc = hpc2.maindesc, subcode = "01MBM" });
                        results.Add(new MappingLocationResult { mainid = hpc2.mainid, maincode = hpc2.maincode, maindesc = hpc2.maindesc, subcode = "01MXU" });
                    }
                    var ams = results.Where(x => x.subcode == "01AMS").FirstOrDefault();
                    if (ams != null)
                    {
                        results.Add(new MappingLocationResult { mainid = ams.mainid, maincode = ams.maincode, maindesc = ams.maindesc, subcode = "01AMSCHK" });
                        results.Add(new MappingLocationResult { mainid = ams.mainid, maincode = ams.maincode, maindesc = ams.maindesc, subcode = "01AMS2" });
                    }
                    return results;
                }
            }
            catch (Exception ex)
            {
                Class.globalCls.MessageError("MappingLocationCls", "Mapping", ex.Message);
                throw ex;
            }
        }
    }

    public class MappingLocationResult
    {
        public int mainid { get; set; }
        public string maincode { get; set; }
        public string maindesc { get; set; }
        public string subcode { get; set; }
    }
}