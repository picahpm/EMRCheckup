using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using DBToDoList;
using System.Collections;
using BKvs2010.Class;

//add suriya 17/12/2014
using System.Runtime.Serialization.Json;
using System.Configuration;
using System.Net;
using System.IO;

//end suriya 17/12/2014

namespace BKvs2010
{
    public partial class frmRegister : Form
    {
        private bool _loadsuccess;
        public string patient_type;

        public frmRegister()
        {
            InitializeComponent();
            setRadioPEonSite2();
        }
        private void setRadioPEonSite2()
        {
            string mhs_code = Program.CurrentSite.mhs_code;
            tbPnPESite.Height = 30;

            if (Program.CurrentSite.mhs_extra_pe_type == true)
            {
                tbPnPESite.RowStyles[0].Height = 0;
                tbPnPESite.RowStyles[1].Height = 26;
                tbPnPESite.Enabled = false;
                rdPEWaitResult.Checked = false;
                rdPENotWaitResult.Checked = false;
            }
            else
            {
                tbPnPESite.RowStyles[0].Height = 26;
                tbPnPESite.RowStyles[1].Height = 0;
                tbPnPESite.Enabled = true;
                GBRequestPEBefore.Enabled = true;
            }
        }

        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public Boolean IsAddnew { get; set; }
        public Boolean Iscompleted = false;
        string strSelected, nationcode;
        int mhcid;
        public int SelectedID;
        public string strSelectedName;
        private static List<pw_Get_AviationTypeResult> getAviationType;
        SkipOrderSetEST runskip = new SkipOrderSetEST();
        ToolTip toolTip1 = new ToolTip();

        private void frmRegister_Load(object sender, EventArgs e)
        {
            _loadsuccess = false;
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            try
            {
                this.Text = Program.GetRoomName();
                DateTime dateNow = Program.GetServerDateTime();
                setDDLocationOutDepartment(dateNow);

                LoadData();

                // Add Button in Gridview
                DataGridViewButtonColumn doppatientButton = new DataGridViewButtonColumn();
                doppatientButton.HeaderText = "";
                doppatientButton.Name = "ColDelete";
                doppatientButton.UseColumnTextForButtonValue = true;
                doppatientButton.Text = "ลบ";
                doppatientButton.Width = 40;
                GridPatientAlert.Columns.Add(doppatientButton);

                this.GetAllergyByHN(Program.Tmp_GetPtarrived.papmi_no);

                //// start tune performance
                //if (isAddPreRegis == false)
                //{
                //    Ws_GetDataByTrak.WS_GetDataBytrakSoapClient ws = new Ws_GetDataByTrak.WS_GetDataBytrakSoapClient();
                //    //Ws_GetDataByTrak_test.WS_GetDataBytrakSoapClient ws = new Ws_GetDataByTrak_test.WS_GetDataBytrakSoapClient();
                //    //LoadOutDepart
                //    DataTable dtOutDepart = ws.getApptByHN(Program.Tmp_GetPtarrived.papmi_no, Program.Tmp_GetPtarrived.ctloc_code, dateNow.ToString("yyyy-MM-dd"));
                //    foreach (DataRow droutDepart in dtOutDepart.Rows)
                //    {
                //        trn_out_department newitem = (trn_out_department)trnoutdepartmentsBindingSource.AddNew();
                //        newitem.tod_desc = droutDepart["SER_Desc"].ToString();
                //        newitem.tod_location = droutDepart["CTLOC_Desc"].ToString();

                //        try
                //        {
                //            DateTime dtdata = Convert.ToDateTime(droutDepart["AS_Date"].ToString());
                //            TimeSpan tarrivaltime = TimeSpan.Parse(droutDepart["AS_SessStartTime"].ToString().Replace("PT", "").Replace("H", ":").Replace("M", ""));
                //            newitem.tod_start_date = new DateTime(dtdata.Year, dtdata.Month, dtdata.Day, tarrivaltime.Hours, tarrivaltime.Minutes, 0);
                //        }
                //        catch (Exception)
                //        {
                //            newitem.tod_start_date = null;
                //            return;
                //        }
                //        newitem.tod_create_by = Program.CurrentUser.mut_id;
                //        newitem.tod_create_date = dateNow;
                //        newitem.tod_update_by = newitem.tod_create_by;
                //        newitem.tod_update_date = newitem.tod_create_date;
                //    }
                //}
                //GenRowNoDridoutDepartment();
                //btnloadPackageTK_Click(null, null);
                //// end tune performance

                //ไม่Default Aviation Type
                RDAviationCategoryNewcase.Enabled = false;
                RDaviationTypeFollowup.Enabled = false;
                RDAviationCategoryNewcase.Checked = false;
                RDaviationTypeFollowup.Checked = false;
                CBAviationCategory.Enabled = false;
                // Other
                GridOtherAddress.AutoGenerateColumns = false;
                panel2.Enabled = true;
                GC.Collect();

                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "";
                btn.Name = "Coldel";
                btn.UseColumnTextForButtonValue = true;
                btn.Text = "Delete";
                btn.Width = 70;
                Gridout_department.Columns.Add(btn);
                //
                //
                //if (this.GridPackage.Rows.Count > 0)
                //    txtHealthCheckup.Text = GridPackage.Rows[0].Cells[1].Value.ToString();
                trn_patient_regi PRegis = PatientRegisBindingSource.OfType<trn_patient_regi>().FirstOrDefault();
                if (PRegis != null)
                {
                    string setName = PRegis.tpr_mhc_ename;
                    chsameMainAddress.Checked = true;
                    PRegis.tpr_other_address = PRegis.tpr_main_address;
                    PRegis.tpr_other_province = PRegis.tpr_main_province;
                    PRegis.tpr_other_amphur = PRegis.tpr_main_amphur;
                    PRegis.tpr_other_tumbon = PRegis.tpr_main_tumbon;
                    PRegis.tpr_other_zip_code = PRegis.tpr_main_zip_code;
                }
                else
                {
                    txtHealthCheckup.Text = "";
                }
                _loadsuccess = true;

            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "frmRegister_Load", ex, false);
            }
            frmbg.Close();
        }
        private void setDDLocationOutDepartment(DateTime dateNow)
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var objLocation = cdc.mst_locations
                                     .Where(x => (x.mlc_effective_date == null ? true : dateNow.Date >= x.mlc_effective_date.Value.Date) &&
                                                 (x.mlc_expire_date == null ? true : dateNow.Date <= x.mlc_expire_date.Value.Date))
                                     .Select(x => new
                                     {
                                         strlocation = x.mlc_ename
                                     }).ToList();
                objLocation.Insert(0, new { strlocation = "- Select -" });

                DDLocation.DataSource = objLocation;
                DDLocation.DisplayMember = "strlocation";
                DDLocation.ValueMember = "strlocation";
            }
        }

        private void Clear()
        {
            ListAviationFinalSelect.Items.Clear();
            ListAviationSelect.Items.Clear();
            g1.Rows.Clear();
            g2.Rows.Clear();
            foreach (int i in CheckListAviation.CheckedIndices)
                CheckListAviation.SetItemCheckState(i, CheckState.Unchecked);
        }


        private void LoadPatient()
        {
            try
            {
                if (Program.Tmp_GetPtarrived == null)
                {
                    this.Close();
                    return;
                }
                else
                {
                    LoadCompany();

                    DateTime dateNow = Program.GetServerDateTime();
                    trn_patient patient = new trn_patient();
                    int tpt_id = dbc.trn_patients.Where(x => x.tpt_hn_no == Program.Tmp_GetPtarrived.papmi_no).Select(x => x.tpt_id).FirstOrDefault();
                    if (tpt_id == 0)
                    {
                        dbc.trn_patients.InsertOnSubmit(patient);
                    }
                    else
                    {
                        patient = dbc.trn_patients.Where(x => x.tpt_id == tpt_id).FirstOrDefault();
                    }
                    this.PatientBindingSource.DataSource = patient;

                    if (Program.Tmp_GetPtarrived.ttl_desc != null && Program.Tmp_GetPtarrived.ttl_desc != "")
                    {
                        patient.tpt_pre_name = Program.Tmp_GetPtarrived.ttl_desc;
                    }
                    if (Program.Tmp_GetPtarrived.papmi_name != null && Program.Tmp_GetPtarrived.papmi_name != "")
                    {
                        patient.tpt_first_name = Program.Tmp_GetPtarrived.papmi_name;
                    }
                    if (Program.Tmp_GetPtarrived.papmi_name2 != null && Program.Tmp_GetPtarrived.papmi_name2 != "")
                    {
                        patient.tpt_last_name = Program.Tmp_GetPtarrived.papmi_name2;
                    }
                    if (Program.Tmp_GetPtarrived.paper_photo != null)
                    {
                        patient.tpt_image = Program.Tmp_GetPtarrived.paper_photo;
                    }

                    patient.tpt_create_date = dateNow;
                    patient.tpt_update_date = dateNow;
                    patient.tpt_hn_no = Program.Tmp_GetPtarrived.papmi_no;
                    patient.tpt_gender = Convert.ToChar(Program.Tmp_GetPtarrived.ctsex_code);

                    patient.tpt_pre_name = Program.Tmp_GetPtarrived.ttl_desc;
                    patient.tpt_first_name = Program.Tmp_GetPtarrived.papmi_name;
                    patient.tpt_last_name = Program.Tmp_GetPtarrived.papmi_name2;
                    patient.tpt_image = Program.Tmp_GetPtarrived.paper_photo;

                    patient.tpt_dob = Program.Tmp_GetPtarrived.papmi_dob;
                    patient.tpt_dob_text = Program.Tmp_GetPtarrived.papmi_dob_text;
                    patient.tpt_nation_code = Program.Tmp_GetPtarrived.ctnat_code;
                    patient.tpt_nation_desc = Program.Tmp_GetPtarrived.ctnat_desc;
                    patient.tpt_id_card = Program.Tmp_GetPtarrived.paper_id;
                    nationcode = Program.Tmp_GetPtarrived.ctnat_code;
                    dataFullName.Text = patient.tpt_othername;
                    dataGender.Text = patient.tpt_gender == 'M' ? "ชาย(Male)" : "หญิง(Female)";
                    dataDOB.Text = Program.Tmp_GetPtarrived.papmi_dob.Value.ToString("yyyy-MM-dd");

                    if (patient.tpt_dob != null)
                    {
                        dataAge.Text = Program.CalculateAge(patient.tpt_dob.Value, dateNow);
                    }
                    //new En1 En2 En3 Marri
                    //S=Single,M=Married,W=Widowed,D=Divorced,U=Unknown
                    if (Program.Tmp_GetPtarrived.ctmar_desc != null && Program.Tmp_GetPtarrived.ctmar_desc != "")
                    {
                        string strmarried = CheckdMarried(Program.Tmp_GetPtarrived.ctmar_desc);
                        string[] datamrr = strmarried.Split(',');
                        if (datamrr[0].ToString() != "")
                        {
                            patient.tpt_married = Convert.ToChar(datamrr[0]);
                            patient.tpt_married_desc = datamrr[1];
                        }
                    }
                    patient.tpt_en_name1 = Program.Tmp_GetPtarrived.paper_name5;
                    patient.tpt_en_name2 = Program.Tmp_GetPtarrived.paper_name6;
                    patient.tpt_en_name3 = Program.Tmp_GetPtarrived.paper_name7;


                    trn_patient_regi patient_regis = new trn_patient_regi();
                    patient.trn_patient_regis.Add(patient_regis);

                    patient_regis.mhs_id = Program.CurrentSite.mhs_id;
                    patient_regis.tpr_vip_code = Program.Tmp_GetPtarrived.penstype_code;
                    patient_regis.tpr_vip_desc = Program.Tmp_GetPtarrived.penstype_desc;

                    txtMainAddress.Text = getstring(Program.Tmp_GetPtarrived.paper_stname) +
                                          " แขวง " + getstring(Program.Tmp_GetPtarrived.citarea_desc) +
                                          " เขต " + getstring(Program.Tmp_GetPtarrived.ctcit_desc) +
                                          " จังหวัด " + getstring(Program.Tmp_GetPtarrived.prov_desc) +
                                          " " + getstring(Program.Tmp_GetPtarrived.ctzip_code);

                    patient_regis.tpr_main_address = getstring(Program.Tmp_GetPtarrived.paper_stname);
                    patient_regis.tpr_main_tumbon = getstring(Program.Tmp_GetPtarrived.citarea_desc);
                    patient_regis.tpr_main_amphur = getstring(Program.Tmp_GetPtarrived.ctcit_desc);
                    patient_regis.tpr_main_province = getstring(Program.Tmp_GetPtarrived.prov_desc);
                    patient_regis.tpr_main_zip_code = getstring(Program.Tmp_GetPtarrived.ctzip_code);
                    patient_regis.tpr_foreigner = (Program.Tmp_GetPtarrived.ctnat_code == "TH") ? 'N' : 'Y';
                    patient_regis.tpr_update_by = Program.CurrentUser.mut_username;
                    patient_regis.tpr_create_date = dateNow;
                    patient_regis.tpr_update_date = dateNow;
                    patient_regis.tpr_en_no = Program.Tmp_GetPtarrived.paadm_admno;
                    patient_regis.tpr_en_rowid = Program.Tmp_GetPtarrived.paadm_rowid;
                    patient_regis.tpr_new_patient = (Program.Tmp_GetPtarrived.paadm_type_of_patient_calc == "1" || Program.Tmp_GetPtarrived.paadm_type_of_patient_calc == "2") ? 'Y' : 'N';

                    patient_regis.tpr_mobile_phone = Program.Tmp_GetPtarrived.paper_mobphone;
                    patient_regis.tpr_office_phone = Program.Tmp_GetPtarrived.paper_telo;
                    patient_regis.tpr_home_phone = Program.Tmp_GetPtarrived.paper_telh;
                    patient_regis.tpr_email = Program.Tmp_GetPtarrived.paper_email;

                    //Load Other Address
                    var objhistory_regis = patient.trn_patient_regis.Where(x => x.tpr_id != 0)
                                                  .OrderByDescending(x => x.tpr_create_date)
                                                  .FirstOrDefault();
                    if (objhistory_regis != null)
                    {
                        patient_regis.tpr_other_address = objhistory_regis.tpr_other_address;
                        patient_regis.tpr_other_amphur = objhistory_regis.tpr_other_amphur;
                        patient_regis.tpr_other_province = objhistory_regis.tpr_other_province;
                        patient_regis.tpr_other_tumbon = objhistory_regis.tpr_other_tumbon;
                        patient_regis.tpr_other_zip_code = objhistory_regis.tpr_other_zip_code;
                    }

                    trnoutdepartmentsBindingSource.DataSource = patient_regis.trn_out_departments;
                    trnpatientorderitemsBindingSource.DataSource = patient_regis.trn_patient_order_items;
                    trnpatientordersetsBindingSource.DataSource = patient_regis.trn_patient_order_sets;
                    //if (patient_regis.trn_patient_order_sets.Count() > 0)
                    //{

                    //}
                    //else
                    //{
                    //    trnpatientordersetsBindingSource.DataSource = null;
                    //}
                    this.CheckQueueType();
                    CheckPendding();
                    //*** yee กรณีที่Login ด้วย site 2 ให้Default patient Type =Corporate
                    if (Program.CurrentSite.mhs_extra_pe_type == true)
                    {
                        RDPatientType_Corporate.Checked = true;
                    }
                    else
                    {
                        RDPatientType_General.Checked = true;
                    }

                    lbdataPatientAlert.Text = "";
                    foreach (trn_patient_alert ptitem in patient.trn_patient_alerts)
                    {
                        lbdataPatientAlert.Text += "-" + ptitem.tpa_alert + Environment.NewLine;
                    }

                    if (lbdataPatientAlert.Text.Length > 0)
                    {
                        btnPatientAlertView.Visible = true;
                    }
                    else
                    {
                        btnPatientAlertView.Visible = false;
                    }
                    this.SetComboBoxControl();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadPatient", ex, false);
            }
        }
        private void LoadCompany()
        {
            //try
            //{
            //    EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();
            //    List<ObjCompany> objCom = cls.getListCompany();
            //    DDcompany.DataSource = objCom;
            //    DDcompany.ValueMember = "code";
            //    DDcompany.DisplayMember = "name";

            //    trn_name_check tnc = cls.getNameCheck(Program.Tmp_GetPtarrived.papmi_no);
            //    if (tnc != null)
            //    {
            //        DDcompany.SelectedValue = tnc.trn_company_detail.tcd_code;
            //        txtEmployeeID.Text = tnc.tnc_emp_id;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Program.MessageError(this.Name, "LoadCompany", ex, false);
            //}
        }
        private void LoadData()
        {
            try
            {
                //if (Program.Tmp_GetPtarrived != null)
                //{
                //    EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();
                //    List<ObjCompany> objCom = cls.getListCompany();
                //    DDcompany.DataSource = objCom;
                //    DDcompany.ValueMember = "code";
                //    DDcompany.DisplayMember = "name";

                //    trn_name_check tnc = cls.getNameCheck(Program.Tmp_GetPtarrived.papmi_no);
                //    if (tnc != null)
                //    {
                //        DDcompany.SelectedValue = tnc.trn_company_detail.tcd_code;
                //        txtEmployeeID.Text = tnc.tnc_emp_id;
                //    }
                //}

                if (IsAddnew)
                {
                    DateTime Datenow = Program.GetServerDateTime();
                    if (Program.Tmp_GetPtarrived == null)
                    {
                        this.Close();
                        return;
                    }

                    if (Program.Tmp_GetPtarrived != null)
                    {
                        //เช็คว่า HN นี้มีอยู่หรือไม่
                        var objpatientlist = (from t1 in dbc.trn_patients
                                              where t1.tpt_hn_no == Program.Tmp_GetPtarrived.papmi_no
                                              select t1).FirstOrDefault();
                        if (objpatientlist != null)
                        {
                            this.PatientBindingSource.DataSource = objpatientlist;
                            //คำสั่ง update Patient Profile กรณีที่เป็นคนเก่า 
                            var objpatientupdate = (trn_patient)this.PatientBindingSource.Current;
                            if (Program.Tmp_GetPtarrived.ttl_desc != null && Program.Tmp_GetPtarrived.ttl_desc != "")
                            {
                                objpatientupdate.tpt_pre_name = Program.Tmp_GetPtarrived.ttl_desc;
                            }
                            if (Program.Tmp_GetPtarrived.papmi_name != null && Program.Tmp_GetPtarrived.papmi_name != "")
                            {
                                objpatientupdate.tpt_first_name = Program.Tmp_GetPtarrived.papmi_name;
                            }
                            if (Program.Tmp_GetPtarrived.papmi_name2 != null && Program.Tmp_GetPtarrived.papmi_name2 != "")
                            {
                                objpatientupdate.tpt_last_name = Program.Tmp_GetPtarrived.papmi_name2;
                            }
                            if (Program.Tmp_GetPtarrived.paper_photo != null)
                            {
                                objpatientupdate.tpt_image = Program.Tmp_GetPtarrived.paper_photo;
                            }
                        }
                        else
                        {
                            this.PatientBindingSource.DataSource = (from t1 in dbc.trn_patients select t1).Take(0);//วิธีการดึง Diagrame มาใช้สำหรับ insert data new
                            this.PatientBindingSource.AddNew();
                        }
                        // เช็คว่า HN
                        var objpatient = (trn_patient)this.PatientBindingSource.Current;

                        try
                        {
                            string tname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.ttl_desc) ? "" : Program.Tmp_GetPtarrived.ttl_desc.Trim();
                            string fname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.papmi_name) ? "" : " " + Program.Tmp_GetPtarrived.papmi_name.Trim();
                            string mname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.paper_name7) ? "" : " " + Program.Tmp_GetPtarrived.paper_name7.Trim();
                            string lname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.papmi_name2) ? "" : " " + Program.Tmp_GetPtarrived.papmi_name2.Trim();
                            string fullname = (tname + fname + mname + lname).Trim();
                            objpatient.tpt_fullname = fullname;
                            objpatient.tpt_othername = fullname;
                        }
                        catch (Exception ex)
                        {
                            try
                            {
                                string tname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.ttl_desc) ? "" : Program.Tmp_GetPtarrived.ttl_desc.Trim();
                                string fname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.papmi_name) ? "" : " " + Program.Tmp_GetPtarrived.papmi_name.Trim();
                                string lname = string.IsNullOrEmpty(Program.Tmp_GetPtarrived.papmi_name2) ? "" : " " + Program.Tmp_GetPtarrived.papmi_name2.Trim();
                                string fullname = (tname + fname + lname).Trim();
                                objpatient.tpt_fullname = fullname;
                                objpatient.tpt_othername = fullname;
                            }
                            catch
                            {

                            }
                            Program.MessageError(this.Name, "tpt_fullname", ex, false);
                        }
                        objpatient.tpt_create_date = Datenow;
                        objpatient.tpt_update_date = objpatient.tpt_create_date;
                        objpatient.tpt_hn_no = Program.Tmp_GetPtarrived.papmi_no;
                        objpatient.tpt_gender = Convert.ToChar(Program.Tmp_GetPtarrived.ctsex_code);

                        objpatient.tpt_pre_name = Program.Tmp_GetPtarrived.ttl_desc;
                        objpatient.tpt_first_name = Program.Tmp_GetPtarrived.papmi_name;
                        objpatient.tpt_last_name = Program.Tmp_GetPtarrived.papmi_name2;
                        objpatient.tpt_image = Program.Tmp_GetPtarrived.paper_photo;

                        objpatient.tpt_dob = Program.Tmp_GetPtarrived.papmi_dob;
                        objpatient.tpt_dob_text = Program.Tmp_GetPtarrived.papmi_dob_text;
                        objpatient.tpt_nation_code = Program.Tmp_GetPtarrived.ctnat_code;
                        objpatient.tpt_nation_desc = Program.Tmp_GetPtarrived.ctnat_desc;
                        objpatient.tpt_create_date = Datenow;
                        objpatient.tpt_update_date = objpatient.tpt_create_date;
                        objpatient.tpt_id_card = Program.Tmp_GetPtarrived.paper_id;
                        nationcode = Program.Tmp_GetPtarrived.ctnat_code;
                        dataFullName.Text = objpatient.tpt_othername;
                        dataGender.Text = objpatient.tpt_gender == 'M' ? "ชาย(Male)" : "หญิง(Female)";
                        dataDOB.Text = Program.Tmp_GetPtarrived.papmi_dob.Value.ToString("yyyy-MM-dd");

                        if (objpatient.tpt_dob != null)
                        {
                            dataAge.Text = Program.CalculateAge(objpatient.tpt_dob.Value, Datenow);
                        }
                        //new En1 En2 En3 Marri
                        //S=Single,M=Married,W=Widowed,D=Divorced,U=Unknown
                        if (Program.Tmp_GetPtarrived.ctmar_desc != null && Program.Tmp_GetPtarrived.ctmar_desc != "")
                        {
                            string strmarried = CheckdMarried(Program.Tmp_GetPtarrived.ctmar_desc);
                            string[] datamrr = strmarried.Split(',');
                            if (datamrr[0].ToString() != "")
                            {
                                objpatient.tpt_married = Convert.ToChar(datamrr[0]);
                                objpatient.tpt_married_desc = datamrr[1];
                            }
                        }
                        objpatient.tpt_en_name1 = Program.Tmp_GetPtarrived.paper_name5;
                        objpatient.tpt_en_name2 = Program.Tmp_GetPtarrived.paper_name6;
                        objpatient.tpt_en_name3 = Program.Tmp_GetPtarrived.paper_name7;
                    }
                    this.PatientRegisBindingSource.AddNew();
                    var objregis = (trn_patient_regi)this.PatientRegisBindingSource.Current;
                    objregis.mhs_id = Program.CurrentSite.mhs_id;
                    objregis.tpr_vip_code = Program.Tmp_GetPtarrived.penstype_code;
                    objregis.tpr_vip_desc = Program.Tmp_GetPtarrived.penstype_desc;
                    tmp_getptarrived ad = Program.Tmp_GetPtarrived;

                    txtMainAddress.Text = getstring(ad.paper_stname) + " แขวง " + getstring(ad.citarea_desc) + " เขต " + getstring(ad.ctcit_desc) + " จังหวัด " + getstring(ad.prov_desc) + " " + getstring(ad.ctzip_code);
                    //objregis.tpr_main_address = txtMainAddress.Text;
                    objregis.tpr_main_address = getstring(ad.paper_stname);
                    objregis.tpr_main_tumbon = getstring(ad.citarea_desc);
                    objregis.tpr_main_amphur = getstring(ad.ctcit_desc);
                    objregis.tpr_main_province = getstring(ad.prov_desc);
                    objregis.tpr_main_zip_code = getstring(ad.ctzip_code);
                    objregis.tpr_foreigner = (Program.Tmp_GetPtarrived.ctnat_code == "TH") ? 'N' : 'Y';
                    objregis.tpr_update_by = Program.CurrentUser.mut_username;
                    objregis.tpr_create_date = Datenow;
                    objregis.tpr_update_date = Datenow;
                    objregis.tpr_en_no = Program.Tmp_GetPtarrived.paadm_admno;
                    objregis.tpr_en_rowid = Program.Tmp_GetPtarrived.paadm_rowid;
                    objregis.tpr_new_patient = (Program.Tmp_GetPtarrived.paadm_type_of_patient_calc == "1" || Program.Tmp_GetPtarrived.paadm_type_of_patient_calc == "2") ? 'Y' : 'N';

                    objregis.tpr_mobile_phone = ad.paper_mobphone;
                    objregis.tpr_office_phone = ad.paper_telo;
                    objregis.tpr_home_phone = ad.paper_telh;
                    objregis.tpr_email = ad.paper_email;

                    //Load Other Address
                    var objhistory_regis = (from t1 in dbc.trn_patient_regis where t1.trn_patient.tpt_hn_no == Program.Tmp_GetPtarrived.papmi_no select t1).FirstOrDefault();
                    if (objhistory_regis != null)
                    {
                        objregis.tpr_other_address = objhistory_regis.tpr_other_address;
                        objregis.tpr_other_amphur = objhistory_regis.tpr_other_amphur;
                        objregis.tpr_other_province = objhistory_regis.tpr_other_province;
                        objregis.tpr_other_tumbon = objhistory_regis.tpr_other_tumbon;
                        objregis.tpr_other_zip_code = objhistory_regis.tpr_other_zip_code;
                    }

                    trnoutdepartmentsBindingSource.DataSource = objregis.trn_out_departments;
                    trnpatientorderitemsBindingSource.DataSource = objregis.trn_patient_order_items;
                    trnpatientordersetsBindingSource.DataSource = objregis.trn_patient_order_sets;
                    this.CheckQueueType();
                    CheckPendding();
                    //*** yee กรณีที่Login ด้วย site 2 ให้Default patient Type =Corporate
                    if (Program.CurrentSite.mhs_extra_pe_type == true)
                    {
                        RDPatientType_Corporate.Checked = true;
                    }
                    else
                    {
                        RDPatientType_General.Checked = true;
                    }
                    //***
                    //Load default PreRegister
                    try
                    {

                        var objtmpPatientRegis = (from t1 in dbc.tmp_patient_regis
                                                  where t1.hn_no == Program.Tmp_GetPtarrived.papmi_no
                                                  && t1.status == 'N'
                                                  orderby t1.id descending
                                                  select t1).FirstOrDefault();
                        if (objtmpPatientRegis != null)
                        {
                            Program.SetValueRadioGroup(GBRequestPEBefore, objtmpPatientRegis.req_pe_bef_chkup);
                            Program.SetValueRadioGroup(GBRequestPE, objtmpPatientRegis.req_doctor);
                            if (objregis.tpr_req_doctor != 'N')
                            {
                                if (objtmpPatientRegis.req_doc_gender == null)
                                    Program.SetValueRadioGroup(GBDoctorGender, 'N');
                                else
                                    Program.SetValueRadioGroup(GBDoctorGender, objtmpPatientRegis.req_doc_gender);
                                Program.SetValueRadioGroup(GBRequestDoctor, objtmpPatientRegis.req_inorout_doctor);
                            }

                            //noina
                            Program.SetValueRadioGroup(GBPatienttype, objtmpPatientRegis.patient_type);
                            Program.SetValueRadioGroup(GBAviationType, objtmpPatientRegis.aviation_type);
                            Program.SetValueRadioGroup(GBPEType, objtmpPatientRegis.pe_type);
                            Program.SetValueRadioGroup(GBNPOTime, objtmpPatientRegis.npo_time);
                            Program.SetValueRadioGroup(GBBook, objtmpPatientRegis.send_book);
                            //objregis.tpr_req_doc_code = objtmpPatientRegis.req_doc_code;
                            //objregis.tpr_req_doc_name = objtmpPatientRegis.req_doc_name;
                            objregis.tpr_req_doc_code = DoctorCompleteBox.SelectedValue == null ? null : DoctorCompleteBox.SelectedValue.ToString();
                            objregis.tpr_req_doc_name = DoctorCompleteBox.SelectedValue == null ? null : DoctorCompleteBox.Text;
                            objregis.tpr_npo_text = objtmpPatientRegis.npo_text;
                            objregis.tpr_other_address = objtmpPatientRegis.other_address;
                            objregis.tpr_other_amphur = objtmpPatientRegis.other_amphur;
                            objregis.tpr_other_province = objtmpPatientRegis.other_province;
                            objregis.tpr_other_tumbon = objtmpPatientRegis.other_tumbon;
                            objregis.tpr_other_zip_code = objtmpPatientRegis.other_zip_code;
                            objregis.tpr_send_to = objtmpPatientRegis.send_to;
                            objregis.tpr_remark = objtmpPatientRegis.remark;
                            var objpatientlist = (from t1 in dbc.tmp_patient_regis
                                                  where t1.hn_no == Program.Tmp_GetPtarrived.papmi_no
                                                  && t1.status == 'N'
                                                  orderby t1.id descending
                                                  select t1).FirstOrDefault();
                            foreach (tmp_out_department item in objpatientlist.tmp_out_departments)
                            {
                                trn_out_department newitem = (trn_out_department)trnoutdepartmentsBindingSource.AddNew();
                                newitem.tod_desc = item.description;
                                newitem.tod_location = item.location;
                                newitem.tod_start_date = item.start_date;

                                newitem.tod_create_by = Program.CurrentUser.mut_id;
                                newitem.tod_create_date = Datenow;
                                newitem.tod_update_by = newitem.tod_create_by;
                                newitem.tod_update_date = newitem.tod_create_date;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    this.PatientBindingSource.DataSource = Program.CurrentRegis.trn_patient;
                }

                //Show Patient Alert
                lbdataPatientAlert.Text = "";
                trn_patient ptList = (trn_patient)this.PatientBindingSource.Current;
                foreach (trn_patient_alert ptitem in ptList.trn_patient_alerts)
                {
                    lbdataPatientAlert.Text += "-" + ptitem.tpa_alert + Environment.NewLine;
                }

                if (lbdataPatientAlert.Text.Length > 0)
                {
                    btnPatientAlertView.Visible = true;
                }
                else
                {
                    btnPatientAlertView.Visible = false;
                }
                this.SetComboBoxControl();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadData", ex, false);
            }

            if (Program.CurrentSite.mhs_code == "01HPC3")
            {
                chkVipHpc.Checked = true;
            }
            else
            {
                chkVipHpc.Checked = false;
            }
        }
        private string CheckdMarried(string strMarried)
        {
            if (strMarried != "")
            {//S=Single,M=Married,W=Widowed,D=Divorced,U=Unknown
                if (strMarried.ToString().Contains("Single") == true)
                {
                    return "S,Single";
                }
                else if (strMarried.ToString().Contains("Married") == true)
                {
                    return "M,Married";
                }
                else if (strMarried.ToString().Contains("Widowed") == true)
                {
                    return "W,Widowed";
                }
                else if (strMarried.ToString().Contains("Divorced") == true)
                {
                    return "D,Divorced";
                }
                else if (strMarried.ToString().Contains("Unknown") == true)
                {
                    return "U,Unknown";
                }
                else
                {
                    return ",";
                }
            }
            else
            {
                return ",";
            }
        }
        private string getstring(object bx)
        {
            if (bx != null)
            {
                return bx.ToString();
            }
            else
            {
                return "";
            }
        }

        private void CheckQueueType()
        {
            try
            {
                tmp_getptarrived tmp = Program.Tmp_GetPtarrived;
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    string HN = tmp.papmi_no;
                    mst_check_vvip mst_vvip = cdc.mst_check_vvips.Where(x => x.mvp_hn_no == HN).FirstOrDefault();
                    if (mst_vvip != null)
                    {
                        Program.SetValueRadioGroup(GBQueueType, "1");//VVIP
                    }
                    else
                    {
                        Program.SetValueRadioGroup(GBQueueType, this.patient_type);
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "CheckQueueType", ex, false);
            }
        }
        private void SetComboBoxControl()
        {
            try
            {
                DateTime datenow = Program.GetServerDateTime();
                var mreport = (from t1 in dbc.mst_reports
                               where t1.mrt_status == 'A'
                               && (from t2 in t1.mst_report_matches where t2.mst_report_grp.mrm_id == Program.CurrentRoom.mrm_id select t2).Count() > 0
                               && datenow >= t1.mrt_effective_date.Value
                               && (t1.mrt_expire_date != null ? (datenow <= t1.mrt_expire_date.Value) : true)
                               orderby t1.mrt_report_seq
                               select new { t1.mrt_code, t1.mrt_ename }).ToList();

                CheckConsentForm.DataSource = mreport;
                CheckConsentForm.DisplayMember = "mrt_ename";
                CheckConsentForm.ValueMember = "mrt_code";
                RDNormal_CheckedChanged(null, null);

                var objmst_health_checkup = (from t1 in dbc.mst_health_checkups
                                             where t1.mhc_status == 'A'
                                             && datenow >= t1.mhc_effective_date.Value
                                             && (t1.mhc_expire_date != null ? (datenow <= t1.mhc_expire_date.Value) : true)
                                             select new DropdownData { Code = t1.mhc_id, Name = t1.mhc_ename }).ToList();
                DropdownData newselect = new DropdownData();
                newselect.Code = null;
                newselect.Name = "- Select -";
                objmst_health_checkup.Insert(0, newselect);
                CBHealthCheckUPProgram.DataSource = objmst_health_checkup;
                CBHealthCheckUPProgram.DisplayMember = "Name";
                CBHealthCheckUPProgram.ValueMember = "Code";
                if (objmst_health_checkup.Count > 0)
                {
                    // CBHealthCheckUPProgram.SelectedValue = null;
                    CBHealthCheckUPProgram.SelectedIndex = 0;
                }

                //Doctorcategory
                var listdoctorCat = (from t1 in dbc.mst_doc_categories
                                     where t1.mdc_status == 'A'
                                     && datenow >= t1.mdc_effective_date.Value
                                               && (t1.mdc_expire_date != null ? (datenow <= t1.mdc_expire_date.Value) : true)
                                     orderby t1.mdc_code ascending
                                     select new { Code = t1.mdc_code, Name = t1.mdc_ename }).ToList();
                var listAviationCat = (from t1 in dbc.mst_aviation_categories
                                       where t1.mac_status == 'A'
                                       && datenow >= t1.mac_effective_date.Value
                                                 && (t1.mac_expire_date != null ? (datenow <= t1.mac_expire_date.Value) : true)
                                       orderby t1.mac_ename ascending
                                       select new DropdownData { Code = t1.mac_id, Name = t1.mac_ename }).ToList();
                DropdownData newselect3 = new DropdownData();
                newselect3.Code = null;
                newselect3.Name = "- Select -";
                listAviationCat.Insert(0, newselect3);
                CBAviationCategory.DataSource = listAviationCat;
                CBAviationCategory.ValueMember = "Code";
                CBAviationCategory.DisplayMember = "Name";

                DataTable Tmptable = new DataTable(); DataSet ds = new DataSet("dss");
                Tmptable.Columns.Add("ID"); Tmptable.Columns.Add("Name");
                ds.Tables.Add(Tmptable);
                var item = listBoxMasterCate.Items;
                foreach (var itemselected in listdoctorCat)
                {
                    var row = Tmptable.NewRow();
                    row["ID"] = itemselected.Code;
                    row["Name"] = itemselected.Name;
                    Tmptable.Rows.Add(row);
                }
                for (int i = 0; i <= Tmptable.Rows.Count - 1; i++)
                {
                    item.Add(Tmptable.Rows[i]["Name"].ToString());
                    if (nationcode != "TH")
                    {
                        if (Tmptable.Rows[i]["ID"].ToString() == "MD014")
                        {
                            listBoxCateSelected.Items.Add(Tmptable.Rows[i]["Name"].ToString());
                            listBoxMasterCate.Items.RemoveAt(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetComboBoxControl", ex, false);
            }

        }
        private void CheckPendding()
        {
            LoadPending();
            if (Program.CurrentRegis == null)
            {
                return;
            }
            try
            {
                var objpendding = (from t1 in dbc.trn_patient_pendings
                                   where t1.tpr_id == Program.CurrentRegis.tpr_id
                                   && t1.trn_patient_regi.tpr_status == "PD"
                                   select t1).Count();
                if (objpendding > 0)
                {
                    ChPendingData.Checked = true;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "CheckPendding", ex, false);
            }
        }
        private void LoadPending()
        {
            try
            {
                var objpendding = (from t1 in dbc.trn_patient_pendings
                                   where t1.tpp_status == 'P'
                                   && t1.trn_patient_regi.trn_patient.tpt_hn_no == Program.Tmp_GetPtarrived.papmi_no
                                   orderby t1.trn_patient_regi.tpr_arrive_date descending
                                   select new
                                   {
                                       Date = t1.trn_patient_regi.tpr_arrive_date,
                                       Desc = t1.mst_room_hdr.mrm_ename,
                                       mrm_id = t1.mrm_id
                                   }).Distinct().ToList();
                if (objpendding.Count() > 0)
                {
                    GridPending.DataSource = objpendding;
                    GridPending.Columns["Colmrmid"].Visible = false;
                    for (int i = 0; i <= GridPending.Rows.Count - 1; i++)
                    {
                        GridPending[0, i].Value = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadPending", ex, false);
            }
        }

        private void GridPackage_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                var listpackage = trnpatientordersetsBindingSource.List;
                listpackage.RemoveAt(e.RowIndex);
                //DataGridViewRow dr = GridPackage.CurrentRow;
                //GridPackage.Rows.Remove(dr);
            }
        }
        private void GridOptionsItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 12)
            {
                var listoption = trnpatientorderitemsBindingSource.List;
                listoption.RemoveAt(e.RowIndex);
            }
        }
        private char GetArriveType()
        {
            string strtype = Program.GetValueRadio(GBQueueType);//2345
            if (strtype == "2" || strtype == "4")
            {
                return 'A';
            }
            else
            {
                return 'W';
            }
        }

        private void UpdateProfilepanel()
        {
            var objpatient = (trn_patient)this.PatientBindingSource.Current;
            dataDOB.Text = Program.Tmp_GetPtarrived.papmi_dob.Value.ToString("yyyy-MM-dd");
            if (objpatient.tpt_dob != null)
            {
                dataAge.Text = Program.CalculateAge(objpatient.tpt_dob.Value, Program.GetServerDateTime());
            }
            dataGender.Text = objpatient.tpt_gender == 'M' ? "ชาย(Male)" : "หญิง(Female)";
            dataFullName.Text = objpatient.tpt_othername;

            //Show Patient Alert
            lbdataPatientAlert.Text = "";
            trn_patient ptList = (trn_patient)this.PatientBindingSource.Current;
            foreach (trn_patient_alert ptitem in ptList.trn_patient_alerts)
            {
                if (lbdataPatientAlert.Text == "")
                {
                    lbdataPatientAlert.Text += "-" + ptitem.tpa_alert;
                }
                else
                {
                    lbdataPatientAlert.Text += Environment.NewLine + "-" + ptitem.tpa_alert;
                }

            }
            //*****  แสดงปุ่ม Patient Alert  ************
            if (lbdataPatientAlert.Text.Length > 0)
            {
                btnPatientAlertView.Visible = true;
            }
            else
            {
                btnPatientAlertView.Visible = false;
            }
            //*****************************************

            // Allergy
            lbdataAllergy.Text = ptList.tpt_allergy;
        }

        private void btnOutDepartment_Click(object sender, EventArgs e)
        {
            if (!checkAddOutDepartment())
            {
                return;
            }
            if (DDLocation.SelectedIndex < 1)
            {
                return;
            }
            try
            {

                //trnoutdepartmentsBindingSource.AddNew();
                //trn_out_department newitem =(trn_out_department) trnoutdepartmentsBindingSource.Current;
                trn_out_department newitem = new trn_out_department();

                newitem.tod_desc = txtDescription.Text.Trim();
                newitem.tod_location = DDLocation.Text;
                try
                {
                    newitem.tod_start_date = Convert.ToDateTime(txtTime.Text.Trim());
                }
                catch (Exception)
                {
                    newitem.tod_start_date = null;
                    return;
                }
                newitem.tod_create_by = Program.CurrentUser.mut_id;
                newitem.tod_create_date = Program.GetServerDateTime();
                newitem.tod_update_by = newitem.tod_create_by;
                newitem.tod_update_date = newitem.tod_create_date;

                txtDescription.Text = "";
                DDLocation.Text = "";
                txtTime.Text = "";
                //GenRowNoDridoutDepartment();
                trnoutdepartmentsBindingSource.Add(newitem);
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnOutDepartment_Click", ex, false);
            }
        }
        private void btnAddPatientAlert_Click(object sender, EventArgs e)
        {
            GridPatientAlertSearch.Visible = false;
            if (txtPatientAlert.Text.Trim().Length == 0)
            {
                return;
            }

            var objregcurrent = (trn_patient_regi)this.PatientRegisBindingSource.Current;
            trn_patient_alert newitem = (trn_patient_alert)this.PatientAlertBindingSource.AddNew();
            newitem.tpt_id = objregcurrent.tpt_id;
            newitem.mut_id = Program.CurrentUser.mut_id;
            newitem.tpa_alert = txtPatientAlert.Text.Trim();
            newitem.tpa_status = 'A';
            newitem.tpa_create_by = Program.CurrentUser.mut_username;
            newitem.tpa_create_date = Program.GetServerDateTime();
            newitem.tpa_update_by = Program.CurrentUser.mut_username;
            newitem.tpa_update_date = newitem.tpa_create_date;
            txtPatientAlert.Text = "";
        }
        private bool checkAddOutDepartment()
        {
            DateTime dnow;
            bool istime = false;
            try
            {
                dnow = Convert.ToDateTime(txtTime.Text.Trim());
                istime = true;
            }
            catch (Exception)
            {
            }
            lbAlertMsg.Text = "";
            if (txtDescription.Text.Trim() == "" || DDLocation.Text == "")
            {
                lbAlertMsg.Text = "Please input data require.";
                return false;
            }
            if (istime == false)
            {
                lbAlertMsg.Text = "Input time : Incorrect Ex. 13:30";
                return false;
            }
            return true;
        }

        private void GetAllergyByHN(String HN)
        {
            try
            {
                lbdataAllergy.Text = Program.Tmp_GetPtarrived.allergy_eng;
                return;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "GetAllergyByHN", ex, false);
            }
        }

        private void RDPatientType_Corporate_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Corporate.Checked)
            {
                lbEmpCode.Visible = true;
                txtEmployeeID.Visible = true;

                chOfficeAddress.Enabled = true;
                //gpCompany.Enabled = true;
                //DDcompany.Enabled = true;
                rdWaitPE.Checked = false; // morn : default pe type is null for checked Corporate
                rdNotWaitPE.Checked = false;
            }
            else
            {
                txtEmployeeID.Text = "";
                txtEmployeeID.Enabled = false;
                lbEmpCode.Enabled = false;
                txtEmployeeID.Visible = false;
                chOfficeAddress.Checked = false;
                chOfficeAddress.Enabled = false;
                //gpCompany.Enabled = false;
                //DDcompany.Enabled = false;
            }
            RDBookwantResult_CheckedChanged(null, null);
        }
        private void RDrequestPE_CheckedChanged(object sender, EventArgs e)
        {
            //Request PE = No
            if (RDrequestPE.Checked == true)
            {
                DoctorCompleteBox.Enabled = false;
                DoctorCompleteBox.SelectedValue = null;
                GBRequestDoctor.Enabled = false;
                GBDoctorGender.Enabled = false;
                RDRequestDoctorInDepart.Checked = false;
                RDRequestoutLet.Checked = false;
                RDDoctorMan.Checked = false;
                RDNA.Checked = false;
                RDDoctorFemale.Checked = false;
                //mornAuto txtDoctorName.Enabled = false;
                //mornAuto txtDoctorName.Text = String.Empty;
            }
            else
            {
                DoctorCompleteBox.Enabled = true;
                GBRequestDoctor.Enabled = true;
                RDRequestDoctorInDepart.Checked = true;
                //RDDoctorMan.Checked = true;
                RDNA.Checked = true;
                GBDoctorGender.Enabled = true;
                //mornAuto txtDoctorName.Enabled = true;
                //mornAuto txtDoctorName.Text = String.Empty;
            }
        }
        //ปุ่ม  Register

        private void btnRegister_Click(object sender, EventArgs e)
        {
            EmrClass.GetDataMasterCls mst = new EmrClass.GetDataMasterCls();
            this.btnRegister.Enabled = false;
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show();
            Application.DoEvents();

            //Avation Type Value
            label18.ForeColor = Color.Black;
            if (Program.GetValueRadioTochar(GBPatienttype) == '2' || Program.GetValueRadioTochar(GBPatienttype) == '4')
            {
                //var AvationtypeValue = Program.GetValueRadioTochar(GBAviationType);
                //if (AvationtypeValue == null || CBAviationCategory.SelectedValue == null)
                //{
                //    if (CBAviationCategory.SelectedValue == null)
                //    {
                //        lbAlertMsg.Text = "Avation Category must have value!";
                //    }
                //    if (AvationtypeValue == null)
                //    {
                //        lbAlertMsg.Text = "Avation type must have value!";
                //    }
                //    lbAlertMsg.Focus();
                //    Application.DoEvents();
                //    label18.ForeColor = Color.Red;
                //    frmbg.Close();
                //    return;
                //}
            }

            //check NEO Time Correct
            TimeSpan interval;
            bool isconvert = true;
            try
            {
                if (txtnpotimeRemark.Text.Trim() == ":")
                {
                    interval = TimeSpan.Parse("00:00");
                }
                else
                {
                    interval = TimeSpan.Parse(txtnpotimeRemark.Text.Trim());
                }
            }
            catch (FormatException)
            {
                isconvert = true;
            }
            catch (OverflowException)
            {
                isconvert = false;
            }

            //Noina Modify 03/12/2013
            if (RDrequestPE_yes.Checked == true) //ไม่ได้เลือก No แสดงว่าเลือก Yes อยู่
            {
                if (RDRequestoutLet.Checked)
                {
                    if (DoctorCompleteBox.SelectedValue == null)
                    {
                        lbAlertMsg.Text = "Please required specify a doctor name.";
                        this.btnRegister.Enabled = true;
                        frmbg.Close();
                        lbAlertMsg.Focus();
                        return;
                    }
                }
                else if (RDRequestDoctorInDepart.Checked)
                {
                    if (DoctorCompleteBox.SelectedValue == null && !RDDoctorMan.Checked && !RDDoctorFemale.Checked)
                    {
                        lbAlertMsg.Text = "Please required specify a doctor's name or gender of doctor.";
                        this.btnRegister.Enabled = true;
                        frmbg.Close();
                        lbAlertMsg.Focus();
                        return;
                    }
                }
            }

            if (RDnpotimeYes.Checked && (txtnpotimeRemark.Text.Trim().Length < 5 || isconvert == false))
            {
                lbAlertMsg.Text = "NPO Time incorrect.";
                lbAlertMsg.Focus();
                frmbg.Close();
                this.btnRegister.Enabled = true;
                return;
            }
            if (RDPatientType_Aviation.Checked == true && g2.Rows.Count == 0)
            {
                lbAlertMsg.Text = "Please select Aviation Category";
                lbAlertMsg.Focus();
                frmbg.Close();
                this.btnRegister.Enabled = true;
                return;
            }
            //Save Data 
            try
            {
                Boolean saveIsCompleted = false;
                if (dbc.Connection.State == ConnectionState.Open)
                    dbc.Connection.Close();
                dbc.Connection.Open();
                DbTransaction trans = dbc.Connection.BeginTransaction();
                dbc.Transaction = trans;

                DateTime datenowvalue = Program.GetServerDateTime();
                var objregis = (trn_patient_regi)this.PatientRegisBindingSource.Current;
                var objpatient = (trn_patient)this.PatientBindingSource.Current;
                var QueueTypevalue = Program.GetValueRadio(GBQueueType);

                DateTime dtx = DateTime.Parse(Program.Tmp_GetPtarrived.paadm_admdate.Value.ToString("yyyy-MM-dd") + " " + Program.Tmp_GetPtarrived.paadm_admtime);
                objregis.tpr_type = 'D';
                objregis.tpr_print_book = "N";
                objregis.tpr_appointment_date = dtx;
                objregis.tpr_arrive_date = DateTime.Parse(Program.Tmp_GetPtarrived.paadm_admdate.Value.ToString("yyyy-MM-dd") + " " + Program.Tmp_GetPtarrived.appt_arrivaltime);
                objregis.tpr_arrive_type = this.GetArriveType();
                objregis.tpr_vip_code = Program.Tmp_GetPtarrived.penstype_code;
                objregis.tpr_vip_desc = Program.Tmp_GetPtarrived.penstype_desc;
                if (CBAviationCategory.Enabled == true && CBAviationCategory.SelectedValue != null)
                {
                    objregis.mac_id = Program.GetValueComboBoxInt(CBAviationCategory);
                }
                else
                {
                    objregis.mac_id = null;
                }

                if (CBDoctorCategory.Enabled == true && CBDoctorCategory.SelectedValue != null)
                {
                    objregis.mdc_id = Program.GetValueComboBoxInt(CBDoctorCategory);
                }
                else
                {
                    objregis.mdc_id = null;
                }

                if (mhcid != 0)
                {
                    objregis.mhc_id = mhcid;
                    objregis.tpr_mhc_ename = strSelected;
                }
                if (mhcid == 0)
                {
                    objregis.mhc_id = null;
                    objregis.tpr_mhc_ename = txtHealthCheckup.Text;
                }

                TimeSpan dtarrive = TimeSpan.Parse(Program.Tmp_GetPtarrived.appt_arrivaltime);
                TimeSpan dtappoint = TimeSpan.Parse(Program.Tmp_GetPtarrived.paadm_admtime);
                TimeSpan dx = dtarrive.Subtract(dtappoint);

                if (dx.TotalMinutes > Program.GetLimitTime("WTM"))
                    objregis.tpr_appoint_type = 'L';
                else
                    objregis.tpr_appoint_type = 'T';

                objregis.tpr_queue_no = Program.GenQueueNo(QueueTypevalue);//Gen Queue
                objregis.tpr_queue_type = Program.GetValueRadioTochar(GBQueueType);
                objregis.tpr_patient_type = Program.GetValueRadioTochar(GBPatienttype);
                objregis.tpr_req_pe_bef_chkup = Program.GetValueRadioTochar(GBRequestPEBefore);
                objregis.tpr_req_doctor = Program.GetValueRadioTochar(GBRequestPE);

                if (objregis.tpr_req_doctor == 'N')
                {
                    objregis.tpr_req_doc_gender = null;
                    objregis.tpr_req_inorout_doctor = string.Empty;
                    objregis.tpr_req_doc_name = string.Empty;
                }
                else
                {
                    if (Program.GetValueRadioTochar(GBDoctorGender) != null)
                        if (Program.GetValueRadioTochar(GBDoctorGender) != 'N')
                            objregis.tpr_req_doc_gender = Program.GetValueRadioTochar(GBDoctorGender);
                    objregis.tpr_req_inorout_doctor = Program.GetValueRadio(GBRequestDoctor);
                }


                objregis.tpr_pe_type = Program.GetValueRadioTochar(GBPEType);
                objregis.tpr_npo_time = Program.GetValueRadioTochar(GBNPOTime);
                objregis.tpr_send_book = Program.GetValueRadioTochar(GBBook);
                objregis.tpr_send_to = Program.GetValueRadioTochar(panelBookSendTo);
                objregis.tpr_create_date = datenowvalue;
                objregis.tpr_create_by = Program.CurrentUser.mut_username;
                objregis.tpr_update_date = objregis.tpr_create_date;
                objregis.tpr_update_by = objregis.tpr_create_by;
                objregis.tpr_check_pending = (ChPendingData.Checked) ? 'Y' : 'N';// check Pending
                objregis.tpr_pe_site2 = Program.GetValueRadioTochar(pnSite2);
                objregis.tpr_req_same_doc = chkReqSameDR.Checked;

                //morn new field for company
                EmrClass.GetDataToDoListCls tCls = new EmrClass.GetDataToDoListCls();
                if (DDcompany.SelectedValue != null)
                {
                    trn_company_detail tcd = tCls.getCompanyDetail(DDcompany.SelectedValue.ToString());
                    if (tcd != null)
                    {
                        objregis.tcd_id = tcd.tcd_id;
                        objregis.tpr_company_code = tcd.tcd_code;
                        objregis.tpr_comp_tdesc = tcd.tcd_tname;
                        objregis.tpr_comp_edesc = tcd.tcd_ename;
                        objregis.tpr_comp_dep_tdesc = DDDepartment.Text;
                    }
                }
                //end morn

                objpatient.tpt_vip_hpc = (chkVipHpc.Checked) ? true : false;//Added.Akkaradech on 2013-12-24{viphpc}

                trn_patient_regi currentPatientRegi = (trn_patient_regi)PatientRegisBindingSource.Current;

                //GetDataFromWSTrakCare getDataWS = new GetDataFromWSTrakCare();
                //getDataWS.genPatientPlan(ref currentPatientRegi, Program.CurrentSite.mhs_id, Program.GetServerDateTime());

                EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                PackageCls.skipReqDoctorOutDepartment(ref currentPatientRegi);
                PackageCls.CompleteEcho(ref currentPatientRegi);
                PackageCls.skipChangeEstToEcho(ref currentPatientRegi, currentPatientRegi.mhs_id);
                PackageCls.checkOrderPMR(ref currentPatientRegi, currentPatientRegi.mhs_id);

                StatusTransaction getPTCate = getPatientCate(ref currentPatientRegi);
                StatusTransaction getAviaType = getAviationCate(ref currentPatientRegi);

                //Select Pending Grid && ChPendingData is checked
                if (ChPendingData.Checked)
                {
                    foreach (DataGridViewRow dr in GridPending.Rows)
                    {
                        if (Convert1.ToBoolean(dr.Cells[0].Value) == true)
                        {
                            int pmrmid = Convert1.ToInt32(dr.Cells["Colmrmid"].Value);
                            var objseventList = (from t1 in dbc.mst_room_events
                                                 from rm in dbc.mst_events
                                                 where t1.mvt_id == rm.mvt_id
                                                 && t1.mrm_id == pmrmid
                                                 && rm.mvt_type_cate == 'M'
                                                 select t1).FirstOrDefault();
                            //check ว่ามี mvt id นี้ใน plan หรือไม่ถ้ามีแล้วไม่ต้องเพิ่ม
                            int countmvt = currentPatientRegi.trn_patient_plans.Where(x => x.mvt_id == objseventList.mvt_id).Count();
                            if (countmvt == 0)
                            {
                                trn_patient_plan newtpp = new trn_patient_plan();
                                newtpp.mvt_id = objseventList.mvt_id;
                                newtpp.tpl_status = 'N';
                                newtpp.tpl_by = 'A';
                                newtpp.tpl_new = false;
                                newtpp.tpl_create_by = Program.CurrentUser.mut_username;
                                newtpp.tpl_create_date = datenowvalue;
                                currentPatientRegi.trn_patient_plans.Add(newtpp);
                            }
                            // update trn_patient_pending = 'C' morn
                            var result = objpatient.trn_patient_regis.Select(x => x.trn_patient_pendings.Where(y => y.mrm_id == pmrmid)).ToList();
                            result.ForEach(x => x.ToList().ForEach(y => y.tpp_status = 'C'));
                            //
                        }
                    }
                }

                //Update Table tmp_getPtarrived Flag_Success='Y'
                tmp_getptarrived currenttmp = (from t1 in dbc.tmp_getptarriveds
                                               where t1.appt_rowid == Program.Tmp_GetPtarrived.appt_rowid
                                               select t1).FirstOrDefault();
                if (currenttmp != null)
                {
                    currenttmp.flag_success = 'Y';
                }

                #region RunEst and MarryPackage

                /*int mvt_ES = dbc.mst_events.Where(x => x.mvt_code == "ES").Select(x => x.mvt_id).FirstOrDefault();
                int mvt_EC = dbc.mst_events.Where(x => x.mvt_code == "EC").Select(x => x.mvt_id).FirstOrDefault();
                int ct_ESTECHO = currentPatientRegi.trn_patient_plans.Where(x => (x.mvt_id == mvt_ES || x.mvt_id == mvt_EC)).Count();*/

                //if (currentPatientRegi.tpr_req_doctor == 'Y' && currentPatientRegi.tpr_req_inorout_doctor == "UT")// && ct_ESTECHO == 0)
                //{
                //    int mvt_pe = dbc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                //    List<trn_patient_plan> planPE = currentPatientRegi.trn_patient_plans.Where(x => x.mvt_id == mvt_pe).ToList();
                //    planPE.ForEach(x => currentPatientRegi.trn_patient_plans.Remove(x));
                //}

                //////RunSkipEst
                //SkipOrderSetEST.RunSkipEST(Program.CurrentSite.mhs_id, ref currentPatientRegi);
                //////RunSkipEst 
                //////marry package
                //Class.Pmr runprm = new Pmr();
                //runprm.RunPRM(Program.CurrentSite.mhs_id, ref currentPatientRegi);
                ////marry package

                // For HPC Site 2
                //int ct_PE = (from t in dbc.trn_patient_plans

                //if (Program.CurrentSite.mhs_code == "01HPC2")
                //{
                //    if (currentPatientRegi.tpr_pe_site2 == 'P')
                //    {
                //        int mvt_pe = dbc.mst_events.Where(x => x.mvt_code == "PE").Select(x => x.mvt_id).FirstOrDefault();
                //        List<trn_patient_plan> planPE = currentPatientRegi.trn_patient_plans.Where(x => x.mvt_id == mvt_pe).ToList();
                //        planPE.ForEach(x => currentPatientRegi.trn_patient_plans.Remove(x));
                //    }
                //    else if (currentPatientRegi.tpr_pe_site2 == 'N')
                //    {
                //        mst_event mvt = mst.getMstEvent("PE");
                //        List<trn_patient_plan> planPE = currentPatientRegi.trn_patient_plans.Where(x => x.mvt_id == mvt.mvt_id).ToList();
                //        if (planPE == null || planPE.Count == 0)
                //        {
                //            trn_patient_plan newPlanPE = new trn_patient_plan
                //            {
                //                mvt_id = mvt.mvt_id,
                //                tpl_use_pac = false,
                //                tpl_status = 'N',
                //                tpl_by = 'A',
                //                tpl_new = false
                //            };
                //            currentPatientRegi.trn_patient_plans.Add(newPlanPE);
                //        }
                //        //เพิ่ม PE Station ใน trn_patient_plan ถ้ายังไม่มี PE Station
                //    }
                //}
                #endregion


                try
                {
                    PatientRegisBindingSource.EndEdit(); // Patient Register

                    trnoutdepartmentsBindingSource.EndEdit(); //  Out Department
                    PatientAlertBindingSource.EndEdit();// Patient Alert
                    //trnpatientorderitemsBindingSource.EndEdit(); // Patient Order item
                    //trnpatientordersetsBindingSource.EndEdit(); //Patient order Set
                    PatientBindingSource.EndEdit();

                    try
                    {
                        dbc.SubmitChanges();
                    }
                    catch (System.Data.Linq.ChangeConflictException)
                    {
                        foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                        {
                            dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                        }
                        dbc.SubmitChanges();
                    }
                    new EmrClass.GetPTPackageCls().setRelationOrderSet(ref objregis);
                 //   StatusTransaction stamp_main_tpr_id = new EmrClass.GetDataFromWSTrakCare().stampMain_tpr_id(ref objregis);
                    try
                    {
                        dbc.SubmitChanges();
                    }
                    catch (System.Data.Linq.ChangeConflictException)
                    {
                        foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                        {
                            dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                        }
                        dbc.SubmitChanges();
                    }
                    dbc.Transaction.Commit();

                    saveIsCompleted = true;
                    Iscompleted = true;
                }
                catch (Exception ex)
                {
                    dbc.Transaction.Rollback();
                    Program.MessageError(this.Name, "btnRegister_Click", ex, false);
                }
                finally
                {
                    dbc.Connection.Close();
                }

                //#region morn update tpr_main_id = tpr_id when register
                //if (saveIsCompleted == true)
                //{
                //    if (currentPatientRegi.tpr_id != null || currentPatientRegi.tpr_id != 0)
                //    {
                //        currentPatientRegi.tpr_main_id = currentPatientRegi.tpr_id;
                //        //using (CheckupDataContext dcUpdateMainTpr = new CheckupDataContext(Program.Connectionstring))
                //        //{
                //        //    var tpr = dcUpdateMainTpr.trn_patient_regis.Where(x => x.tpr_id == tpr_id).FirstOrDefault();
                //        //    if (tpr != null)
                //        //    {
                //        //        tpr.tpr_main_id = tpr.tpr_id;
                //        //        tpridInAviation = tpr.tpr_id;
                //        //        dcUpdateMainTpr.SubmitChanges();
                //        //    }
                //        //}
                //        if (dbc.Connection.State == ConnectionState.Closed) dbc.Connection.Open();
                //        List<trn_patient_aviation> tpa = SaveAivation(tpridInAviation);
                //        if (tpa != null) currentPatientRegi.trn_patient_aviations.AddRange(tpa);
                //        List<trn_patient_cat> tpc = SaveDoctorCategory(tpridInAviation);
                //        if (tpc != null) currentPatientRegi.trn_patient_cats.AddRange(tpc);
                //        try
                //        {
                //            dbc.SubmitChanges();
                //        }
                //        catch (System.Data.Linq.ChangeConflictException)
                //        {
                //            foreach (System.Data.Linq.ObjectChangeConflict occ in dbc.ChangeConflicts)
                //            {
                //                dbc.Refresh(System.Data.Linq.RefreshMode.KeepChanges, occ.Object);
                //            }
                //            dbc.SubmitChanges();
                //        }

                //        Clear();
                //        CheckListAviation.Items.Clear();
                //    }
                //}
                //#endregion morn update tpr_main_id = tpr_id when register


                if (saveIsCompleted == true)
                    Program.CurrentRegis = (trn_patient_regi)PatientRegisBindingSource.Current;
                {

                    this.PatientBindingSource.DataSource = Program.CurrentRegis.trn_patient;
                    UpdateProfilepanel();


                    btnRegister.Enabled = false;
                    btnprintslip.Enabled = true;
                    btnPrintConsenform.Enabled = true;
                    panelDisableEdit.Enabled = false;  // morn : use panal for disable controls when register complete

                    if (RDPatientType_Corporate.Checked)
                    {
                        btnCOFrom.Enabled = true;
                    }

                    try
                    {
                        CallQueue.SetUpdateTextfile(objpatient.tpt_hn_no, Program.CurrentRegis.tpr_id);
                    }
                    catch (Exception)
                    {

                    }

                    try
                    {
                        string strHN = Program.CurrentRegis.trn_patient.tpt_hn_no;
                        //CheckUpLabClass.ws_Getcheckuplab_Async(strHN, Program.CurrentRegis.tpr_id);
                        using (Service.WS_CheckupCls ws = new Service.WS_CheckupCls())
                        {
                            ws.retrieveLabToPatientLabBackground(Program.CurrentRegis.tpr_id);
                        }
                    }
                    catch (Exception ex)
                    {
                        Program.MessageError(this.Name, "btnRegister_Click", ex, false);
                    }
                    string messege = "";
                    StatusTransaction result = new Class.SendQueue().SendToBasic(Program.CurrentRegis.tpr_id, Program.CurrentRoom.mrm_id, ref messege);
                    lbAlertMsg.Text = messege;


                    StatusTransaction calBasic = new Class.FunctionDataCls().calSendBasic();
                    if (calBasic == StatusTransaction.Error)
                    {

                    }

                    try
                    {
                        new Class.logPatientFlowCls(logPatientFlowCls.sendType.Regis,
                                                    Program.CurrentRegis.tpr_id,
                                                    0,
                                                    Program.CurrentSite.mhs_id,
                                                    Program.CurrentRoom.mrd_ename,
                                                    Program.CurrentUser.mut_username);
                    }
                    catch
                    {

                    }

                    btnprintslip_Click(null, null);
                    btnprintslip.Enabled = true;

                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnRegister_Click", ex, false);
            }
            //ปิดหน้าจอมืดๆๆ


            //add suriya 17/12/2014
            //try
            //{
            //    string serviceURL = ConfigurationManager.AppSettings["EBookServiceURLRegis"];
            //    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(string.Format("{0}", serviceURL)));
            //    request.Method = "POST";
            //    request.ContentType = "application/json";

            //    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            //    {
            //        var tprID = string.Format("('tpr_id':'{0}')", Program.CurrentRegis.tpr_id.ToString());
            //        tprID = tprID.Replace('(', '{');
            //        tprID = tprID.Replace(')', '}');

            //        var x = new InterfaceRequest { tpr_id = Convert.ToInt32(Program.CurrentRegis.tpr_id.ToString()) };
            //        var ccc = Newtonsoft.Json.JsonConvert.SerializeObject(x);

            //        streamWriter.Write(ccc);
            //        streamWriter.Flush();
            //    }

            //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //    using (var streanReader = new StreamReader(response.GetResponseStream()))
            //    {
            //        var result = streanReader.ReadToEnd();

            //        if (result.Contains("Code") && result.Contains("0000"))
            //        {
            //            //MessageBox.Show("Success.");
            //        }
            //        else
            //            throw new Exception("Send interface failed.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Program.MessageError(this.Name, "btnRegister_Click", ex, false);
            //}
            //end suriya 17/12/2014

            if (Program.CurrentRegis != null)
            {
                if (new Class.FunctionDataCls().CheckPatientPackage(Program.CurrentRegis.tpr_id))
                {
                    new Class.FunctionDataCls().dispense_doctor_by_point(Program.CurrentRegis.tpr_id);
                }
            }
            frmbg.Close();
        }
        public class InterfaceRequest//add suriya 17/12/2014
        {
            public int tpr_id { get; set; }
        }

        private void GridPatientAlert_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var listoption = PatientAlertBindingSource.List;
                listoption.RemoveAt(e.RowIndex);
            }
            // MessageBox.Show(e.ColumnIndex.ToString());
        }

        private List<tmpDoctorTable> DoctorList = new List<tmpDoctorTable>();
        //private void SearchGetDoctor(string strSearch)
        //{
        //    if (RDRequestDoctorInDepart.Checked == true)
        //    {
        //        var objlistMstUserType = (from t1 in dbc.mst_user_types
        //                                  where t1.mut_type == 'D'
        //                                  && t1.mut_out_checkup == false
        //                                  && t1.mut_fullname.Contains(strSearch)
        //                                  select new { DoctorName = t1.mut_fullname, DoctorCode = t1.mut_username }).ToList();
        //        GridDoctorName.DataSource = objlistMstUserType;
        //        GridDoctorName.Columns[1].Visible = false;
        //        GridDoctorName.Columns[0].HeaderText = "Doctor Name";
        //        GridDoctorName.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //        if (objlistMstUserType.Count() > 0)
        //        {
        //            GridDoctorName.Visible = true;
        //        }
        //    }
        //    else
        //    {
        //        if (UseWebservice == false) { return; }
        //        try
        //        {

        //            if (strSearch.Length >= 2)
        //            {
        //                GridDoctorName.Visible = true;
        //                Ws_GetDataByTrak.WS_GetDataBytrakSoapClient ws = new Ws_GetDataByTrak.WS_GetDataBytrakSoapClient();
        //                Application.DoEvents();
        //                DataTable dt = ws.GetCareprovider(strSearch);

        //                GridDoctorName.DataSource = dt;

        //                GridDoctorName.Columns[0].Visible = false;
        //                GridDoctorName.Columns[1].Visible = false;
        //                GridDoctorName.Columns[2].Visible = false;
        //                GridDoctorName.Columns[3].Visible = false;

        //                GridDoctorName.Columns[4].Visible = true;
        //                GridDoctorName.Columns[4].HeaderText = "Doctor Name";
        //                GridDoctorName.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        //                GridDoctorName.Visible = true;
        //            }
        //            else if (strSearch.Length >= 2)
        //            {
        //                GridDoctorName.Visible = true;
        //                var datasourc = DoctorList.Where(c => c.CTPCP_Desc.Contains(strSearch));
        //                GridDoctorName.DataSource = datasourc;
        //                Application.DoEvents();
        //            }
        //            else
        //            {
        //                GridDoctorName.Visible = false;
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            // Program.MessageError("=>SearchGetDoctor :" + ex.Message);
        //        }
        //    }

        //}
        //private void txtDoctorName_KeyUp(object sender, KeyEventArgs e)
        //{
        //    GridDoctorName.Top = txtDoctorName.Top + 24;
        //    GridDoctorName.Left = txtDoctorName.Left ;
        //    GridDoctorName.Width = 465;
        //    GridDoctorName.Height = 150;
        //    GridDoctorName.BringToFront();
        //    SearchGetDoctor(txtDoctorName.Text.Trim());
        //}
        //private void GridDoctorName_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    try
        //    {
        //        var selectedItems = GridDoctorName.CurrentRow;
        //        trn_patient_regi objcurrenttpr = (trn_patient_regi)PatientRegisBindingSource.Current;
        //        if (RDRequestDoctorInDepart.Checked == true)
        //        {
        //            objcurrenttpr.tpr_req_doc_code = selectedItems.Cells[1].Value.ToString();
        //            objcurrenttpr.tpr_req_doc_name = selectedItems.Cells[0].Value.ToString();
        //        }
        //        else
        //        {
        //            objcurrenttpr.tpr_req_doc_code = selectedItems.Cells[3].Value.ToString();
        //            objcurrenttpr.tpr_req_doc_name = selectedItems.Cells[4].Value.ToString();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    GridDoctorName.Visible = false;
        //}
        private void txtDoctorName_MouseClick(object sender, MouseEventArgs e)
        {
            //GridDoctorName.Visible = true;
        }

        private void clearAviationType()
        {
            ListAviationFinalSelect.Items.Clear();
            ListAviationSelect.Items.Clear();
            g1.Rows.Clear();
            g2.Rows.Clear();
            foreach (int i in CheckListAviation.CheckedIndices)
            {
                CheckListAviation.SetItemCheckState(i, CheckState.Unchecked);
            }
        }
        private void RDPatientType_Aviation_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            if (rd.Checked)// || RDPatientType_AviationAircrew.Checked)
            {
                //noina
                loadAviationTodolist();

                CBAviationCategory.Enabled = true;
                RDAviationCategoryNewcase.Enabled = true;
                RDaviationTypeFollowup.Enabled = true;

                ////Added:Akkaradech
                CheckListAviation.Enabled = true;
                ListAviationSelect.Enabled = true;
                btnselectFinal.Enabled = true;
                ListAviationFinalSelect.Enabled = true;
                btnClear.Enabled = true;
                btnmove.Enabled = true;
                DateTime datenow = Program.GetServerDateTime();
                var listAviationCat = (from t1 in dbc.mst_aviation_categories
                                       where t1.mac_status == 'A'
                                       && datenow >= t1.mac_effective_date.Value
                                                 && (t1.mac_expire_date != null ? (datenow <= t1.mac_expire_date.Value) : true)
                                       select t1).ToList();

                //DataTable Tmptable = new DataTable(); DataSet ds = new DataSet("dss");
                //Tmptable.Columns.Add("ID"); Tmptable.Columns.Add("Name");
                //ds.Tables.Add(Tmptable);
                var item = CheckListAviation.Items;

                foreach (var itemlist in listAviationCat)
                {
                    item.Add(itemlist.mac_ename);
                    //var row = Tmptable.NewRow();
                    //row["ID"] = itemlist.mac_id;
                    //row["Name"] = itemlist.mac_ename;
                    //Tmptable.Rows.Add(row);
                }
                //for (int i = 0; i <= Tmptable.Rows.Count - 1; i++)
                //    item.Add(Tmptable.Rows[i][1].ToString());

            }
            else
            {
                RDAviationCategoryNewcase.Checked = false;
                RDaviationTypeFollowup.Checked = false;
                RDAviationCategoryNewcase.Enabled = false;
                RDaviationTypeFollowup.Enabled = false;
                CBAviationCategory.Enabled = false;
                CheckListAviation.Enabled = false;
                ListAviationSelect.Enabled = false;
                btnselectFinal.Enabled = false;
                ListAviationFinalSelect.Enabled = false;
                btnClear.Enabled = false;
                btnmove.Enabled = false;
                CheckListAviation.Items.Clear();
                DDcompany.SelectedValue = 0;
                clearAviationType();
            }
        }
        private void btnloadPackageTK_Click(object sender, EventArgs e)
        {
            try
            {
                trn_patient_regi tpr = (trn_patient_regi)PatientRegisBindingSource.Current;

                EmrClass.GetDataFromWSTrakCare getWs = new EmrClass.GetDataFromWSTrakCare();
                DateTime dateNow = Program.GetServerDateTime();

                int enRowID = Convert.ToInt32(tpr.tpr_en_rowid);
                EmrClass.GetPTPackageCls PackageCls = new EmrClass.GetPTPackageCls();
                EnumerableRowCollection<DataRow> getPTPackage = PackageCls.GetPTPackage(enRowID);
                PackageCls.AddPatientOrderItem(ref tpr, "System", dateNow, getPTPackage);
                PackageCls.AddPatientOrderSet(ref tpr, "System", dateNow, getPTPackage);
                List<MapOrderEvent> mapOrder = PackageCls.MapEvent(getPTPackage);
                PackageCls.AddPatientEvent(ref tpr, "System", dateNow, mapOrder);
                PackageCls.AddPatientPlan(ref tpr, "System", dateNow, mapOrder);

                //StatusTransaction getPackage = getWs.GetPatientPackage(ref tpr, dateNow);
                //if (getPackage == StatusTransaction.True)
                //{

                //}
                //else
                //{

                //}

                if (tpr.trn_patient_order_sets.Count > 0)
                {
                    string setName = tpr.trn_patient_order_sets.Where(x => x.tos_status == true).Select(x => x.tos_od_set_name).FirstOrDefault();
                    txtHealthCheckup.Text = setName;
                }
                GridPackage.DataSource = tpr.trn_patient_order_sets
                                            .Where(x => x.tos_status == true)
                                            .Select(x => new
                                            {
                                                tpr_id = x.tpr_id,
                                                tos_od_set_name = x.tos_od_set_name
                                            }).ToList();
            }
            catch
            {

            }
        }
        private void GridPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridPending.Rows.Count > 0)
            {
                try
                {
                    if ((bool)GridPending[0, e.RowIndex].Value == true)
                    {
                        GridPending[0, e.RowIndex].Value = false;
                    }
                    else
                    {
                        GridPending[0, e.RowIndex].Value = true;
                    }
                }
                catch (Exception)
                {
                    //throw;
                }
            }
        }

        private void chsameMainAddress_CheckedChanged(object sender, EventArgs e)
        {
            trn_patient_regi objregis = (trn_patient_regi)PatientRegisBindingSource.Current;
            if (chsameMainAddress.Checked)
            {
                objregis.tpr_other_name = dataFullName.Text;
                objregis.tpr_other_address = objregis.tpr_main_address;
                objregis.tpr_other_province = objregis.tpr_main_province;
                objregis.tpr_other_amphur = objregis.tpr_main_amphur;
                objregis.tpr_other_tumbon = objregis.tpr_main_tumbon;
                objregis.tpr_other_zip_code = objregis.tpr_main_zip_code;
                chOfficeAddress.Checked = false;
            }
            else
            {
                objregis.tpr_other_name = "";
                objregis.tpr_other_address = "";
                objregis.tpr_other_province = "";
                objregis.tpr_other_amphur = "";
                objregis.tpr_other_tumbon = "";
                objregis.tpr_other_zip_code = "";
            }
        }
        private void chOfficeAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chOfficeAddress.Checked)
            {
                trn_patient_regi objregis = (trn_patient_regi)PatientRegisBindingSource.Current;
                objregis.tpr_other_address = "";
                objregis.tpr_other_province = "";
                objregis.tpr_other_amphur = "";
                objregis.tpr_other_tumbon = "";
                objregis.tpr_other_zip_code = "";
                chsameMainAddress.Checked = false;
            }
        }

        private void RDBookwantResult_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Corporate.Checked)
            {
                panelBookSendTo.Enabled = true;
                if (RDBookwantResult.Checked)
                {
                    panelBookSendTo.Visible = true;
                    RDBookSendToPN.Checked = true;
                }
                else
                {
                    panelBookSendTo.Visible = false;
                }
            }
            else
            {
                panelBookSendTo.Enabled = false;
                if (RDBookwantResult.Checked)
                {
                    panelBookSendTo.Visible = true;
                    RDBookSendToPN.Checked = true;
                }
                else
                {
                    panelBookSendTo.Visible = false;
                }
            }
        }
        private void RDRequestoutLet_CheckedChanged(object sender, EventArgs e)
        {
            //GBRequestPEBefore.Enabled = !RDRequestoutLet.Checked;
            //DoctorCompleteBox.SelectedValue = null;
            //if (RDRequestoutLet.Checked)
            //{
            //    RDNA.Checked = true;
            //    RDNA.AutoCheck = false;
            //    RDDoctorMan.AutoCheck = false;
            //    RDDoctorFemale.AutoCheck = false;
            //}
            //else
            //{
            //    RDNA.AutoCheck = true;
            //    RDDoctorMan.AutoCheck = true;
            //    RDDoctorFemale.AutoCheck = true;
            //} 
            //setDataSourceDoctor();
            //if (RDRequestoutLet.Checked == false)
            //{
            //    //GBDoctorGender.Enabled = true;
            //}
            //else
            //{
            //    //GBDoctorGender.Enabled = false;
            //    RDDoctorFemale.Checked = false;
            //    RDDoctorMan.Checked = false;
            //    RDNA.Checked = false;
            //}
        }

        private void txtPatientAlert_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.ToString().Length == 2)
            {
                GridPatientAlertSearch.Top = GridPatientAlert.Top - 1;
                GridPatientAlertSearch.Left = GridPatientAlert.Left;
                GridPatientAlertSearch.Width = 461;
                GridPatientAlertSearch.Visible = true;
                var objsearchData = (from t1 in dbc.trn_patient_alerts
                                     where t1.tpa_alert.ToString().ToLower().Contains(txtPatientAlert.Text.Trim().ToLower())
                                     orderby t1.tpa_alert
                                     select new { MessageAlert = t1.tpa_alert }).ToList();
                if (objsearchData.Count == 0)
                {
                    GridPatientAlertSearch.Visible = false;
                }
                GridPatientAlertSearch.DataSource = objsearchData.DistinctBy(x => x.MessageAlert).ToList();
            }
            else if (e.KeyValue.ToString().Length == 0)
            {
                GridPatientAlertSearch.Visible = false;
            }
        }
        private void GridPatientAlertSearch_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtPatientAlert.Text = GridPatientAlertSearch[0, e.RowIndex].Value.ToString();
            }
            catch (Exception)
            {
            }
            GridPatientAlertSearch.Visible = false;
        }
        private void txtPatientAlert_MouseClick(object sender, MouseEventArgs e)
        {
            if (txtPatientAlert.Text.Length > 1)
            {
                GridPatientAlertSearch.Visible = true;
            }
            else
            {
                GridPatientAlertSearch.Visible = false;
            }
        }

        private void txtOtherTumbon_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtOtherAumpher.Top - 1;
            GridOtherAddress.Left = txtOtherAumpher.Left;
            GridOtherAddress.Width = 461;
            GridOtherAddress.Height = 150;
            SearchOtherAddress(txtOtherTumbon.Text.Trim());
        }
        private void txtOtherAumpher_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtmobilePhone.Top - 1;
            GridOtherAddress.Left = txtmobilePhone.Left;
            GridOtherAddress.Width = 461;
            GridOtherAddress.Height = 150;
            SearchOtherAddress(txtOtherAumpher.Text.Trim());
        }
        private void txtOtherProvice_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtmobilePhone.Top - 1;
            GridOtherAddress.Left = txtmobilePhone.Left;
            GridOtherAddress.Width = 461;
            GridOtherAddress.Height = 150;
            SearchOtherAddress(txtOtherProvice.Text.Trim());
        }
        private void txtOtherPostCode_KeyUp(object sender, KeyEventArgs e)
        {
            //GridOtherAddress.Top = 846;
            //SearchOtherAddress(txtOtherPostCode.Text.Trim());
        }

        private void SearchOtherAddress(string strtext)
        {
            if (strtext.Length > 1)
            {
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    DataTable dt = ws.ListAddress(strtext);
                    GridOtherAddress.DataSource = dt;
                    if (GridOtherAddress.Rows.Count > 0)
                    {
                        GridOtherAddress.Visible = true;
                    }
                }
            }
        }
        private void GridOtherAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (GridOtherAddress.CurrentRow != null)
            {

                DataGridViewRow dr = GridOtherAddress.CurrentRow;
                trn_patient_regi pr = (trn_patient_regi)PatientRegisBindingSource.Current;
                pr.tpr_other_tumbon = dr.Cells[2].Value.ToString();
                pr.tpr_other_amphur = dr.Cells[1].Value.ToString();
                pr.tpr_other_province = dr.Cells[0].Value.ToString();
                //if (pr.tpr_other_zip_code == null || pr.tpr_other_zip_code == "")
                //{
                pr.tpr_other_zip_code = dr.Cells[3].Value.ToString();
                //}
                GridOtherAddress.Visible = false;
                txtmobilePhone.Focus();

            }
        }

        private void GenRowNoDridoutDepartment()
        {
            int indexrow = 1;
            for (int i = 0; i < Gridout_department.Rows.Count; i++)
            {
                Gridout_department["colNo", i].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void Gridout_department_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GenRowNoDridoutDepartment();
        }
        private void txtOtherAddress_Click(object sender, EventArgs e)
        {
            if (GridOtherAddress.Visible == true)
            {
                GridOtherAddress.Visible = false;
            }
        }
        private void txtOtherAumpher_Click(object sender, EventArgs e)
        {
            if (GridOtherAddress.Visible == true)
            {
                GridOtherAddress.Visible = false;
            }
        }
        private void txtOtherProvice_Click(object sender, EventArgs e)
        {
            if (GridOtherAddress.Visible == true)
            {
                GridOtherAddress.Visible = false;
            }
        }
        private void txtOtherTumbon_Click(object sender, EventArgs e)
        {
            if (GridOtherAddress.Visible == true)
            {
                GridOtherAddress.Visible = false;
            }
        }
        private void txtOtherPostCode_Click(object sender, EventArgs e)
        {
            if (GridOtherAddress.Visible == true)
            {
                GridOtherAddress.Visible = false;
            }
        }
        private void txtPatientAlert_Click(object sender, EventArgs e)
        {
            if (GridPatientAlertSearch.Visible == true)
            {
                GridPatientAlertSearch.Visible = false;
            }
        }
        //private void txtDoctorName_Click(object sender, EventArgs e)
        //{
        //    if (GridDoctorName.Visible == true)
        //    {
        //        GridDoctorName.Visible = false;
        //    }
        //}

        private void RDnpotimeYes_CheckedChanged(object sender, EventArgs e)
        {
            if (RDnpotimeYes.Checked)
            {
                txtnpotimeRemark.Enabled = true;
            }
            else
            {
                txtnpotimeRemark.Text = "";
                txtnpotimeRemark.Enabled = false;
            }
        }

        private void RDNormal_CheckedChanged(object sender, EventArgs e)
        {
            int index = 0;
            if (RDNormal.Checked)
            {
                for (index = 0; index < CheckConsentForm.Items.Count; index++)
                {
                    string valuecheckbox = Program.GetCheckedListBoxValue(index, CheckConsentForm).ToString();
                    if (Program.Tmp_GetPtarrived.ctnat_code == "TH")
                    {
                        if (valuecheckbox == "RG103")
                        { CheckConsentForm.SetItemChecked(index, true); }
                        else
                        {
                            CheckConsentForm.SetItemChecked(index, false);
                        }
                    }
                    else
                    {
                        if (valuecheckbox == "RG104")
                        { CheckConsentForm.SetItemChecked(index, true); }
                        else
                        {
                            CheckConsentForm.SetItemChecked(index, false);
                        }
                    }
                }
            }
            else if (RDBig.Checked)
            {
                for (index = 0; index < CheckConsentForm.Items.Count; index++)
                {
                    string valuecheckbox = Program.GetCheckedListBoxValue(index, CheckConsentForm).ToString();
                    if (Program.Tmp_GetPtarrived.ctnat_code == "TH")
                    {
                        if (valuecheckbox == "RG101" || valuecheckbox == "RG103")
                        { CheckConsentForm.SetItemChecked(index, true); }
                        else
                        {
                            CheckConsentForm.SetItemChecked(index, false);
                        }
                    }
                    else
                    {
                        if (valuecheckbox == "RG102" || valuecheckbox == "RG104")
                        { CheckConsentForm.SetItemChecked(index, true); }
                        else
                        {
                            CheckConsentForm.SetItemChecked(index, false);
                        }
                    }
                }
            }

        }

        private void btnprintslip_Click(object sender, EventArgs e)
        {
            int tprID = 0;
            if (Program.CurrentRegis != null)
            {
                tprID = Program.CurrentRegis.tpr_id;
            }
            List<string> rptCode = new List<string>() { "RG120" };
            Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode, true, null, true);
            frm.previewReport();

            //try
            //{//แสดง Report
            //    ClsReport.printWristband();
            //}
            //catch (Exception)
            //{

            //}
        }
        private void btnCOFrom_Click(object sender, EventArgs e)
        {
            try
            {//แสดง Report
                int tprID = 0;
                if (Program.CurrentRegis != null)
                {
                    tprID = Program.CurrentRegis.tpr_id;
                }
                List<string> rptCode = new List<string>() { "RG115" };
                Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, rptCode);
                frm.previewReport();
                //ClsReport.previewRpt(new List<string> { "RG115" });
            }
            catch (Exception)
            {

            }
        }
        private void btnPrintConsenform_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> arraystr = new List<string>();
                for (int index = 0; index < CheckConsentForm.Items.Count; index++)
                {
                    if (CheckConsentForm.GetItemChecked(index))
                    {
                        string valuecheckbox = Program.GetCheckedListBoxValue(index, CheckConsentForm).ToString();
                        arraystr.Add(valuecheckbox);
                    }
                }
                if (arraystr.Count() != 0)
                {
                    int tprID = 0;
                    if (Program.CurrentRegis != null)
                    {
                        tprID = Program.CurrentRegis.tpr_id;
                    }
                    Report.frmPreviewReport frm = new Report.frmPreviewReport(tprID, arraystr);
                    frm.previewReport();
                    //ClsReport.previewRpt(arraystr);
                }
                else
                {
                    lbAlertMsg.Text = "Please select Consent Form report.";
                    lbAlertMsg.Focus();
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnPatientAlertView_Click(object sender, EventArgs e)
        {
            frmAlertPatient frm = new frmAlertPatient();
            frm.SetHNno = Program.Tmp_GetPtarrived.papmi_no;
            frm.ShowDialog();
        }

        private void frmRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_loadsuccess)
            {
                this.PatientBindingSource.DataSource = new trn_patient();
                GC.Collect();
                dbc = null;
            }
        }

        private void txtnpotimeRemark_KeyUp(object sender, KeyEventArgs e)
        {
            Timecheck(txtnpotimeRemark, e);
        }

        private void txtTime_KeyUp(object sender, KeyEventArgs e)
        {
            Timecheck(txtTime, e);
        }
        private void Timecheck(MaskedTextBox txt, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Delete) { return; }
            string[] timekey = txt.Text.ToString().Split(':');
            int time1Houre = Convert1.ToInt32(timekey[0].Trim());
            if (timekey[0].Trim().Length == 2)
            {
                if (time1Houre > 23)
                {
                    time1Houre = 23;
                    txt.Text = time1Houre.ToString() + ":" + timekey[1];
                }
            }
            int time2Houre = Convert1.ToInt32(timekey[1].Trim());
            if (timekey[1].Trim().Length == 2)
            {
                if (time2Houre > 59)
                {
                    time2Houre = 59;
                    txt.Text = time1Houre.ToString("00") + ":" + time2Houre.ToString();
                }
            }
        }

        private void GridPatientAlertSearch_Leave(object sender, EventArgs e)
        {
            GridPatientAlertSearch.Visible = false;
        }

        private void frmRegister_Leave(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void CheckListAviation_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CheckListAviation.SelectedIndex == -1)
                return;
            else
            {
                ArrayList SelectedItems = new ArrayList(CheckListAviation.CheckedItems);
                DataTable results = new DataTable(); DataSet ds = new DataSet();
                DateTime datenow = Program.GetServerDateTime();

                if (SelectedItems.Count != 0)
                {
                    string strNameSelected = CheckListAviation.SelectedItem.ToString();
                    string aviatype = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelected select t1.mac_avia_type).FirstOrDefault();

                    DataTable dt = new DataTable();
                    string strNameSelect = CheckListAviation.SelectedItem.ToString();
                    int macid = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelect select t1.mac_id).FirstOrDefault();
                    string mactype = (from t1 in dbc.mst_aviation_categories where t1.mac_ename == strNameSelect select t1.mac_avia_type).FirstOrDefault();
                    string initial = "";

                    switch (mactype)
                    {
                        case "TH":
                            initial = "(Thai)";
                            break;
                        case "CN":
                            initial = "(Canada)";
                            break;
                        case "FA":
                            initial = "(FAA)";
                            break;
                        case "AS":
                            initial = "(Australia)";
                            break;
                        case "FD":
                            initial = "(Flight Attendant)";
                            break;
                    }

                    getAviationType = dbc.pw_Get_AviationType(macid).ToList();
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Name");
                    dt.Columns.Add("flag");
                    ds.Tables.Add(dt);
                    foreach (var item in getAviationType)
                    {
                        var row = dt.NewRow();
                        row["ID"] = item.mav_id;
                        row["Name"] = initial + "~" + item.mat_ename;
                        row["flag"] = initial;
                        dt.Rows.Add(row);
                    }
                    for (int j = 0; j <= dt.Rows.Count - 1; j++)
                    {
                        ////Check duplicate data
                        bool chk = true;
                        int count = g1.Rows.Count;
                        List<string> newList = new List<string>();
                        string[] temp = new string[count];
                        for (int c = 0; c < count; c++)
                        {
                            temp[c] = g1.Rows[c].Cells[0].Value.ToString();
                            if (temp[c] == dt.Rows[j][1].ToString())
                            {
                                chk = false;
                                break;
                            }
                        }
                        if (chk == true)
                        {
                            g1.Rows.Add(dt.Rows[j][1].ToString(), dt.Rows[j][0].ToString(), dt.Rows[j][2].ToString());
                        }
                    }//for
                }//if
            }
        }

        private void ListAviationSelect_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void SelectDeselectCheckBoxes(CheckedListBox chklisbox, bool CheckThem)
        {
            for (int i = 0; i <= (chklisbox.Items.Count - 1); i++)
            {
                if (CheckThem)
                    chklisbox.SetItemCheckState(i, CheckState.Checked);
                else
                    chklisbox.SetItemCheckState(i, CheckState.Unchecked);
            }
        }

        private void CheckListAviation_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            strSelected = this.CheckListAviation.SelectedItem.ToString();
            int num_checked = CheckListAviation.CheckedItems.Count;
            if ((e.CurrentValue != CheckState.Checked) && (e.NewValue == CheckState.Checked))
            {
                num_checked++;
            }
            //if ((e.CurrentValue == CheckState.Checked) && (e.NewValue != CheckState.Checked))
            //{
            //    ListAviationSelect.Items.Remove(strSelected);
            //    num_checked--;

            //}
            // lblStatusSelect.Text = CheckListAviation.Items.Count + " items, " + num_checked + " selected"; 
        }

        private void btnselectFinal_Click(object sender, EventArgs e)
        {
            #region Selected
            for (int i = 0; i <= g1.Rows.Count - 1; i++)
            {
                if (g1.Rows[i].Cells[0].Selected == true)
                {
                    bool chk = true;
                    int count = g2.Rows.Count;
                    List<string> newList = new List<string>();
                    string[] temp = new string[count];
                    string str = g1.Rows[i].Cells[0].Value.ToString();
                    for (int c = 0; c <= count - 1; c++)
                    {
                        temp[c] = g2.Rows[c].Cells[0].Value.ToString();
                        if (temp[c] == str)
                        {
                            chk = false;
                            break;
                        }
                    }
                    if (chk == true)
                    {
                        g2.Rows.Add(g1.Rows[i].Cells[0].Value.ToString(), g1.Rows[i].Cells[1].Value.ToString());
                    }
                }
            }

            for (int i = 0; i < g2.Rows.Count; i++)
            {
                for (int j = 0; j < g1.Rows.Count; j++)
                {
                    if (g1.Rows[j].Cells[0].Value.ToString() == g2.Rows[i].Cells[0].Value.ToString())
                    {
                        g1.Rows.RemoveAt(j);
                        break;
                    }
                }
            }
            #endregion
        }

        private void ListAviationFinalSelect_MouseDoubleClick(object sender, MouseEventArgs e)
        {
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            clearAviationType();
        }

        private void btnmove_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.Empty;
                for (int i = 0; i <= g2.Rows.Count - 1; i++)
                {
                    if (g2.Rows[i].Cells[0].Selected == true)
                    {
                        g1.Rows.Add(g2.Rows[i].Cells[0].Value.ToString(), g2.Rows[i].Cells[1].Value.ToString());
                    }
                }
                for (int i = 0; i < g1.Rows.Count; i++)
                {
                    for (int j = 0; j < g2.Rows.Count; j++)
                    {
                        if (g2.Rows[j].Cells[0].Value.ToString() == g1.Rows[i].Cells[0].Value.ToString())
                        {
                            g2.Rows.RemoveAt(j);
                            break;
                        }
                    }
                }
            }
            catch
            {
                return;
            }
        }

        private void Gridout_department_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Gridout_department.Columns[e.ColumnIndex].Name == "Coldel")
            {
                //var listOutDepartment = trnoutdepartmentsBindingSource.List;
                //listOutDepartment.RemoveAt(e.RowIndex);
                //var outDepartment = trnoutdepartmentsBindingSource.Current;
                trnoutdepartmentsBindingSource.Remove(trnoutdepartmentsBindingSource.Current);
                //PatientRegisBindingSource.Remove(outDepartment);
                var regis = PatientRegisBindingSource.List;
                var depart = trnoutdepartmentsBindingSource;
            }
        }

        private void btnmovecate_Click(object sender, EventArgs e)
        {
            if (listBoxMasterCate.SelectedItems.Count == 0)
            {
                return;
            }
            else
            {
                string strName = listBoxMasterCate.SelectedItem.ToString();
                ArrayList SelectedItems = new ArrayList(listBoxMasterCate.SelectedItems);
                DataTable results = new DataTable(); DataSet ds = new DataSet();
                DateTime datenow = Program.GetServerDateTime();
                foreach (string item in SelectedItems)
                {
                    if (listBoxCateSelected.FindStringExact(item) == -1)
                        listBoxCateSelected.Items.Add(item);

                    while (listBoxMasterCate.SelectedIndex != -1)
                        listBoxMasterCate.Items.RemoveAt(listBoxMasterCate.SelectedIndex);
                }
            }
        }

        private void btnprecate_Click(object sender, EventArgs e)
        {
            try
            {
                if (listBoxCateSelected.Items.Count > 0)
                {
                    string strName = listBoxCateSelected.SelectedItem.ToString();
                    ArrayList SelectedItems = new ArrayList(listBoxCateSelected.SelectedItems);

                    foreach (string item in SelectedItems)
                        if (listBoxMasterCate.FindStringExact(item) == -1)
                            listBoxMasterCate.Items.Add(item);

                    while (listBoxCateSelected.SelectedIndex != -1)
                        listBoxCateSelected.Items.RemoveAt(listBoxCateSelected.SelectedIndex);
                }
            }
            catch
            {
                return;
            }
        }

        private void btnClearAllCate_Click(object sender, EventArgs e)
        {
            if (listBoxCateSelected.Items.Count != 0)
            {
                listBoxCateSelected.Items.Clear();
                listBoxMasterCate.Items.Clear();
                SetComboBoxControl();
            }
            else
                return;
        }

        #region SaveDoctorCategory
        private StatusTransaction getPatientCate(ref trn_patient_regi tpr)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    foreach (var item in listBoxCateSelected.Items)
                    {
                        int mdc_id = cdc.mst_doc_categories.Where(x => x.mdc_ename == item.ToString()).Select(x => x.mdc_id).FirstOrDefault();
                        tpr.trn_patient_cats.Add(new trn_patient_cat
                        {
                            mdc_id = mdc_id
                        });
                    }
                    return StatusTransaction.True;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("frmRegister", "getPatientCate", ex, false);
                return StatusTransaction.Error;
            }
        }
        private List<trn_patient_cat> SaveDoctorCategory(int tprid)
        {
            try
            {
                List<trn_patient_cat> tpc = null;
                if (listBoxCateSelected.Items.Count > 0)
                {
                    tpc = new List<trn_patient_cat>();
                    for (int i = 0; i <= listBoxCateSelected.Items.Count - 1; i++)
                    {
                        this.listBoxCateSelected.SelectedIndex = i;
                    }
                    ArrayList SelectedItems = new ArrayList(listBoxCateSelected.SelectedItems);//Listbox
                    for (int i = 0; i <= SelectedItems.Count - 1; i++)
                    {
                        string strNameSelect = SelectedItems[i].ToString();
                        var getmdcid = (from t1 in dbc.mst_doc_categories
                                        where t1.mdc_ename == strNameSelect
                                        select t1).ToList();
                        foreach (var item in getmdcid)
                        {
                            trn_patient_cat newobj = new trn_patient_cat
                            {
                                tpr_id = tprid,
                                mdc_id = Convert.ToInt32(item.mdc_id)
                            };
                            tpc.Add(newobj);
                            //dbc.trn_patient_cats.InsertOnSubmit(newobj);
                            //dbc.SubmitChanges();
                        }
                    }
                }
                return tpc;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region SavePatientAviation
        private StatusTransaction getAviationCate(ref trn_patient_regi tpr)
        {
            try
            {
                foreach (DataGridViewRow row in g2.Rows)
                {
                    int get_mav_id = Convert.ToInt32(row.Cells[1].Value.ToString());
                    tpr.trn_patient_aviations.Add(new trn_patient_aviation
                    {
                        mav_id = get_mav_id
                    });
                }
                return StatusTransaction.True;
            }
            catch (Exception ex)
            {
                Program.MessageError("frmRegister", "getPatientCate", ex, false);
                return StatusTransaction.Error;
            }
        }
        private List<trn_patient_aviation> SaveAivation(int tprid)
        {
            try
            {
                List<trn_patient_aviation> tpa = null;
                #region SaveAviation
                if (g2.Rows.Count > 0)
                {
                    tpa = new List<trn_patient_aviation>();
                    for (int i = 0; i <= g2.Rows.Count - 1; i++)
                    {
                        trn_patient_aviation newobj = new trn_patient_aviation
                        {
                            tpr_id = tprid,
                            mav_id = Convert.ToInt32(g2.Rows[i].Cells[1].Value.ToString())
                        };
                        tpa.Add(newobj);
                    }
                }
                return tpa;
                #endregion
            }
            catch
            {
                return null;
            }
        }

        #endregion

        private void btnAddHealth_Click(object sender, EventArgs e)
        {
            Oldtext.oldtxt = txtHealthCheckup.Text;
            frmHealthSelect newfrm = new frmHealthSelect();
            newfrm.ShowDialog();
            txtHealthCheckup.Text = newfrm.strSelectedName;
            mhcid = newfrm.SelectedID;
            strSelected = newfrm.strSelectedName;
        }

        #region LoadAviation-Todolist
        private void loadAviationTodolist()
        {

            if (RDPatientType_Aviation.Checked == true)
            {
                txtEmployeeID.Text = "";
                lbEmpCode.Visible = true;
                lbEmpCode.Enabled = true;
                txtEmployeeID.Enabled = true;
                txtEmployeeID.Visible = true;
                chOfficeAddress.Enabled = true;
                //gpCompany.Enabled = true;
                //DDcompany.Enabled = true;
                rdWaitPE.Checked = false; // morn : default pe type is null for checked Corporate
                rdNotWaitPE.Checked = false;
            }
            else
            {
                txtEmployeeID.Visible = false;
                lbEmpCode.Visible = false;
                txtEmployeeID.Visible = false;
                chOfficeAddress.Checked = false;
                chOfficeAddress.Enabled = false;
                gpCompany.Enabled = false;
                //DDcompany.Enabled = false;
            }

            //var objmco = dbc.sp_get_mst_company_todolist().ToList();
            //sp_get_mst_company_todolistResult emp = new sp_get_mst_company_todolistResult();
            //emp.mco_code = null;
            //emp.company_name = "";
            //emp.mco_id = 0;
            //objmco.Add(emp);

            //if(objmco.Count > 0)
            //{
            //    DDcompany.DataSource = objmco;
            //    DDcompany.DisplayMember = "company_name";
            //    DDcompany.ValueMember = "mco_id";
            //}

            string fname, lname;
            fname = "";
            lname = "";
            if (Program.CurrentRegis == null)
            {
                fname = Program.Tmp_GetPtarrived.papmi_name;
                lname = Program.Tmp_GetPtarrived.papmi_name2;
            }
            else
            {
                var objpt = dbc.trn_patients.Where(x => x.tpt_id == Program.CurrentRegis.tpt_id).FirstOrDefault();
                fname = objpt.tpt_first_name;
                lname = objpt.tpt_last_name;
            }

            var objnamechk = dbc.sp_get_trn_name_check_todolist(fname, lname).FirstOrDefault();
            if (objnamechk != null)
            {
                txtEmployeeID.Text = objnamechk.tnc_emp_id;
            }
        }
        #endregion

        /*private void RDPatientType_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Aviation.Checked || RDPatientType_AviationAircrew.Checked)
            {
                if (RDPatientType_Aviation.Checked)
                {
                    rdWaitPE.Checked = false; // morn : default pe type is null for checked Corporate
                    rdNotWaitPE.Checked = false;

                    chOfficeAddress.Enabled = true;
                    gpCompany.Enabled = true;
                        
                }
                else
                {
                    chOfficeAddress.Checked = false;
                    chOfficeAddress.Enabled = false;
                    gpCompany.Enabled = false;
                }

                CBAviationCategory.Enabled = true;
                RDAviationCategoryNewcase.Enabled = true;
                RDaviationTypeFollowup.Enabled = true;

                CheckListAviation.Enabled = true;
                ListAviationSelect.Enabled = true;
                btnselectFinal.Enabled = true;
                ListAviationFinalSelect.Enabled = true;
                btnClear.Enabled = true;
                btnmove.Enabled = true;
                DateTime datenow = Program.GetServerDateTime();
                var listAviationCat = (from t1 in dbc.mst_aviation_categories
                                        where t1.mac_status == 'A'
                                        && datenow >= t1.mac_effective_date.Value
                                                    && (t1.mac_expire_date != null ? (datenow <= t1.mac_expire_date.Value) : true)
                                        select t1).ToList();
                DataTable Tmptable = new DataTable(); DataSet ds = new DataSet("dss");

                Tmptable.Columns.Add("ID"); Tmptable.Columns.Add("Name");
                ds.Tables.Add(Tmptable);
                var item = CheckListAviation.Items;
                foreach (var element in listAviationCat)
                {
                    var row = Tmptable.NewRow();
                    row["ID"] = element.mac_id;
                    row["Name"] = element.mac_ename;
                    Tmptable.Rows.Add(row);
                }
                for (int i = 0; i <= Tmptable.Rows.Count - 1; i++)
                {
                    item.Add(Tmptable.Rows[i][1].ToString());
                }
            }
            else
            {
                RDAviationCategoryNewcase.Checked = false;
                RDaviationTypeFollowup.Checked = false;
                RDAviationCategoryNewcase.Enabled = false;
                RDaviationTypeFollowup.Enabled = false;
                CBAviationCategory.Enabled = false;
                CheckListAviation.Enabled = false;
                ListAviationSelect.Enabled = false;
                btnselectFinal.Enabled = false;
                ListAviationFinalSelect.Enabled = false;
                btnClear.Enabled = false;
                btnmove.Enabled = false;
                CheckListAviation.Items.Clear();
                ListAviationSelect.Items.Clear();
                ListAviationFinalSelect.Items.Clear();

                if (RDPatientType_General.Checked)
                {
                    gpCompany.Enabled = false;
                    chOfficeAddress.Checked = false;
                    chOfficeAddress.Enabled = false;
                }
                else if (RDPatientType_Corporate.Checked)
                {
                    chOfficeAddress.Enabled = true;
                    rdWaitPE.Checked = false; // morn : default pe type is null for checked Corporate
                    rdNotWaitPE.Checked = false;
                    gpCompany.Enabled = true;
                }
            }
        }*/

        private void RDPatientType_walkin_CheckedChanged(object sender, EventArgs e)
        {
            //if (RDPatientType_General.Checked == true)
            //{
            //    txtEmployeeID.Visible = false;
            //    lbEmpCode.Visible = false;
            //    txtEmployeeID.Visible = false;
            //    chOfficeAddress.Checked = false;
            //    chOfficeAddress.Enabled = false;
            //    gpCompany.Enabled = false;
            //    DDcompany.SelectedIndex = 0;
            //}
        }

        private void ListAviationSelect_MouseMove(object sender, MouseEventArgs e)
        {
            SetTooltip(sender, e);
        }

        private void ListAviationFinalSelect_MouseMove(object sender, MouseEventArgs e)
        {
            SetTooltip(sender, e);
        }

        private void SetTooltip(object sender, MouseEventArgs e)
        {
            try
            {
                ListBox lb = (ListBox)sender;
                int index = lb.IndexFromPoint(e.Location);
                if (index >= 0 && index < lb.Items.Count)
                {
                    string toolTipString = lb.Items[index].ToString();
                    if (toolTip1.GetToolTip(lb) != toolTipString)
                        toolTip1.SetToolTip(lb, toolTipString);
                }
                else
                    toolTip1.Hide(lb);
            }
            catch
            {
            }
        }

        private void ListAviationSelect_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void DDcompany_SelectedValueChanged(object sender, EventArgs e)
        {
            EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();
            if (DDcompany.SelectedValue == null)
            {
                DDDepartment.DataSource = null;
            }
            else
            {
                List<string> dep = cls.getListDep(DDcompany.SelectedValue.ToString());
                DDDepartment.DataSource = dep;
            }
        }

        private void DDcompany_Leave(object sender, EventArgs e)
        {
            if (DDcompany.SelectedItem == null)
            {
                DDcompany.SelectedIndex = 0;
            }
        }

        private void gpCompany_EnabledChanged(object sender, EventArgs e)
        {
            if (((GroupBox)sender).Enabled == false)
            {
                DDcompany.SelectedItem = 0;
            }
        }

        
        private void DoctorCompleteBox_currentChangeHandler(object sender, object e)
        {
            try
            {
                trn_patient_regi objcurrenttpr = (trn_patient_regi)PatientRegisBindingSource.Current;
                if (objcurrenttpr != null)
                {
                    if (e != null)
                    {
                        objcurrenttpr.tpr_req_doc_code = DoctorCompleteBox.SelectedValue.ToString();
                        objcurrenttpr.tpr_req_doc_name = DoctorCompleteBox.Text;
                    }
                    else
                    {
                        objcurrenttpr.tpr_req_doc_code = null;
                        objcurrenttpr.tpr_req_doc_name = null;
                    }
                }
            }
            catch(Exception ex)
            {
                Program.MessageError(this.Name, "DoctorCompleteBox_currentChangeHandler",ex,false);
            }
        }

        private void RDRequestDoctorInDepart_CheckedChanged(object sender, EventArgs e)
        {
            GBRequestPEBefore.Enabled = !RDRequestoutLet.Checked;
            DoctorCompleteBox.SelectedValue = null;
            if (RDRequestoutLet.Checked)
            {
                RDNA.Checked = true;
                RDNA.AutoCheck = false;
                RDDoctorMan.AutoCheck = false;
                RDDoctorFemale.AutoCheck = false;
            }
            else
            {
                RDNA.AutoCheck = true;
                RDDoctorMan.AutoCheck = true;
                RDDoctorFemale.AutoCheck = true;
            }
            setDataSourceDoctor();
        }

        private void setDataSourceDoctor()
        {
            if (RDRequestDoctorInDepart.Checked)
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    var result = cdc.mst_user_types.Where(x => x.mut_username != null && x.mut_fullname != null)
                    .OrderBy(x => x.mut_fullname)
                    .Select(x => new
                    {
                        val = x.mut_username,
                        dis = x.mut_fullname
                    }).ToList();
                    DoctorCompleteBox.DataSource = result;
                    DoctorCompleteBox.ValueMember = "val";
                    DoctorCompleteBox.DisplayMember = "dis";
                }
            }
            else if (RDRequestoutLet.Checked)
            {
                List<DoctorProfile> result = new EmrClass.AutoCompleteDoctor().GetDoctorData();
                if (result.Count > 0)
                {
                    DoctorCompleteBox.DataSource = result;
                    DoctorCompleteBox.ValueMember = "SSUSR_Initials";
                    DoctorCompleteBox.DisplayMember = "CTPCP_Desc";
                }
                else
                {
                    DoctorCompleteBox.DataSource = null;
                }
            }
        }

        private void frmRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
    class LocationRegis
    {
        public string strlocation { get; set; }
    }
    class PatientOrderItemAddPlans
    {
        public string item_row_id { get; set; }
        public int mvt_id { get; set; }
        public string patho { get; set; }
        public string pacSheet { get; set; }
    }
}


