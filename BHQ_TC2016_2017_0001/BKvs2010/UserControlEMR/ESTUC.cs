using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;
using BKvs2010.Usercontrols;

namespace BKvs2010.UserControlEMR
{
    public partial class ESTUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public ESTUC()
        {
            InitializeComponent();

            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);

            List<mst_conclusion_favorite_dtl> mst = new FavoriteCls().getFavorite(favoriteTextBox1.FavoriteOrder, favoriteTextBox1.FavoriteType);
            favoriteTextBox1.AutoCompleteListThList = mst.Select(x => x.mcfd_description).ToList();
            favoriteTextBox1.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
        }
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, username);
                txtBox.AutoCompleteListThList.Remove(e);
            }
        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_doctor_est es = bsDoctorEST.OfType<trn_doctor_est>().FirstOrDefault();
                if (es != null)
                {
                    if (e == null)
                    {
                        es.tres_cardiologist_code = null;
                        es.tres_cardiologist_license = null;
                        es.tres_cardiologist_name_en = null;
                        es.tres_cardiologist_name_th = null;
                    }
                    else
                    {
                        es.tres_cardiologist_code = ((DoctorProfile)e).SSUSR_Initials;
                        es.tres_cardiologist_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        es.tres_cardiologist_name_en = dn.NameEN;
                        es.tres_cardiologist_name_th = dn.NameTH;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }

        private string username = null;
        public trn_patient_regi _PatientRegis;
        public trn_patient_regi PatientRegis
        {
            get { return _PatientRegis; }
            set
            {
                if (value == null)
                {
                    Clear();
                }
                else
                {
                    try
                    {
                        username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

                        trn_doctor_hdr DoctorHdr = value.trn_doctor_hdrs.FirstOrDefault();
                        if (DoctorHdr == null)
                        {
                            DoctorHdr = new trn_doctor_hdr();
                            DoctorHdr.trh_create_by = username;
                            DoctorHdr.trh_create_date = DateTime.Now;
                            value.trn_doctor_hdrs.Add(DoctorHdr);
                        }
                        DoctorHdr.trh_update_by = username;
                        DoctorHdr.trh_update_date = DateTime.Now;
                        trn_doctor_est DoctorEST = DoctorHdr.trn_doctor_ests.FirstOrDefault();
                        if (DoctorEST == null)
                        {
                            DoctorEST = new trn_doctor_est();
                            DoctorEST.tres_create_by = username;
                            DoctorEST.tres_create_date = DateTime.Now;
                            DoctorHdr.trn_doctor_ests.Add(DoctorEST);
                        }
                        DoctorEST.tres_update_by = username;
                        DoctorEST.tres_update_date = DateTime.Now;
                        favoriteTextBox1.Text = DoctorEST.tres_doc_result_thai;
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        autoCompleteUC1.SelectedValue = DoctorEST.tres_cardiologist_code;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }

        private class clsSourceMaster
        {
            public int? val { get; set; }
            public string langThai { get; set; }
            public string langEng { get; set; }
        }
        private Language _Language = Language.TH;
        public Language Language
        {
            get { return _Language; }
            set
            {
                if (value != _Language)
                {
                   // txtremark.DataBindings.Clear();
                    switch (this._Language)
                    {
                        case Language.TH:
                           // cbmConclusionEST.DisplayMember = "langThai";
                           // txtremark.DataBindings.Add(new Binding("Text", bsDoctorEST, "tres_doc_result_thai"));
                            break;
                        case Language.EN:
                            //cbmConclusionEST.DisplayMember = "langEng";
                            //txtremark.DataBindings.Add(new Binding("Text", bsDoctorEST, "tres_doc_result_eng"));
                            break;
                    }
                    _Language = value;
                }
            }
        }

        public void EndEdit()
        {
            trn_doctor_hdr DoctorHdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            if (DoctorHdr == null)
            {
                DoctorHdr = new trn_doctor_hdr();
                _PatientRegis.trn_doctor_hdrs.Add(DoctorHdr);
            }
            trn_doctor_est DoctorEST = DoctorHdr.trn_doctor_ests.FirstOrDefault();
            if (DoctorEST == null)
            {
                DoctorEST = new trn_doctor_est();
                DoctorHdr.trn_doctor_ests.Add(DoctorEST);
            }

            DoctorEST.tres_doc_result_thai = favoriteTextBox1.Text;
            DoctorEST.tres_doc_result_eng = favoriteTextBox1.Text;
        }
        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }

        private int? ConvertToInt(object value)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }
                else
                {
                    return Convert.ToInt32(value);
                }
            }
            catch
            {
                return null;
            }
        }

        private void favoriteTextBox1_btnFavoriteClick(object sender, EventArgs e)
        {
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null)
            {
                bool savefav = new FavoriteCls().saveFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, txtBox.Text, username);
                if (savefav)
                {
                    txtBox.AutoCompleteListThList.Add(txtBox.Text);
                    MessageBox.Show("Add '" + txtBox.Text + "' to favorite Complete.");
                }
            }
        }
    }
}
