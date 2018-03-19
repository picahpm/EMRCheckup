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
namespace BKvs2010
{
    public partial class frmPreRegister : Form
    {
        public frmPreRegister()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        public Boolean IsAddnew { get; set; }
        public Boolean Iscompleted = false;
        public DateTime AppointDate;
        public string HNno;
        public string FullName;
        private bool isAddnew = false;

        private void frmPreRegister_Load(object sender, EventArgs e)
        {
            this.Text = "Pre Register";
            lbDataFullName.Text = FullName;
            lbDataHN.Text = HNno;
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show(); 
            Application.DoEvents();

            LoadData();

            Application.DoEvents();
            try
            {
                DateTime datenow = Program.GetServerDateTime();

                var objlocation = (from t1 in dbc.mst_locations
                                   where datenow.Date >= t1.mlc_effective_date.Value.Date
                                   && (t1.mlc_expire_date == null || (t1.mlc_expire_date != null && datenow.Date <= t1.mlc_expire_date.Value.Date))
                                   select new LocationRegis { strlocation = t1.mlc_ename }).ToList();
                LocationRegis newselect = new LocationRegis();
                newselect.strlocation = "- Select -";
                objlocation.Insert(0, newselect);
                DDLocation.DataSource = objlocation;
                DDLocation.DisplayMember = "strlocation";
                DDLocation.ValueMember = "strlocation";
                if (isAddnew == true)
                {
                    using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                    {
                        string SiteCode = Program.CurrentSite.mhs_code;

                        //LoadOutDepart
                        DataTable dtOutDepart = ws.getApptByHN(HNno, SiteCode, datenow.ToString("yyyy-MM-dd"));
                        foreach (DataRow droutDepart in dtOutDepart.Rows)
                        {
                            tmp_out_department newitem = (tmp_out_department)tmpoutdepartmentsBindingSource.AddNew();
                            newitem.description = droutDepart["SER_Desc"].ToString();
                            newitem.location = droutDepart["CTLOC_Desc"].ToString();

                            try
                            {
                                DateTime dtdata = Convert.ToDateTime(droutDepart["AS_Date"].ToString());
                                TimeSpan tarrivaltime = TimeSpan.Parse(droutDepart["AS_SessStartTime"].ToString().Replace("PT", "").Replace("H", ":").Replace("M", ""));
                                newitem.start_date = new DateTime(dtdata.Year, dtdata.Month, dtdata.Day, tarrivaltime.Hours, tarrivaltime.Minutes, 0);
                            }
                            catch (Exception)
                            {
                                newitem.start_date = null;
                                return;
                            }

                        }
                    }
                }
                GenRowNoDridoutDepartment();
                //this.GetPackageTakecare(Program.Tmp_GetPtarrived.paadm_rowid);// Load Pacakge
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "frmPreRegister_Load", ex, false);
                //throw;
            }

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

            frmbg.Close();
        }
        public void LoadData()
        {
            try
            {
                this.SetComboBoxControl();
                DateTime Datenow = Program.GetServerDateTime();

                //เช็คว่า HN นี้มีอยู่หรือไม่
                    var objpatientlist = (from t1 in dbc.tmp_patient_regis
                                            where t1.hn_no == this.HNno
                                            && t1.status=='N'
                                            orderby t1.id descending
                                            select t1).FirstOrDefault();
                    if (objpatientlist != null)
                    {
                        this.TmpPatientRegisBindingSource.DataSource = objpatientlist;
                        this.tmpoutdepartmentsBindingSource.DataSource = objpatientlist.tmp_out_departments;
                        tmp_patient_regi objregis = (tmp_patient_regi)TmpPatientRegisBindingSource.Current;
                        Program.SetValueRadioGroup(GBPatienttype, objregis.patient_type);

                        Program.SetValueRadioGroup(GBRequestPEBefore, objregis.req_pe_bef_chkup);
                        Program.SetValueRadioGroup(GBRequestPE, objregis.req_doctor);

                        if (objregis.req_doctor != 'N')
                        {
                            Program.SetValueRadioGroup(GBDoctorGender, objregis.req_doc_gender);
                            Program.SetValueRadioGroup(GBRequestDoctor, objregis.req_inorout_doctor);
                        }
                        Program.SetValueRadioGroup(GBAviationType, objregis.aviation_type);
                        Program.SetValueRadioGroup(GBPEType, objregis.pe_type);
                        Program.SetValueRadioGroup(GBNPOTime, objregis.npo_time);
                        Program.SetValueRadioGroup(GBBook, objregis.send_book);

                        var objdataaviation = (from t1 in dbc.mst_aviation_categories
                                               where t1.mac_id == objregis.mac_id
                                               select new { Code = t1.mac_id, Name = t1.mac_ename }).FirstOrDefault();
                        if (objdataaviation != null)
                        {
                            CBAviationCategory.SelectedValue = objdataaviation.Code;
                        }
                        else
                        {
                            CBAviationCategory.SelectedIndex = 0;
                        }

                        var objdataCBHealthCheckUPProgram = (from t1 in dbc.mst_health_checkups
                                                             where t1.mhc_id == objregis.mhc_id
                                                             select new { Code = t1.mhc_id, Name = t1.mhc_ename }).FirstOrDefault();
                        if (objdataCBHealthCheckUPProgram != null)
                        {
                            CBHealthCheckUPProgram.SelectedValue = objdataCBHealthCheckUPProgram.Code;
                        }
                        else
                        {
                            CBHealthCheckUPProgram.SelectedIndex = 0;
                        }

                        //CBDoctorCategory
                        var objdataCBDoctorCategory = (from t1 in dbc.mst_doc_categories
                                                       where t1.mdc_id == objregis.mdc_id
                                                       select new { Code = t1.mdc_id, Name = t1.mdc_ename }).FirstOrDefault();
                        if (objdataCBDoctorCategory != null)
                        {
                            CBDoctorCategory.SelectedValue = objdataCBDoctorCategory.Code;
                        }
                        else
                        {
                            CBDoctorCategory.SelectedIndex = 0;
                        }

                        ////Added.Akkaradech on 2013-12-24Program.Tmp_GetPtarrived.papmi_no
                        //var objpatient = (from t in dbc.trn_patients where t.tpt_hn_no == HNno select t).FirstOrDefault();
                        var objpatient = (from t in dbc.tmp_patient_regis where t.hn_no == HNno select t).FirstOrDefault();
                        if (objpatient != null)
                        {
                            if (objpatient.vip_hpc == true)
                                chkviphpc.Checked = true;
                            if (objpatient.vip_hpc == false)
                                chkviphpc.Checked = false;
                        }
                        ////EndAdded.Akkaradech on 2013-12-24
                    }
                    else
                    {
                        isAddnew = true;
                        this.TmpPatientRegisBindingSource.DataSource =(from t1 in dbc.tmp_patient_regis select t1).Take(0);
                        this.TmpPatientRegisBindingSource.AddNew();
                        tmp_patient_regi tmb = (tmp_patient_regi)TmpPatientRegisBindingSource.Current;
                        this.tmpoutdepartmentsBindingSource.DataSource = tmb.tmp_out_departments;
                        // เช็คว่า HN
                        tmb.hn_no = HNno;
                        tmb.appoint_date = AppointDate;
                        tmb.status = 'N';//U=Use,N=No use
                    }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "LoadData", ex, false);
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

        private void SetComboBoxControl()
        {
            try
            {
                DateTime datenow = Program.GetServerDateTime();
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
                    CBHealthCheckUPProgram.SelectedIndex = 0;
                }

                //Doctorcategory
                var listdoctorCat = (from t1 in dbc.mst_doc_categories
                                     where t1.mdc_status == 'A'
                                     && datenow >= t1.mdc_effective_date.Value
                                               && (t1.mdc_expire_date != null ? (datenow <= t1.mdc_expire_date.Value) : true)
                                     orderby t1.mdc_ename ascending
                                     select new DropdownData { Code = t1.mdc_id, Name = t1.mdc_ename }).ToList();
                DropdownData newselect2 = new DropdownData();
                newselect2.Code = null;
                newselect2.Name = "- Select -";
                listdoctorCat.Insert(0, newselect2);
                CBDoctorCategory.DataSource = listdoctorCat;
                CBDoctorCategory.ValueMember = "Code";
                CBDoctorCategory.DisplayMember = "Name";
                if (listdoctorCat.Count > 0) { 
                    CBDoctorCategory.SelectedIndex=0; 
                }

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
                if (listAviationCat.Count > 0)
                {
                    CBAviationCategory.SelectedIndex = 0; 
                }

            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "SetComboBoxControl", ex, false);
                throw;
            }

        }

        private void btnOutDepartment_Click(object sender, EventArgs e)
        {
            if (!checkAddOutDepartment())
            {
                return;
            }
            try
            {
                tmpoutdepartmentsBindingSource.AddNew();
                tmp_out_department newitem = (tmp_out_department)tmpoutdepartmentsBindingSource.Current;
                newitem.description = txtDescription.Text.Trim();
                newitem.location = DDLocation.Text; 
                try
                {
                    newitem.start_date =Convert.ToDateTime( txtTime.Text.Trim());
                }
                catch (Exception)
                {
                    newitem.start_date = null;
                    return;
                }
                txtDescription.Text = "";
                DDLocation.SelectedIndex = -1;
                txtTime.Text = "";
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnOutDeportment_Click", ex, false);
                throw;
            }


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

        private void GetPackageTakecare(String RowID)
        {
            btnloadPackageTK.Enabled = false;
            try
            {

                if (RowID ==null || RowID == "") { return; }
                //Get from web service
                using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                {
                    DataTable dt = ws.GetPTPackage(Convert.ToInt32(RowID));
                    //show Package

                    //Save Pakcage to Base
                    List<TmpOrderSet> packageList = new List<TmpOrderSet>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        TmpOrderSet newitem = new TmpOrderSet();
                        newitem.Code = dr["ARCOS_Code"].ToString();
                        newitem.Desc = dr["ARCOS_Desc"].ToString();
                        newitem.RowID = dr["ARCOS_RowId"].ToString();
                        packageList.Add(newitem);
                    }
                    var pklist = packageList.DistinctBy(pk => new { pk.RowID, pk.Code, pk.Desc });
                    GridPackage.DataSource = pklist;
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "GetPackageTakecare", ex, false);
                throw;
            }
            btnloadPackageTK.Enabled = true;

        }

        private void RDPatientType_Corporate_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Corporate.Checked)
            {
                lbEmpCode.Visible = true;
                txtEmployeeID.Visible = true;
                chOfficeAddress.Enabled = true;
                radioButton20.Checked = false; // morn : default pe type is null for checked Corporate
                radioButton19.Checked = false;
            }
            else
            {
                txtEmployeeID.Text = "";
                lbEmpCode.Visible = false;
                txtEmployeeID.Visible = false;
                chOfficeAddress.Checked = false;
                chOfficeAddress.Enabled = false;
            }
            RDBookwantResult_CheckedChanged(null, null);
        }
        private void RDrequestPE_CheckedChanged(object sender, EventArgs e)
        {
            if (RDrequestPE.Checked)
            {
                GBRequestDoctor.Enabled = false;
                txtDoctorName.Enabled = false;
                GBDoctorGender.Enabled = false;
                RDRequestDoctorInDepart.Checked = false;
                RDRequestoutLet.Checked = false;
            }
            else
            {
                GBRequestDoctor.Enabled = true;
                txtDoctorName.Enabled = true;
                GBDoctorGender.Enabled = true;
            }
        }


        private void btnRebister_Click(object sender, EventArgs e)
        {
            frmBGScreen frmbg = new frmBGScreen();
            frmbg.Show(); 
            Application.DoEvents();

            //Avation Type Value
            label18.ForeColor = Color.Black;
            if (Program.GetValueRadioTochar(GBPatienttype) == '2' || Program.GetValueRadioTochar(GBPatienttype) == '4')
            {
                var AvationtypeValue = Program.GetValueRadioTochar(GBAviationType);
                if (AvationtypeValue == null || CBAviationCategory.SelectedValue == null)
                {
                    if (CBAviationCategory.SelectedValue == null)
                    {
                        lbAlertMsg.Text = "Avation Category must have value!";
                    }
                    if (AvationtypeValue == null)
                    {
                        lbAlertMsg.Text = "Avation type must have value!";
                    }
                    lbAlertMsg.Focus();
                    label18.ForeColor = Color.Red;
                    frmbg.Close();
                    return;
                }
            }

            //check NEO Time Correct
            TimeSpan interval;
            bool isconvert = true;
            try
            {
                interval = TimeSpan.Parse(txtnpotimeRemark.Text.Trim());
            }
            catch (FormatException)
            {
                isconvert = true;
            }
            catch (OverflowException)
            {
                isconvert = false;
            }

            if (RDnpotimeYes.Checked && (txtnpotimeRemark.Text.Trim().Length < 5 || isconvert == false))
            {
                lbAlertMsg.Text = "NPO Time incorrect.";
                lbAlertMsg.Focus();
                frmbg.Close();
                return;
            }

            //Save Data 
            try
            {
                Boolean saveIsCompleted = false;
                dbc.Connection.Open();
                DbTransaction trans = dbc.Connection.BeginTransaction();
                dbc.Transaction = trans;
                DateTime datenowvalue = Program.GetServerDateTime();
                var objregis = (tmp_patient_regi)this.TmpPatientRegisBindingSource.Current;

                objregis.hn_no = HNno;
                objregis.appoint_date = AppointDate;//รับค่าจากปุ่มที่กดตอนเปิดหน้าจอ
                objregis.status='N'; 

                if (CBAviationCategory.Enabled == true && CBAviationCategory.SelectedValue!=null)
                {
                    objregis.mac_id = Program.GetValueComboBoxInt(CBAviationCategory);
                }
                else
                {
                    objregis.mac_id = null;
                }

                if (CBDoctorCategory.Enabled == true && CBDoctorCategory.SelectedValue!=null)
                {
                    objregis.mdc_id = Program.GetValueComboBoxInt(CBDoctorCategory);
                }
                else
                {
                    objregis.mdc_id = null;
                }
                if (CBHealthCheckUPProgram.SelectedValue != null)
                {
                    objregis.mhc_id = Convert1.ToInt32(CBHealthCheckUPProgram.SelectedValue);
                }
                else
                {
                    objregis.mhc_id = null;
                }
                objregis.patient_type = Program.GetValueRadioTochar(GBPatienttype);
                objregis.req_pe_bef_chkup =Program.GetValueRadioTochar(GBRequestPEBefore); 
                objregis.req_doctor=Program.GetValueRadioTochar(GBRequestPE);
                if (objregis.req_doctor == 'N')
                {
                    objregis.req_doc_gender = null;
                    objregis.req_inorout_doctor = string.Empty;
                    objregis.req_doc_code = string.Empty;
                    objregis.req_doc_name = string.Empty;
                }
                else
                {
                    objregis.req_doc_gender = Program.GetValueRadioTochar(GBDoctorGender);
                    objregis.req_inorout_doctor = Program.GetValueRadio(GBRequestDoctor);

                }
                if (RDPatientType_Aviation.Checked || RDPatientType_AviationAircrew.Checked)
                {//ถ้าเลือกAViation ค่อย Save 
                    objregis.aviation_type = Program.GetValueRadioTochar(GBAviationType);
                }
                objregis.pe_type = Program.GetValueRadioTochar(GBPEType);
                objregis.npo_time = Program.GetValueRadioTochar(GBNPOTime);
                objregis.send_book = Program.GetValueRadioTochar(GBBook);
                objregis.send_to = Program.GetValueRadioTochar(panelBookSendTo);
                objregis.vip_hpc = (chkviphpc.Checked) ? true : false;//Added.Akkaradech on 2013-12-24{viphpc}
                try
                {
                    tmpoutdepartmentsBindingSource.EndEdit();
                    TmpPatientRegisBindingSource.EndEdit();
                    dbc.SubmitChanges();
                    dbc.Transaction.Commit();
                    saveIsCompleted = true;
                    Iscompleted = true;
                }
                catch (Exception ex)
                {
                    dbc.Transaction.Rollback();
                    Program.MessageError(this.Name, "btnRebister_Click", ex, false);
                }
                finally
                {
                    dbc.Connection.Close();
                }

                if (saveIsCompleted == true)
                {
                    lbAlertMsg.Text = "Save data completed.";
                    lbAlertMsg.Focus();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnRebister_Click", ex, false);
            }
            //ปิดหน้าจอมืดๆๆ
            frmbg.Close();
        }

        private List<tmpDoctorTable> DoctorList = new List<tmpDoctorTable>();
        private void SearchGetDoctor(string strSearch)
        {
            if (RDRequestDoctorInDepart.Checked == true)
            {
                var objlistMstUserType = (from t1 in dbc.mst_user_types
                                          where t1.mut_type == 'D'
                                          && t1.mut_out_checkup == false
                                          && t1.mut_fullname.Contains(strSearch)
                                          && t1.mut_status =='A'
                                          select new { DoctorName = t1.mut_fullname, DoctorCode = t1.mut_username }).ToList();
                GridDoctorName.DataSource = objlistMstUserType;
                GridDoctorName.Columns[1].Visible = false;
                GridDoctorName.Columns[0].HeaderText = "Doctor Name";
                GridDoctorName.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (objlistMstUserType.Count() > 0)
                {
                    GridDoctorName.Visible = true;
                }
            }
            else
            {
                try
                {
                    if (strSearch.Length >= 2)
                    {
                        GridDoctorName.Visible = true;
                        using (Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls())
                        {
                            DataTable dt = ws.GetCareprovider(strSearch);

                            GridDoctorName.DataSource = dt;

                            GridDoctorName.Columns[0].Visible = false;
                            GridDoctorName.Columns[1].Visible = false;
                            GridDoctorName.Columns[2].Visible = false;
                            GridDoctorName.Columns[3].Visible = false;

                            GridDoctorName.Columns[4].Visible = true;
                            GridDoctorName.Columns[4].HeaderText = "Doctor Name";
                            GridDoctorName.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                            GridDoctorName.Visible = true;
                        }
                    }
                    else if (strSearch.Length >= 2)
                    {
                        GridDoctorName.Visible = true;
                        var datasourc = DoctorList.Where(c => c.CTPCP_Desc.Contains(strSearch));
                        GridDoctorName.DataSource = datasourc;
                    }
                    else
                    {
                        GridDoctorName.Visible = false;
                    }
                }
                catch (Exception)
                {
                    // Program.MessageError("=>SearchGetDoctor :" + ex.Message);
                }
            }

        }
        private void txtDoctorName_KeyUp(object sender, KeyEventArgs e)
        {
            GridDoctorName.Top = txtDoctorName.Location.Y + 24;
            GridDoctorName.Left = txtDoctorName.Location.X;
            SearchGetDoctor(txtDoctorName.Text.Trim());
        }
        private void GridDoctorName_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                var selectedItems = GridDoctorName.CurrentRow;
                tmp_patient_regi objcurrenttpr = (tmp_patient_regi)TmpPatientRegisBindingSource.Current;
                if (RDRequestDoctorInDepart.Checked == true)
                {
                    objcurrenttpr.req_doc_code = selectedItems.Cells[1].Value.ToString();
                    objcurrenttpr.req_doc_name = selectedItems.Cells[0].Value.ToString();
                }
                else
                {
                    objcurrenttpr.req_doc_code = selectedItems.Cells[3].Value.ToString();
                    objcurrenttpr.req_doc_name = selectedItems.Cells[4].Value.ToString();
                }
            }
            catch (Exception)
            {
            }
            GridDoctorName.Visible = false;
        }
        private void txtDoctorName_MouseClick(object sender, MouseEventArgs e)
        {
            //GridDoctorName.Visible = true;
        }

        private void RDPatientType_Aviation_CheckedChanged(object sender, EventArgs e)
        {
            if (RDPatientType_Aviation.Checked || RDPatientType_AviationAircrew.Checked)
            {
                CBAviationCategory.Enabled = true;
                RDAviationCategoryNewcase.Enabled = true;
                RDaviationTypeFollowup.Enabled = true;
            }
            else
            {
                RDAviationCategoryNewcase.Checked = false;
                RDaviationTypeFollowup.Checked = false;
                RDAviationCategoryNewcase.Enabled = false;
                RDaviationTypeFollowup.Enabled = false;
                CBAviationCategory.Enabled = false;
            }
        }
        private void btnloadPackageTK_Click(object sender, EventArgs e)
        {
            this.GetPackageTakecare(Program.Tmp_GetPtarrived.paadm_rowid);// Load Pacakge
        }

        private void chsameMainAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chsameMainAddress.Checked)
            {
                //tmp_patient_regi objregis = (tmp_patient_regi)TmpPatientRegisBindingSource.Current;
                //objregis.other_address = objregis.main_address;
                //objregis.other_province = objregis.main_province;
                //objregis.other_amphur = objregis.main_amphur;
                //objregis.other_tumbon = objregis.tpr_main_tumbon;
                //objregis.other_zip_code = objregis.tpr_main_zip_code;
                //chOfficeAddress.Checked = false;
            }

        }
        private void chOfficeAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (chOfficeAddress.Checked)
            {
                trn_patient_regi objregis = (trn_patient_regi)TmpPatientRegisBindingSource.Current;
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
            if (RDRequestoutLet.Checked == false)
            {
                GBDoctorGender.Enabled = true;
            }
            else
            {
                GBDoctorGender.Enabled = false;
                RDDoctorFemale.Checked = false;
                RDDoctorMan.Checked = false;
            }
        }
        private void txtOtherTumbon_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtOtherTumbon.Location.Y + 24;
            GridOtherAddress.Left = txtOtherAddress.Location.X;
            SearchOtherAddress(txtOtherTumbon.Text.Trim());
        }
        private void txtOtherAumpher_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtOtherAumpher.Location.Y + 24;
            GridOtherAddress.Left = txtOtherAumpher.Location.X;
            SearchOtherAddress(txtOtherAumpher.Text.Trim());
        }
        private void txtOtherProvice_KeyUp(object sender, KeyEventArgs e)
        {
            GridOtherAddress.Top = txtOtherProvice.Location.Y + 24;
            GridOtherAddress.Left = txtOtherAumpher.Location.X;
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
                tmp_patient_regi pr = (tmp_patient_regi)TmpPatientRegisBindingSource.Current;
                pr.other_tumbon = dr.Cells[2].Value.ToString();
                pr.other_amphur = dr.Cells[1].Value.ToString();
                pr.other_province = dr.Cells[0].Value.ToString();
                if (pr.other_zip_code == null || pr.other_zip_code == "")
                {
                    pr.other_zip_code = dr.Cells[3].Value.ToString();
                }
                GridOtherAddress.Visible = false;
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
        private void txtDoctorName_Click(object sender, EventArgs e)
        {
            if (GridDoctorName.Visible == true)
            {
                GridDoctorName.Visible = false;
            }
        }

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



        private void frmRegister_FormClosing(object sender, FormClosingEventArgs e)
        {
            dbc = null;
            GC.Collect();
        }

        private void GBPatienttype_Paint(object sender, PaintEventArgs e)
        {

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


    }
}
