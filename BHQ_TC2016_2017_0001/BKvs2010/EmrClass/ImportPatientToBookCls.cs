using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using DBCheckup;

namespace BKvs2010.EmrClass
{
    public class ImportPatientToBookCls
    {
        public StatusTransaction importPatient(int tpr_id, List<int> mvt_id)
        {
            try
            {
                using (InhCheckupDataContext contxt = new InhCheckupDataContext())
                {
                    DateTime dateNow = Program.GetServerDateTime();
                    List<trn_RefreshLabHistory> his = contxt.trn_RefreshLabHistories.Where(x => x.tpr_id == tpr_id).ToList();
                    his.ForEach(x => x.status = true);
                    contxt.SubmitChanges();

                    trn_patient_regi patient_regis = contxt.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                    using (Service.WS_CheckupCls wsCheckup = new Service.WS_CheckupCls())
                    {
                        wsCheckup.InsertDBEmrCheckupResultXray(patient_regis.trn_patient.tpt_hn_no, patient_regis.tpr_en_no, dateNow.AddYears(-5), dateNow, true);
                    }
                }

                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    try
                    {
                        string username = Program.CurrentUser == null ? "" : Program.CurrentUser.mut_username;
                        DateTime dateNow = Program.GetServerDateTime();
                        cdc.Connection.Open();
                        DbTransaction trans = cdc.Connection.BeginTransaction();
                        cdc.Transaction = trans;

                        trn_patient_regi patient_regis = cdc.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();

                        patient_regis.trn_patient_queues.ToList().ForEach(x => { x.tps_status = "ED"; x.tps_ns_status = null; x.tps_update_by = username; x.tps_update_date = dateNow; });

                        int enRowID = Convert.ToInt32(patient_regis.tpr_en_rowid);
                        EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                        EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                        PackageCls.AddPatientOrderItem(ref patient_regis, username, dateNow, getPTPackage);
                        PackageCls.AddPatientOrderSet(ref patient_regis, username, dateNow, getPTPackage);
                        List<MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                        PackageCls.AddPatientEvent(ref patient_regis, username, dateNow, mapOrder);
                        PackageCls.AddPatientPlan(ref patient_regis, username, dateNow, mapOrder);
                        PackageCls.skipReqDoctorOutDepartment(ref patient_regis);
                        PackageCls.CompleteEcho(ref patient_regis);
                        PackageCls.skipChangeEstToEcho(ref patient_regis, patient_regis.mhs_id);
                        PackageCls.checkOrderPMR(ref patient_regis, patient_regis.mhs_id);

                        patient_regis.tpr_status = "WB";
                        patient_regis.tpr_pe_status = "RS";
                        try
                        {
                            cdc.SubmitChanges();
                        }
                        catch (System.Data.Linq.ChangeConflictException)
                        {
                            foreach (System.Data.Linq.ObjectChangeConflict occ in cdc.ChangeConflicts)
                            {
                                cdc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                            }
                            cdc.SubmitChanges();
                        }
                        PackageCls.setRelationOrderSet(ref patient_regis);
                        cdc.SubmitChanges();
                        cdc.Transaction.Commit();

                        using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                        {
                            ws.retrieveVitalSign(tpr_id, username);
                            ws.getCheckUpLabResult(patient_regis.trn_patient.tpt_hn_no, tpr_id);
                        }
                        return StatusTransaction.True;
                    }
                    catch (Exception ex)
                    {
                        cdc.Transaction.Rollback();
                        Program.MessageError("ImportPatientToBookCls", "importPatient", ex, false);
                        return StatusTransaction.Error;
                    }
                    finally
                    {
                        cdc.Connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ImportPatientToBookCls", "importPatient", ex, false);
                return StatusTransaction.Error;
            }
        }
    }
}
