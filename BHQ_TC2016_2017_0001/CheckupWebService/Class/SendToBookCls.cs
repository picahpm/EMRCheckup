using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DBCheckup;

namespace CheckupWebService.Class
{
    public partial class SendToBookCls
    {
        public enum StatusSendBook
        {
            SendBook,
            NotSendBook,
            Error
        }
        public StatusSendBook SendToBook(int tpr_id, string endPointAddress)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    trn_patient_regi patientRegis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    DateTime dateNow = globalCls.GetServerDateTime();
                    try
                    {
                        int enRowID = Convert.ToInt32(patientRegis.tpr_en_rowid);
                        GetPTPackageCls PackageCls = new GetPTPackageCls();
                        EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                        PackageCls.AddPatientOrderItem(ref patientRegis, "SysPend", dateNow, getPTPackage);
                        PackageCls.AddPatientOrderSet(ref patientRegis, "SysPend", dateNow, getPTPackage);
                        List<GetPTPackageCls.MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                        PackageCls.AddPatientEvent(ref patientRegis, "SysPend", dateNow, mapOrder);
                        PackageCls.AddPatientPlan(ref patientRegis, "SysPend", dateNow);
                        PackageCls.skipReqDoctorOutDepartment(ref patientRegis);
                        PackageCls.CompleteEcho(ref patientRegis);
                        PackageCls.skipChangeEstToEcho(ref patientRegis, patientRegis.mhs_id);
                        PackageCls.checkOrderPMR(ref patientRegis, patientRegis.mhs_id);
                        cdc.SubmitChanges();

                        if (patientRegis.trn_patient_plans.Any(x => x.tpl_status != 'P' && x.tpl_status != 'L'))
                        {
                            return StatusSendBook.NotSendBook;
                        }
                        else
                        {
                            List<string> site1 = new List<string> { "01CHK" };
                            List<string> site2 = new List<string> { "01HPC2", "01HPC3" };
                            List<string> siteOutResult = new List<string> { "01JMSCK", "01IMS", "01AMS", "01BLC" };
                            string patientSite = patientRegis.mst_hpc_site.mhs_code;
                            if (site1.Contains(patientSite))
                            {
                                int mvt_result = cdc.mst_events.Where(x => x.mvt_code == "DC").Select(x => x.mvt_id).FirstOrDefault();
                                if (patientRegis.trn_patient_queues.Any(x => x.mvt_id == mvt_result && x.tps_status == "ED"))
                                {
                                    // update all trn_patient_queue = 'ED' 
                                    EndQueueCheckC(ref patientRegis, dateNow);
                                    SendQueueBook(ref patientRegis, dateNow);
                                    cdc.SubmitChanges();
                                    return StatusSendBook.SendBook;
                                }
                                else
                                {
                                    return StatusSendBook.NotSendBook;
                                }
                            }
                            else if (siteOutResult.Contains(patientSite))
                            {
                                EndQueueCheckC(ref patientRegis, dateNow);
                                SendQueueBook(ref patientRegis, dateNow);
                                cdc.SubmitChanges();
                                return StatusSendBook.SendBook;
                            }
                            else if (site2.Contains(patientSite))
                            {
                                char? peType = patientRegis.tpr_pe_site2;
                                switch (peType)
                                {
                                    case 'N':
                                        EndQueueCheckC(ref patientRegis, dateNow);
                                        SendQueueBook(ref patientRegis, dateNow);
                                        cdc.SubmitChanges();
                                        return StatusSendBook.SendBook;
                                    default:
                                        int mvt_result = cdc.mst_events.Where(x => x.mvt_code == "DC").Select(x => x.mvt_id).FirstOrDefault();
                                        if (patientRegis.trn_patient_queues.Any(x => x.mvt_id == mvt_result && x.tps_status == "ED"))
                                        {
                                            EndQueueCheckC(ref patientRegis, dateNow);
                                            SendQueueBook(ref patientRegis, dateNow);
                                            cdc.SubmitChanges();
                                            return StatusSendBook.SendBook;
                                        }
                                        else
                                        {
                                            return StatusSendBook.NotSendBook;
                                        }
                                }
                            }
                            else
                            {
                                return StatusSendBook.NotSendBook;
                            }
                        }

                        //WebService.RetrievePackageCls.StatusRetrievePackage status = new WebService.RetrievePackageCls().GetPatientPackage(ref patientRegis, dateNow, endPointAddress);
                        //if (status == WebService.RetrievePackageCls.StatusRetrievePackage.Error)
                        //{
                        //    return StatusSendBook.Error;
                        //}
                        //else
                        //{
                        //    WebService.RetrievePackageCls.StatusRetrievePackage genStatus = new WebService.RetrievePackageCls().genPatientPlan(ref patientRegis, patientRegis.mhs_id, dateNow);
                        //    cdc.SubmitChanges();
                        //    if (patientRegis.trn_patient_plans.Any(x => x.tpl_status != 'P' && x.tpl_status != 'L'))
                        //    {
                        //        return StatusSendBook.NotSendBook;
                        //    }
                        //    else
                        //    {
                        //        List<string> site1 = new List<string> { "01CHK" };
                        //        List<string> site2 = new List<string> { "01HPC2", "01HPC3" };
                        //        List<string> siteOutResult = new List<string> { "01JMSCK", "01IMS", "01AMS", "01BLC" };
                        //        string patientSite = patientRegis.mst_hpc_site.mhs_code;
                        //        if (site1.Contains(patientSite))
                        //        {
                        //            int mvt_result = cdc.mst_events.Where(x => x.mvt_code == "DC").Select(x => x.mvt_id).FirstOrDefault();
                        //            if (patientRegis.trn_patient_queues.Any(x => x.mvt_id == mvt_result && x.tps_status == "ED"))
                        //            {
                        //                EndQueueCheckC(ref patientRegis, dateNow);
                        //                SendQueueBook(ref patientRegis, dateNow);
                        //                cdc.SubmitChanges();
                        //                return StatusSendBook.SendBook;
                        //            }
                        //            else
                        //            {
                        //                return StatusSendBook.NotSendBook;
                        //            }
                        //        }
                        //        else if (siteOutResult.Contains(patientSite))
                        //        {
                        //            EndQueueCheckC(ref patientRegis, dateNow);
                        //            SendQueueBook(ref patientRegis, dateNow);
                        //            cdc.SubmitChanges();
                        //            return StatusSendBook.SendBook;
                        //        }
                        //        else if (site2.Contains(patientSite))
                        //        {
                        //            char? peType = patientRegis.tpr_pe_site2;
                        //            switch (peType)
                        //            {
                        //                case 'N':
                        //                    EndQueueCheckC(ref patientRegis, dateNow);
                        //                    SendQueueBook(ref patientRegis, dateNow);
                        //                    cdc.SubmitChanges();
                        //                    return StatusSendBook.SendBook;
                        //                default:
                        //                    int mvt_result = cdc.mst_events.Where(x => x.mvt_code == "DC").Select(x => x.mvt_id).FirstOrDefault();
                        //                    if (patientRegis.trn_patient_queues.Any(x => x.mvt_id == mvt_result && x.tps_status == "ED"))
                        //                    {
                        //                        EndQueueCheckC(ref patientRegis, dateNow);
                        //                        SendQueueBook(ref patientRegis, dateNow);
                        //                        cdc.SubmitChanges();
                        //                        return StatusSendBook.SendBook;
                        //                    }
                        //                    else
                        //                    {
                        //                        return StatusSendBook.NotSendBook;
                        //                    }
                        //            }
                        //        }
                        //        else
                        //        {
                        //            return StatusSendBook.NotSendBook;
                        //        }
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        globalCls.MessageError("SendToBookCls", "SendToBook(int tpr_id, string endPointAddress)", ex.Message);
                        return StatusSendBook.Error;
                    }
                }
            }
            catch (Exception ex)
            {
                globalCls.MessageError("SendToBookCls", "SendToBook(int tpr_id, string endPointAddress)", ex.Message);
                return StatusSendBook.Error;
            }
        }
        private void SendQueueBook(ref trn_patient_regi patientRegis, DateTime dateNow)
        {
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    int patientSite = patientRegis.mhs_id;
                    int mrm_idBook = dbc.mst_room_hdrs.Where(x => x.mhs_id == patientSite && x.mrm_code == "BK").Select(x => x.mrm_id).FirstOrDefault();
                    int mvt_idBook = dbc.mst_events.Where(x => x.mvt_code == "BK").Select(x => x.mvt_id).FirstOrDefault();
                    
                    patientRegis.tpr_status = "WB";
                    patientRegis.tpr_pe_status = "RS";

                    trn_patient_queue patientQueue = patientRegis.trn_patient_queues.Where(x => x.mrm_id == mrm_idBook && x.mvt_id == mvt_idBook).FirstOrDefault();
                    if (patientQueue == null)
                    {
                        patientQueue = new trn_patient_queue();
                        patientQueue.mrm_id = mrm_idBook;
                        patientQueue.mvt_id = mvt_idBook;
                        patientQueue.mrd_id = null;
                        patientQueue.tps_end_date = null;
                        patientQueue.tps_start_date = null;
                        patientQueue.tps_status = "NS";
                        patientQueue.tps_ns_status = "QL";
                        patientQueue.tps_create_by = "SysPend";
                        patientQueue.tps_create_date = dateNow;
                        patientQueue.tps_update_by = "SysPend";
                        patientQueue.tps_update_date = dateNow;
                        patientRegis.trn_patient_queues.Add(patientQueue);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void EndQueueCheckC(ref trn_patient_regi patientRegis, DateTime dateNow)
        {
            List<trn_patient_queue> patientQueue = patientRegis.trn_patient_queues.Where(x => x.mst_room_hdr.mrm_code == "CC").ToList();
            foreach (trn_patient_queue queue in patientQueue)
            {
                if (queue.tps_status == "WK")
                {
                    queue.tps_status = "ED";
                    queue.tps_send_by = "SysPend";
                    queue.tps_end_date = dateNow;
                    queue.tps_update_by = "SysPend";
                    queue.tps_update_date = dateNow;
                }
            }
        }
    }
}