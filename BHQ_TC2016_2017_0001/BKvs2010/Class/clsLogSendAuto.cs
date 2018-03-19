using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.Class
{
    class clsLogSendAuto
    {
        public List<vw_patient_room> getLogSendAuto(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int? mvt_id;
                    int? mrm_id;
                    int? mze_id;
                    int? current_site = Program.CurrentSite.mhs_id;
                    string queue_no = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).Select(x => x.tpr_queue_no).FirstOrDefault();

                    trn_patient_queue tps = cdc.trn_patient_queues
                                               .Where(x => x.tpr_id == tpr_id &&
                                                           ((x.tps_status == "NS" && x.tps_ns_status == "QL") ||
                                                             (x.tps_status == "WK")))
                                               .FirstOrDefault();
                    mvt_id = tps.mvt_id;
                    mrm_id = tps.mrm_id;
                    mze_id = cdc.mst_room_hdrs.Where(x => x.mrm_id == mrm_id).Select(x => x.mze_id).FirstOrDefault();
                    List<vw_patient_room> list_vw = cdc.vw_patient_rooms
                                                       .Where(x => x.tpr_id == tpr_id &&
                                                                   x.mvt_id != mvt_id &&
                                                                   x.mrm_id != mrm_id &&
                                                                   x.mhs_id == current_site)
                                                       .OrderBy(x => x.send_type == true ? 0 : 1)
                                                       .ThenBy(x => x.skip_seq)
                                                       .ThenBy(x => x.login_flag == "Y" ? 0 : 1)
                                                       .ThenBy(x => x.mze_id == mze_id && x.waiting_time <= 10 ? 0 : 1)
                                                       .ThenBy(x => x.mze_id != mze_id && x.waiting_time <= 10 ? 0 : 1)
                                                       .ThenBy(x => x.mze_id == mze_id ? 0 : 1)
                                                       .ThenBy(x => x.waiting_time)
                                                       .ThenBy(x => x.mze_id)
                                                       .ThenBy(x => x.mrm_seq_room).ToList();

                                                       //.OrderBy(x => x.login_flag == "Y" ? 0 : 1)
                                                       //.ThenBy(x => x.send_type == true ? 0 : 1)
                                                       //.ThenBy(x => x.mze_id == mze_id && x.waiting_time <= 10 ? 0 : 1)
                                                       //.ThenBy(x => x.skip_seq)
                                                       //.ThenBy(x => x.waiting_time)
                                                       //.ThenBy(x => x.mze_id)
                                                       //.ThenBy(x => x.mrm_seq_room).ToList();

                    return list_vw;
                }
            }
            catch
            {
                return null;
            }
        }

        public int? get_tps_id(int tpr_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_queue tps = cdc.trn_patient_queues
                                                .Where(x => x.tpr_id == tpr_id &&
                                                            ((x.tps_status == "NS" && x.tps_ns_status == "QL") ||
                                                                (x.tps_status == "WK")))
                                                .FirstOrDefault();
                    return tps.tps_id;
                }
            }
            catch
            {

            }
            return null;
        }
    }
}
