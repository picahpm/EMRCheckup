using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DBCheckup;

namespace BKvs2010.Class
{
    public partial class WaitingListCls
    {
        public WaitingListCls()
        {

        }

        public class WaitingListDtl
        {
            public int index { get; set; }
            public int tpr_id { get; set; }
            public int tps_id { get; set; }
            public string tpr_queue_no { get; set; }
            public string tpt_hn_no { get; set; }
            public string tpt_othername { get; set; }
            public string tps_call_status { get; set; }
            public bool tps_reserve { get; set; }
            public bool holded { get; set; }
            public bool reserve { get; set; }
        }
        public class WaitingListHdr
        {
            public int priority { get; set; }
            public int tpr_id { get; set; }
            public int tps_id { get; set; }
            public int mvt_id { get; set; }
            public string hn_no { get; set; }
            public string queue_no { get; set; }
            public string name { get; set; }
            public bool holded { get; set; }
            public bool reserve { get; set; }
        }

        public List<WaitingListHdr> getWaitingRoomHdr(int? mrm_id)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    mst_room_hdr room_hdr = cdc.mst_room_hdrs
                                               .Where(x => x.mrm_id == mrm_id)
                                               .FirstOrDefault();
                    if (room_hdr != null)
                    {
                        DateTime dateNow = Program.GetServerDateTime();
                        List<trn_patient_queue> list_queue = cdc.trn_patient_queues
                                                                .Where(x => x.mrm_id == mrm_id &&
                                                                            x.trn_patient_regi.tpr_arrive_date.Value.Date == dateNow.Date &&
                                                                            x.tps_status == "NS" &&
                                                                            x.tps_ns_status == "QL").ToList();

                        List<string> siteVip = new List<string> { "01HPC2", "01HPC3" };
                        if (siteVip.Contains(room_hdr.mst_hpc_site.mhs_code))
                        {
                            switch (room_hdr.mrm_code)
                            {
                                case "US":
                                    List<WaitingListHdr> resultUS = list_queue
                                                                    .OrderBy(x => x.trn_patient_regi.tpr_miss_lower == null ? 2
                                                                                  : x.trn_patient_regi.tpr_miss_lower == false ? 2
                                                                                  : 1)
                                                                    .ThenBy(x => x.trn_patient_regi.trn_patient.tpt_vip_hpc == null ? 2
                                                                                  : x.trn_patient_regi.trn_patient.tpt_vip_hpc == false ? 2
                                                                                  : 1)
                                                                    .ThenBy(x => (x.tps_hold_date == null || x.tps_hold_date >= dateNow) ? 1 : 2)
                                                                    .ThenBy(x => x.tps_create_date)
                                                                    .Select((x, inx) => new WaitingListHdr
                                                                    {
                                                                        priority = inx + 1,
                                                                        tpr_id = x.tpr_id,
                                                                        tps_id = x.tps_id,
                                                                        mvt_id = x.mvt_id == null ? 0
                                                                                 : (int)x.mvt_id,
                                                                        hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                                        queue_no = x.trn_patient_regi.tpr_queue_no,
                                                                        name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                                        holded = (x.tps_call_status == null || x.tps_call_status == "") ? false
                                                                                 : x.tps_call_status == "HD" ? true
                                                                                 : false,
                                                                        reserve = x.tps_reserve == null ? false
                                                                                  : x.tps_reserve == false ? false
                                                                                  : true
                                                                    }).ToList();
                                    return resultUS;
                                case "BM":
                                    List<WaitingListHdr> resultBM = list_queue
                                                                 .OrderBy(x => x.tps_bm_seq)
                                                                 .ThenBy(x => (x.tps_hold_date == null || x.tps_hold_date >= dateNow) ? 1 : 2)
                                                                 .ThenBy(x => x.tps_create_date)
                                                                 .Select((x, inx) => new WaitingListHdr
                                                                 {
                                                                     priority = inx + 1,
                                                                     tpr_id = x.tpr_id,
                                                                     tps_id = x.tps_id,
                                                                     mvt_id = x.mvt_id == null ? 0
                                                                              : (int)x.mvt_id,
                                                                     hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                                     queue_no = x.trn_patient_regi.tpr_queue_no,
                                                                     name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                                     holded = (x.tps_call_status == null || x.tps_call_status == "") ? false
                                                                              : x.tps_call_status == "HD" ? true
                                                                              : false,
                                                                     reserve = x.tps_reserve == null ? false
                                                                               : x.tps_reserve == false ? false
                                                                               : true
                                                                 }).ToList();
                                    return resultBM;
                                default:
                                    List<WaitingListHdr> result = list_queue
                                                               .OrderBy(x => x.trn_patient_regi.tpr_miss_lower == null ? 2
                                                                             : x.trn_patient_regi.tpr_miss_lower == false ? 2
                                                                             : 1)
                                                               .ThenBy(x => x.trn_patient_regi.trn_patient.tpt_vip_hpc == null ? 2
                                                                             : x.trn_patient_regi.trn_patient.tpt_vip_hpc == false ? 2
                                                                             : 1)
                                                               .ThenBy(x => (x.tps_hold_date == null || x.tps_hold_date >= dateNow) ? 1 : 2)
                                                               .ThenBy(x => x.tps_create_date)
                                                               .Select((x, inx) => new WaitingListHdr
                                                               {
                                                                   priority = inx + 1,
                                                                   tpr_id = x.tpr_id,
                                                                   tps_id = x.tps_id,
                                                                   mvt_id = x.mvt_id == null ? 0
                                                                            : (int)x.mvt_id,
                                                                   hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                                   queue_no = x.trn_patient_regi.tpr_queue_no,
                                                                   name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                                   holded = (x.tps_call_status == null || x.tps_call_status == "") ? false
                                                                            : x.tps_call_status == "HD" ? true
                                                                            : false,
                                                                   reserve = x.tps_reserve == null ? false
                                                                             : x.tps_reserve == false ? false
                                                                             : true
                                                               }).ToList();
                                    return result;
                            }
                        }
                        else
                        {
                            switch (room_hdr.mrm_code)
                            {
                                case "US":
                                    List<WaitingListHdr> resultUS = list_queue
                                                                 .OrderBy(x => x.trn_patient_regi.trn_patient.tpt_vip_hpc == null ? 2
                                                                               : x.trn_patient_regi.trn_patient.tpt_vip_hpc == false ? 2
                                                                               : 1)
                                                                 .ThenBy(x => (x.tps_hold_date == null || x.tps_hold_date >= dateNow) ? 1 : 2)
                                                                 .ThenBy(x => x.tps_create_date)
                                                                 .Select((x, inx) => new WaitingListHdr
                                                                 {
                                                                     priority = inx + 1,
                                                                     tpr_id = x.tpr_id,
                                                                     tps_id = x.tps_id,
                                                                     mvt_id = x.mvt_id == null ? 0
                                                                              : (int)x.mvt_id,
                                                                     hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                                     queue_no = x.trn_patient_regi.tpr_queue_no,
                                                                     name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                                     holded = (x.tps_call_status == null || x.tps_call_status == "") ? false
                                                                              : x.tps_call_status == "HD" ? true
                                                                              : false,
                                                                     reserve = x.tps_reserve == null ? false
                                                                               : x.tps_reserve == false ? false
                                                                               : true
                                                                 }).ToList();
                                    return resultUS;
                                case "BM":
                                    List<WaitingListHdr> resultBM = list_queue
                                                                 .OrderBy(x => x.trn_patient_regi.trn_patient.tpt_vip_hpc == null ? 2
                                                                               : x.trn_patient_regi.trn_patient.tpt_vip_hpc == false ? 2
                                                                               : 1)
                                                                 .ThenBy(x => x.tps_bm_seq)
                                                                 .ThenBy(x => (x.tps_hold_date == null || x.tps_hold_date >= dateNow) ? 1 : 2)
                                                                 .ThenBy(x => x.tps_create_date)
                                                                 .Select((x, inx) => new WaitingListHdr
                                                                 {
                                                                     priority = inx + 1,
                                                                     tpr_id = x.tpr_id,
                                                                     tps_id = x.tps_id,
                                                                     mvt_id = x.mvt_id == null ? 0
                                                                              : (int)x.mvt_id,
                                                                     hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                                     queue_no = x.trn_patient_regi.tpr_queue_no,
                                                                     name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                                     holded = (x.tps_call_status == null || x.tps_call_status == "") ? false
                                                                              : x.tps_call_status == "HD" ? true
                                                                              : false,
                                                                     reserve = x.tps_reserve == null ? false
                                                                               : x.tps_reserve == false ? false
                                                                               : true
                                                                 }).ToList();
                                    return resultBM;
                                default:
                                    List<WaitingListHdr> result = list_queue
                                                               .OrderBy(x => x.trn_patient_regi.trn_patient.tpt_vip_hpc == null ? 2
                                                                             : x.trn_patient_regi.trn_patient.tpt_vip_hpc == false ? 2
                                                                             : 1)
                                                               .ThenBy(x => (x.tps_hold_date == null || x.tps_hold_date >= dateNow) ? 1 : 2)
                                                               .ThenBy(x => x.tps_create_date)
                                                               .Select((x, inx) => new WaitingListHdr
                                                               {
                                                                   priority = inx + 1,
                                                                   tpr_id = x.tpr_id,
                                                                   tps_id = x.tps_id,
                                                                   mvt_id = x.mvt_id == null ? 0
                                                                            : (int)x.mvt_id,
                                                                   hn_no = x.trn_patient_regi.trn_patient.tpt_hn_no,
                                                                   queue_no = x.trn_patient_regi.tpr_queue_no,
                                                                   name = x.trn_patient_regi.trn_patient.tpt_othername,
                                                                   holded = (x.tps_call_status == null || x.tps_call_status == "") ? false
                                                                            : x.tps_call_status == "HD" ? true
                                                                            : false,
                                                                   reserve = x.tps_reserve == null ? false
                                                                             : x.tps_reserve == false ? false
                                                                             : true
                                                               }).ToList();
                                    return result;
                            }

                        }
                    }
                    else
                    {
                        return new List<WaitingListHdr>();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("WaitingListCls", "getWaitingRoomHdr", ex, false);
                return new List<WaitingListHdr>();
            }
        }
        public List<WaitingListDtl> getWaitingRoomDtl(int? mrd_id, int? mut_id)
        {
            if (mrd_id == null)
            {
                return new List<WaitingListDtl>();
            }
            else
            {
                try
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        mst_room_dtl room_dtl = cdc.mst_room_dtls.Where(x => x.mrd_id == mrd_id).FirstOrDefault();
                        if (room_dtl == null)
                        {
                            return new List<WaitingListDtl>();
                        }
                        else
                        {
                            List<sp_get_waiting_room_hdrResult> waitingList = cdc.sp_get_waiting_room_hdr(room_dtl.mrm_id).ToList();
                            switch (room_dtl.mst_room_hdr.mrm_code)
                            {
                                case "EM":
                                case "TE":
                                    string mvt_code = room_dtl.mst_room_hdr.mrm_code == "EM" ? (room_dtl.mrd_type == 'D' ? "EM" : "EN")
                                                      : (room_dtl.mrd_type == 'T' ? "TX" : (room_dtl.mrd_type == 'D' ? "TE" : ""));
                                    int mvt_id = cdc.mst_events.Where(x => x.mvt_code == mvt_code).Select(x => x.mvt_id).FirstOrDefault();
                                    if (mvt_id == 0)
                                    {
                                        return new List<WaitingListDtl>();
                                    }
                                    else
                                    {
                                        return waitingList.Where(x => x.mvt_id == mvt_id)
                                                          .OrderBy(x => x.priority)
                                                          .Select((x, inx) => new WaitingListDtl
                                                          {
                                                              index = inx + 1,
                                                              tpr_id = (int)x.tpr_id,
                                                              tps_id = (int)x.tps_id,
                                                              tpt_hn_no = x.tpt_hn_no,
                                                              tpr_queue_no = x.tpr_queue_no,
                                                              tpt_othername = x.tpt_othername,
                                                              holded = (bool)x.holded,
                                                              reserve = (bool)x.reserved
                                                          }).ToList();
                                    }
                                case "SC":
                                    string typeScreening = room_dtl.mrd_screening_type;
                                    if (typeScreening == "GA" || typeScreening == null)
                                    {
                                        return waitingList.OrderBy(x => x.priority)
                                                          .Select((x, inx) => new WaitingListDtl
                                                          {
                                                              index = inx + 1,
                                                              tpr_id = (int)x.tpr_id,
                                                              tps_id = (int)x.tps_id,
                                                              tpt_hn_no = x.tpt_hn_no,
                                                              tpr_queue_no = x.tpr_queue_no,
                                                              tpt_othername = x.tpt_othername,
                                                              holded = (bool)x.holded,
                                                              reserve = (bool)x.reserved
                                                          }).ToList();
                                    }
                                    else
                                    {
                                        List<int?> list_tpr_id = waitingList.Select(x => x.tpr_id).ToList();
                                        if (list_tpr_id.Count() == 0)
                                        {
                                            return new List<WaitingListDtl>();
                                        }
                                        else
                                        {
                                            var resultSC = waitingList.Join(cdc.trn_patient_regis
                                                                               .Where(x => list_tpr_id.Contains((x.tpr_id))),
                                                           x => x.tpr_id,
                                                           y => y.tpr_id,
                                                           (x, y) => new
                                                           {
                                                               list_patient_cate = y.trn_patient_cats.Select(z => new
                                                                                   {
                                                                                       z.mst_doc_category.mdc_code,
                                                                                       z.mst_doc_category.mdc_pre_insure
                                                                                   }).ToList(),
                                                               list_aviation_cate = y.trn_patient_aviations.Select(z => new
                                                                                    {
                                                                                        z.mst_avia_cat_type.mst_aviation_category.mac_code
                                                                                    }).ToList(),
                                                               QueueNo = y.tpr_queue_no,
                                                               HN = x.tpt_hn_no,
                                                               FullName = x.tpt_othername,
                                                               tpr_id = x.tpr_id,
                                                               tps_id = x.tps_id,
                                                               index = x.priority,
                                                               holded = x.holded,
                                                               reserve = x.reserved
                                                           }).OrderBy(x => x.index)
                                                           .ToList();
                                            switch (typeScreening)
                                            {
                                                case "S1":
                                                    {
                                                        List<string> donotAviationCode = new List<string> { "FA01", "CN01", "AS01" };
                                                        return resultSC
                                                               .Where(x => !x.list_patient_cate.Any(y => y.mdc_pre_insure == true) &&
                                                                           !x.list_aviation_cate.Any(y => donotAviationCode.Contains(y.mac_code)))
                                                               .OrderBy(x => x.index)
                                                               .Select((x, inx) => new WaitingListDtl
                                                               {
                                                                   index = inx + 1,
                                                                   tpr_id = (int)x.tpr_id,
                                                                   tps_id = (int)x.tps_id,
                                                                   tpt_hn_no = x.HN,
                                                                   tpr_queue_no = x.QueueNo,
                                                                   tpt_othername = x.FullName,
                                                                   holded = (bool)x.holded,
                                                                   reserve = (bool)x.reserve
                                                               }).ToList();
                                                    }
                                                case "S2":
                                                    {
                                                        List<string> donotAviationCode = new List<string> { "FA01", "CN01", "AS01" };
                                                        return resultSC
                                                               .Where(x => !x.list_aviation_cate.Any(y => donotAviationCode.Contains(y.mac_code)))
                                                               .OrderBy(x => x.index)
                                                               .Select((x, inx) => new WaitingListDtl
                                                               {
                                                                   index = inx + 1,
                                                                   tpr_id = (int)x.tpr_id,
                                                                   tps_id = (int)x.tps_id,
                                                                   tpt_hn_no = x.HN,
                                                                   tpr_queue_no = x.QueueNo,
                                                                   tpt_othername = x.FullName,
                                                                   holded = (bool)x.holded,
                                                                   reserve = (bool)x.reserve
                                                               }).ToList();
                                                    }
                                                case "S3":
                                                    {
                                                        List<string> doAviationCode = new List<string> { "FA01", "CN01", "AS01" };
                                                        return resultSC
                                                               .Where(x => x.list_patient_cate.Where(y => y.mdc_pre_insure == true).Count() == 0 &&
                                                                           x.list_aviation_cate.Any(y => doAviationCode.Contains(y.mac_code)))
                                                               .OrderBy(x => x.index)
                                                               .Select((x, inx) => new WaitingListDtl
                                                               {
                                                                   index = inx + 1,
                                                                   tpr_id = (int)x.tpr_id,
                                                                   tps_id = (int)x.tps_id,
                                                                   tpt_hn_no = x.HN,
                                                                   tpr_queue_no = x.QueueNo,
                                                                   tpt_othername = x.FullName,
                                                                   holded = (bool)x.holded,
                                                                   reserve = (bool)x.reserve
                                                               }).ToList();
                                                    }
                                                default:
                                                    {
                                                        return resultSC
                                                               .OrderBy(x => x.index)
                                                               .Select((x, inx) => new WaitingListDtl
                                                               {
                                                                   index = inx + 1,
                                                                   tpr_id = (int)x.tpr_id,
                                                                   tps_id = (int)x.tps_id,
                                                                   tpt_hn_no = x.HN,
                                                                   tpr_queue_no = x.QueueNo,
                                                                   tpt_othername = x.FullName,
                                                                   holded = (bool)x.holded,
                                                                   reserve = (bool)x.reserve
                                                               }).ToList();
                                                    }
                                            }
                                        }
                                    }
                                case "DC":
                                    mst_user_type user = cdc.mst_user_types.Where(x => x.mut_id == mut_id).FirstOrDefault();
                                    if (user == null)
                                    {
                                        return new List<WaitingListDtl>();
                                    }
                                    else
                                    {
                                        List<int> listCate = user.mst_user_cats.Select(x => x.mdc_id).ToList();
                                        List<int?> list_tpr_id = waitingList.Select(x => x.tpr_id).ToList();
                                        if (list_tpr_id.Count() == 0)
                                        {
                                            return new List<WaitingListDtl>();
                                        }
                                        else
                                        {
                                            if (room_dtl.mst_room_hdr.mst_hpc_site.mhs_use_quota == true)
                                            {
                                                var result = (from wl in waitingList
                                                              join pr in cdc.trn_patient_regis.Where(x => list_tpr_id.Contains(x.tpr_id))
                                                              on wl.tpr_id equals pr.tpr_id
                                                              where ((((pr.tpr_req_inorout_doctor != null ? pr.tpr_req_inorout_doctor.Trim() : "") == "") || pr.tpr_req_inorout_doctor == "IN") &&
                                                                    ((pr.tpr_req_doc_code == null ? "" : pr.tpr_req_doc_code.Trim()) != ""
                                                                      ? pr.tpr_req_doc_code.Trim() == user.mut_username.Trim()
                                                                      : (pr.tpr_pe_doc_code == null ? "" : pr.tpr_pe_doc_code.Trim()) == ""
                                                                      ? false
                                                                      : (pr.tpr_pe_doc_code == null ? "" : pr.tpr_pe_doc_code.Trim()) == user.mut_username.Trim() &&
                                                                        ((pr.tpr_req_doc_gender == null || pr.tpr_req_doc_gender == ' ') ? true : pr.tpr_req_doc_gender == user.mut_gender) &&
                                                                        (pr.trn_patient_cats.Count() == 0 ? true : pr.trn_patient_cats.Any(y => listCate.Contains(y.mdc_id)))))
                                                              select wl)
                                                              .OrderBy(x => x.priority)
                                                              .ToList()
                                                              .Select((x, inx) => new WaitingListDtl
                                                              {
                                                                  index = inx + 1,
                                                                  holded = (bool)x.holded,
                                                                  tpr_id = (int)x.tpr_id,
                                                                  tps_id = (int)x.tps_id,
                                                                  reserve = (bool)x.reserved,
                                                                  tpr_queue_no = x.tpr_queue_no,
                                                                  tpt_hn_no = x.tpt_hn_no,
                                                                  tpt_othername = x.tpt_othername
                                                              }).ToList();
                                                return result;
                                            }
                                            else
                                            {
                                                var result = (from wl in waitingList
                                                              join pr in cdc.trn_patient_regis.Where(x => list_tpr_id.Contains(x.tpr_id))
                                                              on wl.tpr_id equals pr.tpr_id
                                                              where ((((pr.tpr_req_inorout_doctor != null ? pr.tpr_req_inorout_doctor.Trim() : "") == "") || pr.tpr_req_inorout_doctor == "IN") &&
                                                                    ((pr.tpr_req_doc_code == null ? "" : pr.tpr_req_doc_code.Trim()) != ""
                                                                      ? pr.tpr_req_doc_code.Trim() == user.mut_username.Trim()
                                                                      : (pr.tpr_pe_doc_code == null ? "" : pr.tpr_pe_doc_code.Trim()) != "" 
                                                                      ? ((pr.tpr_pe_doc_code == null ? "" : pr.tpr_pe_doc_code.Trim()) == user.mut_username.Trim() &&
                                                                         ((pr.tpr_req_doc_gender == null || pr.tpr_req_doc_gender == ' ') ? true : pr.tpr_req_doc_gender == user.mut_gender) &&
                                                                         (pr.trn_patient_cats.Count() == 0 ? true : pr.trn_patient_cats.All(y => listCate.Contains(y.mdc_id))))
                                                                      : ((pr.tpr_req_doc_gender == null || pr.tpr_req_doc_gender == ' ') ? true : pr.tpr_req_doc_gender == user.mut_gender) &&
                                                                        (pr.trn_patient_cats.Count() == 0 ? true : pr.trn_patient_cats.All(y => listCate.Contains(y.mdc_id)))))
                                                              select wl)
                                                              .OrderBy(x => x.priority)
                                                              .ToList()
                                                              .Select((x, inx) => new WaitingListDtl
                                                              {
                                                                  index = inx + 1,
                                                                  holded = (bool)x.holded,
                                                                  tpr_id = (int)x.tpr_id,
                                                                  tps_id = (int)x.tps_id,
                                                                  reserve = (bool)x.reserved,
                                                                  tpr_queue_no = x.tpr_queue_no,
                                                                  tpt_hn_no = x.tpt_hn_no,
                                                                  tpt_othername = x.tpt_othername
                                                              }).ToList();
                                                return result;
                                            }


                                            //var result = waitingList.Join(cdc.trn_patient_regis
                                            //                                 .Where(x => waitingList.Select(y => y.tpr_id).Contains((x.tpr_id)) &&
                                            //                                             (x.tpr_req_inorout_doctor == null || x.tpr_req_inorout_doctor == "" || x.tpr_req_inorout_doctor == "IN") &&
                                            //                                             (
                                            //                                                 (x.tpr_req_doc_code != "" || x.tpr_req_doc_code != null) ? x.tpr_req_doc_code == user.mut_username :
                                            //                                                 (
                                            //                                                     ((x.tpr_req_doc_gender != ' ' || x.tpr_req_doc_gender != null) ? x.tpr_req_doc_gender == user.mut_gender : true) &&
                                            //                                                     (x.trn_patient_cats.Count() != 0 ? x.trn_patient_cats.Any(y => listCate.Contains(y.mdc_id)) : true)
                                            //                                                 )
                                            //                                             )
                                            //                                       ),
                                            //             x => x.tpr_id,
                                            //             y => y.tpr_id,
                                            //             (x, y) => new
                                            //             {
                                            //                 ReqDoc = y.tpr_req_doctor,
                                            //                 ReqGender = (y.tpr_req_doc_gender == null) ? 'X' : y.tpr_req_doc_gender,
                                            //                 DocCode = y.tpr_req_doc_code,
                                            //                 PEDoc = (y.tpr_pe_doc == null) ? 'N' : y.tpr_pe_doc,
                                            //                 PEDocCode = y.tpr_pe_doc_code,
                                            //                 OutDocCode = (y.tpr_req_inorout_doctor == null || y.tpr_req_inorout_doctor == "") ? false
                                            //                              : y.tpr_req_inorout_doctor == "UT" ? true : false,
                                            //                 QueueNo = y.tpr_queue_no,
                                            //                 HN = x.tpt_hn_no,
                                            //                 FullName = x.tpt_othername,
                                            //                 tprid = x.tpr_id,
                                            //                 tps_id = x.tps_id,
                                            //                 countmdc = y.trn_patient_cats.Count(),
                                            //                 DocCat = y.trn_patient_cats.Where(z => z.mst_doc_category.mdc_code == "MD014").Select(z => z.mdc_id).Distinct().Count(),
                                            //                 mdc_flag = listCate.Where(z => y.trn_patient_cats.Select(cate => cate.mdc_id).Contains(z) &&
                                            //                                                (y.tpr_req_doc_gender == null ? true : (y.tpr_req_doc_gender == user.mut_gender))).Count(),
                                            //                 type_PE = (cdc.mst_events.Where(z => z.mvt_id == x.mvt_id).Select(z => z.mvt_code).FirstOrDefault() == "PE") ? "Y" : "N",
                                            //                 index = x.priority,
                                            //                 holded = x.holded,
                                            //                 reserve = x.reserved
                                            //             }).OrderBy(x => x.index)
                                            //             .ToList();

                                            //char datagender = (user.mut_gender == null) ? 'Z' : Convert.ToChar(user.mut_gender);

                                            //if (room_dtl.mst_room_hdr.mst_hpc_site.mhs_use_quota == true)
                                            //{
                                            //    return result.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == user.mut_username) ||
                                            //                             ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == user.mut_username) ||
                                            //                             ((x.PEDoc == 'N' || x.PEDoc == null) && x.OutDocCode == false && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
                                            //                             (x.OutDocCode == true && x.type_PE == "Y") ||
                                            //                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == user.mut_username) ||
                                            //                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)))
                                            //                 .OrderBy(x => x.index)
                                            //                 .Select((x, inx) => new WaitingListDtl
                                            //                 {
                                            //                     index = inx + 1,
                                            //                     holded = (bool)x.holded,
                                            //                     tpr_id = (int)x.tprid,
                                            //                     tps_id = (int)x.tps_id,
                                            //                     reserve = (bool)x.reserve,
                                            //                     tpr_queue_no = x.QueueNo,
                                            //                     tpt_hn_no = x.HN,
                                            //                     tpt_othername = x.FullName
                                            //                 }).ToList();
                                            //}
                                            //else if (room_dtl.mst_room_hdr.mst_hpc_site.mhs_code == "01HPC2")
                                            //{
                                            //    return result.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == user.mut_username) ||
                                            //                             (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == user.mut_username) ||
                                            //                             (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
                                            //                             (x.OutDocCode == false && x.PEDoc == 'N' && x.DocCode == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
                                            //                             (x.OutDocCode == true && x.type_PE == "Y") ||
                                            //                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == user.mut_username) ||
                                            //                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)))
                                            //                 .OrderBy(x => x.index)
                                            //                 .Select((x, inx) => new WaitingListDtl
                                            //                 {
                                            //                     index = inx + 1,
                                            //                     holded = (bool)x.holded,
                                            //                     tpr_id = (int)x.tprid,
                                            //                     tps_id = (int)x.tps_id,
                                            //                     reserve = (bool)x.reserve,
                                            //                     tpr_queue_no = x.QueueNo,
                                            //                     tpt_hn_no = x.HN,
                                            //                     tpt_othername = x.FullName
                                            //                 }).ToList();
                                            //}
                                            //else
                                            //{
                                            //    return result.Where(x => ((x.OutDocCode == false && x.PEDoc == 'Y' && x.PEDocCode == Program.CurrentUser.mut_username) ||
                                            //                             (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
                                            //                             (x.OutDocCode == false && x.PEDoc == 'N' && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender) ||
                                            //                             (x.OutDocCode == false && x.PEDoc == 'N' && x.DocCode == null && ((x.countmdc > 0 && x.mdc_flag > 0) || (x.countmdc == 0))) ||
                                            //                             (x.OutDocCode == true && x.type_PE == "Y") ||
                                            //                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == Program.CurrentUser.mut_username) ||
                                            //                             (x.OutDocCode == true && x.type_PE == "N" && x.ReqDoc == 'Y' && x.DocCode == null && x.ReqGender == datagender)))
                                            //                 .OrderBy(x => x.index)
                                            //                 .Select((x, inx) => new WaitingListDtl
                                            //                 {
                                            //                     index = inx + 1,
                                            //                     holded = (bool)x.holded,
                                            //                     tpr_id = (int)x.tprid,
                                            //                     tps_id = (int)x.tps_id,
                                            //                     reserve = (bool)x.reserve,
                                            //                     tpr_queue_no = x.QueueNo,
                                            //                     tpt_hn_no = x.HN,
                                            //                     tpt_othername = x.FullName
                                            //                 }).ToList();
                                            //}
                                        }
                                    }
                                default:
                                    return waitingList.OrderBy(x => x.priority)
                                                      .Select((x, inx) => new WaitingListDtl
                                                      {
                                                          index = inx + 1,
                                                          tpr_id = (int)x.tpr_id,
                                                          tps_id = (int)x.tps_id,
                                                          tpt_hn_no = x.tpt_hn_no,
                                                          tpr_queue_no = x.tpr_queue_no,
                                                          tpt_othername = x.tpt_othername,
                                                          holded = (bool)x.holded,
                                                          reserve = (bool)x.reserved
                                                      }).ToList();
                            }
                            
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("WaitingListCls", "getWaitingRoomDtl", ex, false);
                    return new List<WaitingListDtl>();
                }
            }
        }
    }
}
