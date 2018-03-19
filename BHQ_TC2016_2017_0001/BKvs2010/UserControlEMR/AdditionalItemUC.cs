using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class AdditionalItemUC : UserControl
    {
        List<PackageCls> package = new List<PackageCls>();
        List<ObjCompany> objCom = new List<ObjCompany>();
        public AdditionalItemUC()
        {
            InitializeComponent();
            gvAdditionItem.AutoGenerateColumns = false;
            gvAdditionItem.DataSource = bsPatientAddItem;            
            PrepareData();
            EmrClass.GetDataToDoListCls cls = new EmrClass.GetDataToDoListCls();
            objCom = cls.getListCompany();
        }

        private class PackageCls
        {
            public int? id { get; set; }
            public string packageName { get; set; }
        }
        public void PrepareData()
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                   
                    package = cdc.mst_health_checkups
                                                  .Where(x => x.mhc_status == 'A')
                                                  .Select(x => new PackageCls
                                                  {
                                                      id = x.mhc_id,
                                                      packageName = x.mhc_tname
                                                  }).ToList();

                    PackageAC.DataSource = package;
                    PackageAC.DisplayMember = "packageName";
                    PackageAC.ValueMember = "id";                   
                   
                }
            }
            catch
            {

            }
        }

        private trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value == null)
                {
                    Clear();
                    panel1.Enabled = false;
                }
                else
                {
                    try
                    {
                        Clear();
                        trn_patient_book_hdr patientBookHdr = value.trn_patient_book_hdrs
                                                                   .Where(x => x.tpbh_language == _language)
                                                                   .FirstOrDefault();
                        if (patientBookHdr == null)
                        {
                            patientBookHdr = new trn_patient_book_hdr();
                            patientBookHdr.tpbh_language = _language;
                            value.trn_patient_book_hdrs.Add(patientBookHdr);
                        }
                        List<trn_patient_add_item> patientAddition = value.trn_patient_add_items.ToList();
                        foreach (trn_patient_add_item item in patientAddition)
                        {
                            try
                            {
                                foreach (CheckBox cb in groupBox5.Controls)
                                {
                                    try
                                    {
                                        int? tag = (int?)Convert.ToInt32(cb.Tag);
                                        if (tag == item.tpai_seq)
                                        {
                                            cb.Checked = true;
                                            break;
                                        }
                                    }
                                    catch
                                    {

                                    }
                                }
                            }
                            catch
                            {

                            }
                        }
                        PackageAC.Text = patientBookHdr.tpbh_package_name;
                        GridPackage.AutoGenerateColumns = false;
                        GridPackage.Columns["ColPackageName"].DataPropertyName = "tos_od_set_name";
                        GridPackage.AutoGenerateColumns = false;
                        GridPackage.DataSource = value.trn_patient_order_sets.Where(x => x.tos_status == true).ToList();
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        panel1.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "patientRegis", ex, false);
                    }
                }
            }
        }

        public void Clear()
        {
            foreach (CheckBox cb in groupBox5.Controls)
            {
                try
                {
                    cb.Checked = false;
                }
                catch
                {

                }
            }
            this.Enabled = false;
            GridPackage.AutoGenerateColumns = false;
            GridPackage.DataSource = new List<trn_patient_order_set>();
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            DateTime dateNow = Program.GetServerDateTime();
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
        }

        private void chkItem_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int? seq = (int?)Convert.ToInt32(cb.Tag);

            if (cb.Checked)
            {
                trn_patient_add_item item = new trn_patient_add_item
                {
                    tpai_seq = seq,
                    tpai_add_item = cb.Text
                };
                bsPatientAddItem.Add(item);
                bsPatientAddItem.EndEdit();
            }
            else
            {
                trn_patient_add_item item = bsPatientAddItem.OfType<trn_patient_add_item>()
                                                            .Where(x => x.tpai_seq == seq)
                                                            .FirstOrDefault();
                bsPatientAddItem.Remove(item);
                bsPatientAddItem.EndEdit();
            }
        }

        private string _language = "TH";
        public string language
        {
            get { return _language; }
            set
            {
                if (value != _language)
                {
                    _language = value;
                }
            }
        }

        private void gvAdditionItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (dgv.Columns[e.ColumnIndex].Name == "colDel")
            {
                trn_patient_add_item item = (trn_patient_add_item)dgv.Rows[e.RowIndex].DataBoundItem;
                bsPatientAddItem.Remove(item);
                bsPatientAddItem.EndEdit();
                foreach (CheckBox cb in groupBox5.Controls)
                {
                    try
                    {
                        int? tag = (int?)Convert.ToInt32(cb.Tag);
                        if (tag == item.tpai_seq)
                        {
                            cb.Checked = false;
                            break;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        private void btnAdditionItem_Click(object sender, EventArgs e)
        {
            string txt = txtAdditionItem.Text.Trim();
            if (txt.Length > 0)
            {
                trn_patient_add_item item = new trn_patient_add_item
                {
                    tpai_add_item = txt
                };
                bsPatientAddItem.Add(item);
                bsPatientAddItem.EndEdit();
                txtAdditionItem.Text = "";
            }
        }

        private void chkCompanyLoad_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            //int? seq = (int?)Convert.ToInt32(cb.Tag);

            if (cb.Checked)
            {

                PackageAC.DataSource = objCom;
                PackageAC.ValueMember = "code";
                PackageAC.DisplayMember = "name";
                //PackageAC.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(PakageAC_SelectedValueChanged);

            }
            else
            {
                PackageAC.DataSource = package;
                PackageAC.DisplayMember = "packageName";
                PackageAC.ValueMember = "id";
                //PackageAC.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(PakageAC_SelectedValueChanged);
            }
        }
    }
}
