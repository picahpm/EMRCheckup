using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
namespace CheckupBO
{
    public partial class frmSetupData : Form
    {
        public frmSetupData()
        {
            InitializeComponent();
        }
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        private void frmSetupData_Load(object sender, EventArgs e)
        {
            try
            {

                //Add ComboboxColumns Zone and hiddin Columnzone
                var objZone = (from t1 in dbc.mst_hpc_sites
                               select new
                               {
                                   Value = t1.mhs_id,
                                   Text = t1.mhs_ename
                               }).ToList();
                DataGridViewComboBoxColumn dgvCmbSite = new DataGridViewComboBoxColumn();
                {
                    dgvCmbSite.HeaderText = "Zone";// Hearder name of column
                    dgvCmbSite.ValueMember = "Value";// Add items into Combobox
                    dgvCmbSite.DisplayMember = "Text";
                    dgvCmbSite.DataPropertyName = "mhs_id";
                    dgvCmbSite.DataSource = objZone;
                    dgvCmbSite.DisplayIndex = 2;
                    dgvCmbSite.Width = 85;
                    dgvCmbSite.DisplayStyleForCurrentCellOnly = true;
                    dgvCmbSite.FlatStyle = FlatStyle.Flat;
                }
                GridConfigHeader.Columns["Colmhsid"].Visible = false;
                GridConfigHeader.Columns.Add(dgvCmbSite);
                LoaddataTab1();

                //tab2
                LoaddataTab2();
                var objroomlist = (from t1 in dbc.mst_room_hdrs
                                   select new
                                   {
                                       Value = t1.mrm_id,
                                       Text = t1.mrm_ename
                                   }).ToList();
                DataGridViewComboBoxColumn ColStation = new DataGridViewComboBoxColumn();
                {
                    ColStation.HeaderText = "Station";// Hearder name of column
                    ColStation.ValueMember = "Value";// Add items into Combobox
                    ColStation.DisplayMember = "Text";
                    ColStation.DataPropertyName = "mrm_id";
                    ColStation.DataSource = objroomlist;
                    ColStation.DisplayIndex = 2;
                    ColStation.Width = 100;
                    ColStation.DisplayStyleForCurrentCellOnly = true;
                    ColStation.FlatStyle = FlatStyle.Flat;
                }
                GridConclusionHdr.Columns["ColGriddocresult_tab2_station"].Visible = false;
                GridConclusionHdr.Columns.Add(ColStation);
                //tab3
                ShowPHMSearch(tab3txtSearchPHM.Text.Trim());
                
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }

        }

        //Tab 1 Config Hdr && dtl
        private void LoaddataTab1()
        {
            //DD Search
            var objsitelist = (from t1 in dbc.mst_hpc_sites
                               select new DropdownData
                               {
                                   Code = t1.mhs_id,
                                   Name = t1.mhs_ename
                               }).ToList();
            DropdownData newitem = new DropdownData();
            newitem.Code = 0;
            newitem.Name = "Select All";
            objsitelist.Insert(0, newitem);
            DDSite_SearchTab1.ValueMember = "Code";
            DDSite_SearchTab1.DisplayMember = "Name";
            DDSite_SearchTab1.DataSource = objsitelist;
            DDSite_SearchTab1.SelectedIndex = 0;

            var objsitelist2 = (from t1 in dbc.mst_hpc_sites
                               select new DropdownData
                               {
                                   Code = t1.mhs_id,
                                   Name = t1.mhs_ename
                               }).ToList();
            DDtab1_ConfigHeader.ValueMember = "Code";
            DDtab1_ConfigHeader.DisplayMember = "Name";
            DDtab1_ConfigHeader.DataSource = objsitelist2;

            ShowConfigHeader(0);
        }
       
        private void btnSearch_Tab1_Click(object sender, EventArgs e)
        {
            int siteid = Utility.GetInteger( DDSite_SearchTab1.SelectedValue);
            ShowConfigHeader(siteid);
        }
        private void ShowConfigHeader(int siteid)
        {
            var objconfigHeaderList = (from t1 in dbc.mst_config_hdrs
                                       select t1);
            if (siteid != 0)
            {
                objconfigHeaderList = objconfigHeaderList.Where(x => x.mhs_id == siteid);
            }
            ConfigHeader_bindingSource1.DataSource = objconfigHeaderList;
        }
        private void btnTab1Save_Click(object sender, EventArgs e)
        {
            try
            {
                ConfigHeader_bindingSource1.EndEdit();
                mstconfigdtlsBindingSource.EndEdit();
                dbc.SubmitChanges();
                lbMsgAlertTab1.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }
        }
        private void btnCancel_Tab1_Click(object sender, EventArgs e)
        {
            CancelAllTab();
        }

        private void Ch_configHeader_IsActive_MouseClick(object sender, MouseEventArgs e)
        {
            mst_config_hdr currenthdr = (mst_config_hdr)ConfigHeader_bindingSource1.Current;
            if (currenthdr != null)
            {
                var data = (Ch_configHeader_IsActive.Checked == true) ? 'A' : 'I';
                var statusstr = currenthdr.mfh_status;
                if (data != statusstr)
                {
                    currenthdr.mfh_status = data;
                }
            }
        }
        private void Chtab1_ConfigDetail_IsActive_MouseClick(object sender, MouseEventArgs e)
        {
            mst_config_dtl currentdtl = (mst_config_dtl)mstconfigdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                var data = (Chtab1_ConfigDetail_IsActive.Checked == true) ? 'A' : 'I';
                var statusstr = currentdtl.mfd_status;
                if (data != statusstr)
                {
                    currentdtl.mfd_status = data;
                }
            }
        }
        
        private void btntab1_NewConfig_Header_Click(object sender, EventArgs e)
        {
            ConfigHeader_bindingSource1.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_config_hdr currenthdr = (mst_config_hdr)ConfigHeader_bindingSource1.Current;
            if (currenthdr != null)
            {
                string username = Program.CurrentUser.mut_username;
                int mshid = (from t1 in dbc.mst_hpc_sites select t1.mhs_id).FirstOrDefault();
                currenthdr.mhs_id = mshid;
                currenthdr.mfh_status = 'A';
                currenthdr.mfh_effective_date = dtnow;
                currenthdr.mfh_expire_date = dtnow.AddYears(1);
                currenthdr.mfh_create_by = username;
                currenthdr.mfh_create_date = dtnow;
                currenthdr.mfh_update_by = username;
                currenthdr.mfh_update_date = dtnow;

                Ch_configHeader_IsActive.Checked = true;
            }
        }
        private void btntab1_NewConfigDetail_Click(object sender, EventArgs e)
        {
            mstconfigdtlsBindingSource.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_config_dtl currentdtl = (mst_config_dtl)mstconfigdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                string username = Program.CurrentUser.mut_username;
                int mshid = (from t1 in dbc.mst_hpc_sites select t1.mhs_id).FirstOrDefault();
                currentdtl.mfd_status = 'A';
                currentdtl.mfd_effective_date = dtnow;
                currentdtl.mfd_expire_date = dtnow.AddYears(1);
                currentdtl.mfd_create_by = username;
                currentdtl.mfd_create_date = dtnow;
                currentdtl.mfd_update_by = username;
                currentdtl.mfd_update_date = dtnow;
                
                Chtab1_ConfigDetail_IsActive.Checked = true;
            }
        }

        private void ConfigHeader_bindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_config_hdr currenthdr = (mst_config_hdr)ConfigHeader_bindingSource1.Current;
            if (currenthdr != null)
            {
                //status
                if (currenthdr.mfh_status == 'A')
                    Ch_configHeader_IsActive.Checked = true;
                else
                    Ch_configHeader_IsActive.Checked = false;
            }
        }
        private void ConfigHeader_bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            lbMsgAlertTab1.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnTab1Save_Click(null, null);
                }
                else
                {
                    CancelAllTab();
                    return;
                }
            }
        }
        private void mstconfigdtlsBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            lbMsgAlertTab1.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnTab1Save_Click(null, null);
                }
                else
                {
                    CancelAllTab();
                    return;
                }
            }
        }
        private void mstconfigdtlsBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_config_dtl currentdtl = (mst_config_dtl)mstconfigdtlsBindingSource.Current;
            if (currentdtl != null)
            {
                //status
                if (currentdtl.mfd_status == 'A')
                    Chtab1_ConfigDetail_IsActive.Checked = true;
                else
                    Chtab1_ConfigDetail_IsActive.Checked = false;
            }
            
        }

        private void GridConfigHeader_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridConfigHeader.SetRuningNumber();
        }
        private void GridConfigDetail_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridConfigDetail.SetRuningNumber();
        }
        /*end Tab 1*/


        //Clear AllTab
        private void CancelAllTab()
        {
            dbc.Dispose();
            dbc = new InhCheckupDataContext();
            //load Tab1
            int siteid = Utility.GetInteger(DDSite_SearchTab1.SelectedValue);
            ShowConfigHeader(siteid);
            //load Tab2
            int mrmid = Utility.GetInteger(DDRoomSearch_tab2.SelectedValue);
            ShowConclusion(mrmid);
            //load Tab3
            ShowPHMSearch("");
        }

        //tab 2 conclusion Header & Detail
        private void LoaddataTab2()
        {
            //set dropdown Room
            var objroomlist = (from t1 in dbc.mst_room_hdrs
                               select new DropdownData
                               {
                                   Code = t1.mrm_id,
                                   Name = t1.mrm_ename
                               }).ToList();
            DropdownData newitem = new DropdownData();
            newitem.Code = 0;
            newitem.Name = "Select All";
            objroomlist.Insert(0, newitem);
            DDRoomSearch_tab2.ValueMember = "Code";
            DDRoomSearch_tab2.DisplayMember = "Name";
            DDRoomSearch_tab2.DataSource = objroomlist;
            DDRoomSearch_tab2.SelectedIndex = 0;

            var objsitelist2 = (from t1 in dbc.mst_room_hdrs
                                select new DropdownData
                                {
                                    Code = t1.mrm_id,
                                    Name = t1.mrm_ename
                                }).ToList();
            DDstationTab2Conclusion.ValueMember = "Code";
            DDstationTab2Conclusion.DisplayMember = "Name";
            DDstationTab2Conclusion.DataSource = objsitelist2;
            ShowConclusion(0);
        }
        private void ShowConclusion(int mrm_id)
        {
            var objList = (from t1 in dbc.mst_doc_result_hdrs
                                       select t1);
            if (mrm_id != 0)
            {
                objList = objList.Where(x => x.mrm_id == mrm_id);
            }
            mst_doctor_resultHDR_bindingSource1.DataSource = objList;
        }
        private void btnSearchTab2_Click(object sender, EventArgs e)
        {
            int mrmid = Utility.GetInteger(DDRoomSearch_tab2.SelectedValue);
            ShowConclusion(mrmid);
        }
        private void btnSave_Tab2_Click(object sender, EventArgs e)
        {
            try
            {
                mst_doctor_resultHDR_bindingSource1.EndEdit();
                mst_doctor_results_BindingSource.EndEdit();
                dbc.SubmitChanges();
                lbmsgAlert_tab2.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }
        }
        private void btnCancel_tab2_Click(object sender, EventArgs e)
        {
            CancelAllTab();
        }

        private void Chtab2_hdr_Active_MouseClick(object sender, MouseEventArgs e)
        {
            mst_doc_result_hdr currenthdr = (mst_doc_result_hdr)mst_doctor_resultHDR_bindingSource1.Current;
            if (currenthdr != null)
            {
                var data = (Chtab2_hdr_Active.Checked == true) ? 'A' : 'I';
                var statusstr = currenthdr.mrh_status;
                if (data != statusstr)
                {
                    currenthdr.mrh_status = data;
                }
            }
        }
        private void Chtab2_dtl_Active_MouseClick(object sender, MouseEventArgs e)
        {
            mst_doc_result currentdtl = (mst_doc_result)mst_doctor_results_BindingSource.Current;
            if (currentdtl != null)
            {
                var data = (Chtab2_dtl_Active.Checked == true) ? 'A' : 'I';
                var statusstr = currentdtl.mdr_status;
                if (data != statusstr)
                {
                    currentdtl.mdr_status = data;
                }
            }
        }

        private void btnNewConclusionHeader_Click(object sender, EventArgs e)
        {
            mst_doctor_resultHDR_bindingSource1.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_doc_result_hdr currenthdr = (mst_doc_result_hdr)mst_doctor_resultHDR_bindingSource1.Current;
            if (currenthdr != null)
            {
                string username = Program.CurrentUser.mut_username;
                int mrmid = (from t1 in dbc.mst_room_hdrs select t1.mrm_id).FirstOrDefault();
                currenthdr.mrm_id = mrmid;
                currenthdr.mrh_status = 'A';
                currenthdr.mrh_effective_date = dtnow;
                currenthdr.mrh_expire_date = dtnow.AddYears(1);
                currenthdr.mrh_create_by = username;
                currenthdr.mrh_create_date = dtnow;
                currenthdr.mrh_update_by = username;
                currenthdr.mrh_update_date = dtnow;

                Chtab2_hdr_Active.Checked = true;
            }
        }
        private void btnNewConclusionDetail_Click(object sender, EventArgs e)
        {
            mst_doctor_results_BindingSource.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_doc_result currentdtl = (mst_doc_result)mst_doctor_results_BindingSource.Current;
            if (currentdtl != null)
            {
                string username = Program.CurrentUser.mut_username;
                int mrmid = (from t1 in dbc.mst_room_hdrs select t1.mrm_id).FirstOrDefault();
                currentdtl.mdr_status = 'A';
                currentdtl.mdr_effective_date = dtnow;
                currentdtl.mdr_expire_date = dtnow.AddYears(1);
                currentdtl.mdr_create_by = username;
                currentdtl.mdr_create_date = dtnow;
                currentdtl.mdr_update_by = username;
                currentdtl.mdr_update_date = dtnow;

                Chtab2_dtl_Active.Checked = true;
            }
        }

        private void mst_doctor_resultHDR_bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            lbMsgAlertTab1.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnSave_Tab2_Click(null, null);
                }
                else
                {
                    CancelAllTab();
                    return;
                }
            }
        }
        private void mst_doctor_resultHDR_bindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_doc_result_hdr currenthdr = (mst_doc_result_hdr)mst_doctor_resultHDR_bindingSource1.Current;
            if (currenthdr != null)
            {
                //status
                if (currenthdr.mrh_status == 'A')
                    Chtab2_hdr_Active.Checked = true;
                else
                    Chtab2_hdr_Active.Checked = false;
            }
        }
        private void mst_doctor_results_BindingSource_CurrentChanged(object sender, EventArgs e)
        {
            lbMsgAlertTab1.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    btnSave_Tab2_Click(null, null);
                }
                else
                {
                    CancelAllTab();
                    return;
                }
            }
        }
        private void mst_doctor_results_BindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            mst_doc_result currentdtl = (mst_doc_result)mst_doctor_results_BindingSource.Current;
            if (currentdtl != null)
            {
                //status
                if (currentdtl.mdr_status == 'A')
                    Chtab2_dtl_Active.Checked = true;
                else
                    Chtab2_dtl_Active.Checked = false;
            }
        }

        private void GridConclusionHdr_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridConclusionHdr.SetRuningNumber();
        }
        private void GridConclusionDtl_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            GridConclusionDtl.SetRuningNumber();
        }

        //end Tab 2

        /*Tab 3*/
        private void tab3PHM_Hdr_ChActive_Click(object sender, EventArgs e)
        {
            mst_phm_cfg_hdr currenthdr = (mst_phm_cfg_hdr)PHM_hdr_bindingSource1.Current;
            if (currenthdr != null)
            {
                var data = (tab3PHM_Hdr_ChActive.Checked == true) ? 'A' : 'I';
                var statusstr = currenthdr.mph_status;
                if (data != statusstr)
                {
                    currenthdr.mph_status = data;
                }
            }
        }

        private void tab3btnSearch_Click(object sender, EventArgs e)
        {
            ShowPHMSearch(tab3txtSearchPHM.Text.Trim());
        }
        private void tab3btnSearchClear_Click(object sender, EventArgs e)
        {
            tab3txtSearchPHM.Text = "";
            ShowPHMSearch("");
        }

        private void ShowPHMSearch(String phmName)
        {
            var objList = (from t1 in dbc.mst_phm_cfg_hdrs select t1);
            if (phmName != "")
            {
                objList = objList.Where(x => x.mph_tname.Contains(phmName) || x.mph_ename.Contains(phmName));
            }
            PHM_hdr_bindingSource1.DataSource = objList;
        }

        private void tab3PHM_Hdr_btnNew_Click(object sender, EventArgs e)
        {
            PHM_hdr_bindingSource1.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_phm_cfg_hdr currenthdr = (mst_phm_cfg_hdr)PHM_hdr_bindingSource1.Current;
            if (currenthdr != null)
            {
                string username = Program.CurrentUser.mut_username;
                currenthdr.mph_status = 'A';
                currenthdr.mph_effective_date = dtnow;
                currenthdr.mph_expire_date = dtnow.AddYears(1);
                currenthdr.mph_create_by = username;
                currenthdr.mph_create_date = dtnow;
                currenthdr.mph_update_by = username;
                currenthdr.mph_update_date = dtnow;

                tab3PHM_Hdr_ChActive.Checked = true;
            }
        }
        private void tab3PHM_dtl_btnNew_Click(object sender, EventArgs e)
        {
            mstphmcfgdtlsBindingSource1.AddNew();
            DateTime dtnow = Program.GetServerDateTime();
            mst_phm_cfg_dtl currentdtl = (mst_phm_cfg_dtl)mstphmcfgdtlsBindingSource1.Current;
            if (currentdtl != null)
            {
                string username = Program.CurrentUser.mut_username;
                currentdtl.mpd_create_by = username;
                currentdtl.mpd_create_date = dtnow;
                currentdtl.mpd_update_by = username;
                currentdtl.mpd_update_date = dtnow;
            }
        }

        private void tab3PHMbtnsave_Click(object sender, EventArgs e)
        {
            try
            {
                PHM_hdr_bindingSource1.EndEdit();
                mstphmcfgdtlsBindingSource1.EndEdit();
                dbc.SubmitChanges();
                tab3PHMAlertmsg.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                Program.MessageError(ex.Message);
            }
        }
        private void tab3PHMbtnCancel_Click(object sender, EventArgs e)
        {
            CancelAllTab();
        }

        private void tab3PHM_dtl_GridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tab3PHM_dtl_GridView.SetRuningNumber();
        }
        private void tab3PHM_Hdr_GridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            tab3PHM_Hdr_GridView.SetRuningNumber();
        }

        private void PHM_hdr_bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            tab3PHMAlertmsg.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tab3PHMbtnsave_Click(null, null);
                }
                else
                {
                    CancelAllTab();
                    return;
                }
            }
        }
        private void PHM_hdr_bindingSource1_CurrentItemChanged(object sender, EventArgs e)
        {
           
                mst_phm_cfg_hdr currenthdr = (mst_phm_cfg_hdr)PHM_hdr_bindingSource1.Current;
                if (currenthdr != null)
                {
                    //status
                    if (currenthdr.mph_status == 'A')
                        tab3PHM_Hdr_ChActive.Checked = true;
                    else
                        tab3PHM_Hdr_ChActive.Checked = false;
                }
        }

        private void mstphmcfgdtlsBindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            tab3PHMAlertmsg.Text = "";
            if (dbc.GetChangeSet().Updates.Count > 0
                || dbc.GetChangeSet().Inserts.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data.", "Confirm Save", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    tab3PHMbtnsave_Click(null, null);
                }
                else
                {
                    CancelAllTab();
                    return;
                }
            }
        }

    }
}
