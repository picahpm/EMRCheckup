using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;//add suriya 01/04/2015
using DBCheckup;

namespace BKvs2010
{
    public partial class ImportPatientToBookFrm : Form
    {
        public StatusTransaction status = StatusTransaction.False;

        private class strGridStation
        {
            public string station_name { get; set; }
            public int mvt_id { get; set; }
            public string status { get; set; }
        }

        private int? _tpr_id;
        public int? tpr_id
        {
            get
            {
                return _tpr_id;
            }
        }

        public ImportPatientToBookFrm()
        {
            InitializeComponent();
            txtSearchEN.Leave += new EventHandler(txtSearch_Leave);
            txtSearchHN.Leave += new EventHandler(txtSearch_Leave);
        }

        //private void btnSearch_Click(object sender, EventArgs e)
        //{
        //    label3.Visible = false;
        //    label4.Visible = false;
        //    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
        //    {
        //        trn_patient patient = cdc.trn_patients
        //                                 .Where(x => x.tpt_hn_no == txtSearchHN.Text ||
        //                                             x.tpt_hn_no.Replace("-", "") == txtSearchHN.Text).FirstOrDefault();
        //        if (patient != null)
        //        {
        //            trn_patient_regi patient_regis = patient.trn_patient_regis
        //                                                    .Where(x => x.tpr_en_no == txtSearchEN.Text ||
        //                                                                x.tpr_en_no.Replace("-", "") == txtSearchEN.Text).FirstOrDefault();
        //            if (patient_regis != null)
        //            {
        //                patientProfileUC1.tpr_id = patient_regis.tpr_id;
        //                List<Class.GetDataFromWSTrakCare.StationStatus> list_StationStatus = new List<Class.GetDataFromWSTrakCare.StationStatus>();
        //                new Class.GetDataFromWSTrakCare().WS_GetPTPackageAllStation(ref list_StationStatus, patient_regis.tpr_id);

        //                var source = (from ss in list_StationStatus
        //                              join me in cdc.mst_events
        //                              on ss.mvt_id equals me.mvt_id
        //                              select new strGridStation
        //                              {
        //                                  station_name = me.mvt_ename,
        //                                  mvt_id = ss.mvt_id,
        //                                  status = ss.status
        //                              }).ToList();
        //                gridStation.DataSource = source;

        //                foreach (DataGridViewRow gr in gridStation.Rows)
        //                {
        //                    gr.Cells["colChk"].Value = true;
        //                    if (gr.Cells["colStatus"].Value.ToString() == "E")
        //                    {
        //                        gr.Cells["colChk"].ReadOnly = true;
        //                    }
        //                    else
        //                    {
        //                        gr.Cells["colChk"].ReadOnly = false;
        //                    }
        //                }
        //                btnImport.Enabled = true;
        //                _tpr_id = patient_regis.tpr_id;
        //            }
        //            else
        //            {
        //                label4.Visible = true;
        //                gridStation.DataSource = new List<strGridStation>();
        //                patientProfileUC1.Clear();
        //                txtSearchEN.Focus();
        //                txtSearchEN.SelectionStart = 0;
        //                txtSearchEN.SelectionLength = txtSearchEN.Text.Length;
        //                btnImport.Enabled = false;
        //                _tpr_id = null;
        //            }
        //        }
        //        else
        //        {
        //            label3.Visible = true;
        //            gridStation.DataSource = new List<strGridStation>();
        //            patientProfileUC1.Clear();
        //            txtSearchHN.Focus();
        //            txtSearchHN.SelectionStart = 0;
        //            txtSearchHN.SelectionLength = txtSearchEN.Text.Length;
        //            btnImport.Enabled = false;
        //            _tpr_id = null;
        //        }
        //    }
        //} //del suriya 01/04/2015

        private bool ImportPatient(ref int? tpr_id)
        {
            try
            {
                tpr_id = null;
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    var resultEPI = ws.GetEPIRowIDByEN(txtSearchEN.Text).AsEnumerable()
                                      .Select(x => new
                                      {
                                          en = x.Field<string>("PAADM_ADMNo"),
                                          en_rowid = x.Field<int>("PAADM_RowID"),
                                          arrived_date = x.Field<DateTime>("PAADM_AdmDate")
                                      }).FirstOrDefault();
                    if (resultEPI == null)
                    {
                        MessageBox.Show("ไม่พบ Episode นี้ในระบบ Trakcare", "Warning!!");
                        return false;
                    }
                    else
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            trn_patient_regi patient_regis = cdc.trn_patient_regis
                                                                .Where(x => x.tpr_en_no == resultEPI.en)
                                                                .FirstOrDefault();
                            if (patient_regis == null)
                            {
                                var resultArrived = ws.GetPTArrivedCheckUpFilter(Program.CurrentSite.mhs_code, resultEPI.en, resultEPI.arrived_date.ToString("yyyy-MM-dd")).AsEnumerable()
                                //var resultArrived = ws.GetPTArrivedCheckUpFilter(Program.CurrentSite.mhs_code, resultEPI.en, "2017-03-08").AsEnumerable()
                                                      .Select(x => new tmp_getptarrived
                                                      {
                                                          paadm_type_of_patient_calc = x.Field<string>("PAADM_Type_of_Patient_Calc"),
                                                          paadm_rowid = x.Field<int>("PAADM_RowID").ToString(),
                                                          appt_rowid = x.Field<string>("APPT_RowId"),
                                                          appt_arrivaltime = x.Field<TimeSpan>("APPT_ArrivalTime").ToString(),
                                                          paadm_admdate = x.Field<DateTime?>("PAADM_AdmDate"),
                                                          allergy_eng = new APITrakcare.GetAllergyCls().GetByHN(x.Field<string>("PAPMI_No")),//call webserice
                                                          paadm_admno = x.Field<string>("PAADM_ADMNo"),
                                                          papmi_no = x.Field<string>("PAPMI_No"),
                                                          ttl_desc = x.Field<string>("TTL_Desc"),
                                                          papmi_name = x.Field<string>("PAPMI_Name"),
                                                          papmi_name2 = x.Field<string>("PAPMI_Name2"),
                                                          appt_transdate = x.Field<DateTime?>("APPT_TransDate"),
                                                          appt_datesearch = x.Field<DateTime?>("APPT_DateSearch"),
                                                          paadm_admtime = x.Field<TimeSpan>("PAADM_admTime").ToString(),
                                                          ctloc_code = Program.CurrentSite.mhs_code,
                                                          ctloc_desc = x.Field<string>("CTLOC_Desc"),
                                                          penstype_code = x.Field<string>("PENSTYPE_Code"),
                                                          penstype_desc = x.Field<string>("PENSTYPE_Desc"),
                                                          ser_rowid = x.Field<int>("SER_RowId"),
                                                          ser_desc = x.Field<string>("SER_Desc"),
                                                          ctnat_code = x.Field<string>("CTNAT_Code"),
                                                          ctnat_desc = x.Field<string>("CTNAT_Desc"),
                                                          ctsex_code = x.Field<string>("CTSEX_Code"),
                                                          ctsex_desc = x.Field<string>("CTSEX_Desc"),
                                                          papmi_dob = x.Field<DateTime?>("PAPMI_DOB"),
                                                          paper_photo = new EmrClass.GetPatientImage().getByPath(x.Field<string>("PAPMI_No")),

                                                          paper_ageyr = x.Field<string>("PAPER_AgeYr"),
                                                          paper_agemth = x.Field<string>("PAPER_AgeMth"),
                                                          paper_ageday = x.Field<string>("PAPER_AgeDay"),
                                                          paper_stname = x.Field<string>("PAPER_StName"),
                                                          citarea_desc = x.Field<string>("CITAREA_Desc"),
                                                          prov_desc = x.Field<string>("PROV_Desc"),
                                                          ctcit_desc = x.Field<string>("CTCIT_Desc"),
                                                          ctzip_code = x.Field<string>("CTZIP_Code"),
                                                          paper_id = x.Field<string>("PAPER_ID"),
                                                          paper_telo = x.Field<string>("PAPER_TelO"),
                                                          paper_telh = x.Field<string>("PAPER_TelH"),
                                                          paper_mobphone = x.Field<string>("PAPER_MobPhone"),
                                                          paper_email = x.Field<string>("PAPER_Email"),

                                                          ctmar_desc = x.Field<string>("CTMAR_Desc"),
                                                          paper_name5 = x.Table.Columns.Contains("PAPMI_Name5") 
                                                                        ? x.Field<string>("PAPMI_Name5") 
                                                                        : x.Table.Columns.Contains("PAPER_Name5") 
                                                                        ? x.Field<string>("PAPER_Name5") 
                                                                        : "",
                                                          paper_name6 = x.Table.Columns.Contains("PAPMI_Name6") 
                                                                        ? x.Field<string>("PAPMI_Name6") 
                                                                        : x.Table.Columns.Contains("PAPER_Name6") 
                                                                        ? x.Field<string>("PAPER_Name6")
                                                                        : "",
                                                          paper_name7 = x.Table.Columns.Contains("PAPMI_Name7") 
                                                                        ? x.Field<string>("PAPMI_Name7") 
                                                                        : x.Table.Columns.Contains("PAPER_Name7") 
                                                                        ? x.Field<string>("PAPER_Name7") 
                                                                        : "",
                                                          papmi_dob_text = x.Field<string>("papmi_dob_text")
                                                      }).FirstOrDefault();
                                if (resultArrived == null)
                                {
                                    MessageBox.Show("ไม่พบ Episode นี้ใน Location ==>" + Program.CurrentSite.mhs_ename, "Warning!!");
                                    return false;
                                }
                                else
                                {
                                    bool regis = new EmrClass.FunctionDataCls().RegisterPatient(resultArrived);
                                    if (!regis)
                                    {
                                        MessageBox.Show("โปรดลองอีกครั้ง");
                                        return false;
                                    }
                                    else
                                    {
                                        patient_regis = cdc.trn_patient_regis
                                                            .Where(x => x.tpr_en_no == resultEPI.en)
                                                            .FirstOrDefault();
                                    }
                                }
                            }

                            tpr_id = patient_regis.tpr_id;
                            return true;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("โปรดลองอีกครั้ง");
                return false;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)//add suriya 01/04/2015
        {
            label3.Visible = false;
            label4.Visible = false;
            int? tpr_id = null;
            if (ImportPatient(ref tpr_id))
            {
                if (tpr_id != null)
                {
                    patientProfileUC1.tpr_id = tpr_id;
                    List<StationStatus> list_StationStatus = new List<StationStatus>();
                    new EmrClass.GetDataFromWSTrakCare().WS_GetPTPackageAllStation(ref list_StationStatus, (int)tpr_id);

                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        var source = (from ss in list_StationStatus
                                      join me in cdc.mst_events
                                      on ss.mvt_id equals me.mvt_id
                                      select new strGridStation
                                      {
                                          station_name = me.mvt_ename,
                                          mvt_id = ss.mvt_id,
                                          status = ss.status
                                      }).ToList();
                        gridStation.DataSource = source;

                        foreach (DataGridViewRow gr in gridStation.Rows)
                        {
                            gr.Cells["colChk"].Value = true;
                            if (gr.Cells["colStatus"].Value.ToString() == "E")
                            {
                                gr.Cells["colChk"].ReadOnly = true;
                            }
                            else
                            {
                                gr.Cells["colChk"].ReadOnly = false;
                            }
                        }
                        btnImport.Enabled = true;
                        _tpr_id = tpr_id;
                    }
                    return;
                }
            }

            label4.Visible = true;
            gridStation.DataSource = new List<strGridStation>();
            patientProfileUC1.Clear();
            txtSearchEN.Focus();
            txtSearchEN.SelectionStart = 0;
            txtSearchEN.SelectionLength = txtSearchEN.Text.Length;
            btnImport.Enabled = false;
            _tpr_id = null;
        }
        private void btnImport_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คำเตือน!! การ Import มีผลกระทบต่อระบบ Queue คุณต้องการ Import หรือไม่?", "Warning!!", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            List<int> mvt_id = new List<int>();
            foreach (DataGridViewRow row in gridStation.Rows)
            {
                try
                {
                    bool val = Convert.ToBoolean(row.Cells["colChk"].Value);
                    if (val)
                    {
                        int mvt = Convert.ToInt32(row.Cells["col_mvt_id"].Value);
                        mvt_id.Add(mvt);
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError(this.Name, "btnImport_Click", ex, false);
                }
            }

            StatusTransaction tran = new EmrClass.ImportPatientToBookCls().importPatient((int)_tpr_id, mvt_id);
            if (tran == StatusTransaction.True)
            {
                status = StatusTransaction.True;
                this.Close();
            }
            else if (tran == StatusTransaction.Error)
            {
                status = StatusTransaction.Error;
                _tpr_id = null;
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            TextBox txtBox = (TextBox)sender;
            string txt = txtBox.Text.Trim();
            txtBox.Text = txt;
            txtBox.Text = txt.ToUpper();
        }
    }
}
