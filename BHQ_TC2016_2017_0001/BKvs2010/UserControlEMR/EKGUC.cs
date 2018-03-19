using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;
using BKvs2010.Usercontrols;

namespace BKvs2010.UserControlEMR
{
    public partial class EKGUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public EKGUC()
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
                trn_doctor_ekg ekg = bsDoctorEKG.OfType<trn_doctor_ekg>().FirstOrDefault();
                if (ekg != null)
                {
                    if (e == null)
                    {
                        ekg.trek_cardiologist_code = null;
                        ekg.trek_cardiologist_license = null;
                        ekg.trek_cardiologist_name_en = null;
                        ekg.trek_cardiologist_name_th = null;
                    }
                    else
                    {
                        ekg.trek_cardiologist_code = ((DoctorProfile)e).SSUSR_Initials;
                        ekg.trek_cardiologist_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        ekg.trek_cardiologist_name_en = dn.NameEN;
                        ekg.trek_cardiologist_name_th = dn.NameTH;
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

                        var objconclusion = new EmrClass.FunctionDataCls().getDoctorResult(value.mhs_id, "DC", "DCEK")
                                            .Select(x => new clsSourceMaster
                                            {
                                                val = x.mdr_id,
                                                langThai = x.mdr_ename,
                                                langEng = x.mdr_ename
                                            }).ToList();
                        objconclusion.Insert(0, new clsSourceMaster { val = null, langThai = "", langEng = "" });
                       // cbmConclusionEKG.DataSource = objconclusion;
                        switch (this._Language)
                        {
                            case Language.TH:
                             //   cbmConclusionEKG.DisplayMember = "langThai";
                                break;
                            case Language.EN:
                               // cbmConclusionEKG.DisplayMember = "langEng";
                                break;
                        }
                       // cbmConclusionEKG.ValueMember = "val";


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
                        trn_doctor_ekg DoctorEKG = DoctorHdr.trn_doctor_ekgs.FirstOrDefault();
                        if (DoctorEKG == null)
                        {
                            DoctorEKG = new trn_doctor_ekg();
                            DoctorEKG.trek_create_by = username;
                            DoctorEKG.trek_create_date = DateTime.Now;
                            DoctorHdr.trn_doctor_ekgs.Add(DoctorEKG);
                        }
                        DoctorEKG.trek_update_by = username;
                        DoctorEKG.trek_update_date = DateTime.Now;
                        //DoctorEcho.PropertyChanged += new PropertyChangedEventHandler(DoctorEcho_PropertyChanged);
                        linkPDF = value.trn_patient.trn_patient_history_ekgs.Where(x => x.tphk_en_no == value.tpr_en_no).Select(x => x.tphk_link).FirstOrDefault();
                        favoriteTextBox1.Text = DoctorEKG.trek_doc_result_thai;
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        autoCompleteUC1.SelectedValue = DoctorEKG.trek_cardiologist_code;
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
                    //txtremark.DataBindings.Clear();
                    switch (this._Language)
                    {
                        case Language.TH:
                           // cbmConclusionEKG.DisplayMember = "langThai";
                           // txtremark.DataBindings.Add(new Binding("Text", bsDoctorEKG, "trek_doc_result_thai"));
                            break;
                        case Language.EN:
                          //  cbmConclusionEKG.DisplayMember = "langEng";
                          //  txtremark.DataBindings.Add(new Binding("Text", bsDoctorEKG, "trek_doc_result_eng"));
                            break;
                    }
                    _Language = value;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowPDF();
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        { 
          
                trn_doctor_hdr DoctorHdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
                if (DoctorHdr == null)
                {
                    DoctorHdr = new trn_doctor_hdr();
                    _PatientRegis.trn_doctor_hdrs.Add(DoctorHdr);
                }
                trn_doctor_ekg DoctorEKG = DoctorHdr.trn_doctor_ekgs.FirstOrDefault();
                if (DoctorEKG == null)
                {
                    DoctorEKG = new trn_doctor_ekg();
                    DoctorHdr.trn_doctor_ekgs.Add(DoctorEKG);
                }
                //DoctorEcho.PropertyChanged += new PropertyChangedEventHandler(DoctorEcho_PropertyChanged);
               // if (DoctorEKG.trek_doc_result_thai != null && DoctorEKG.trek_doc_result_thai != string.Empty && DoctorEKG.trek_doc_result_thai != "")
               // {
                    //DoctorEKG.trek_doc_result_thai += "\r\n" + cbmConclusionEKG.Text;
                    DoctorEKG.trek_doc_result_thai = favoriteTextBox1.Text;
                    DoctorEKG.trek_doc_result_eng = favoriteTextBox1.Text;
         
               // }
            //    else { DoctorEKG.trek_doc_result_thai += cbmConclusionEKG.Text; }
            //    this.cbmConclusionEKG.SelectedValue = "";
            //    cbmConclusionEKG.Focus();
            //}
            //else { cbmConclusionEKG.Focus(); }
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
        private string linkPDF;
        private void ShowPDF()
        {
            try
            {
                System.Diagnostics.Process.Start(linkPDF);
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "ShowPDF()", ex, false);
            }
        }

        private void btnAddConclusionEKG_Click(object sender, EventArgs e)
        {
            //if (cbmConclusionEKG.Text != null && cbmConclusionEKG.Text != string.Empty && cbmConclusionEKG.Text != "")
            //{
            //    trn_doctor_hdr DoctorHdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            //    if (DoctorHdr == null)
            //    {
            //        DoctorHdr = new trn_doctor_hdr();
            //        _PatientRegis.trn_doctor_hdrs.Add(DoctorHdr);
            //    }
            //    trn_doctor_ekg DoctorEKG = DoctorHdr.trn_doctor_ekgs.FirstOrDefault();
            //    if (DoctorEKG == null)
            //    {
            //        DoctorEKG = new trn_doctor_ekg();
            //        DoctorHdr.trn_doctor_ekgs.Add(DoctorEKG);
            //    }
            //    //DoctorEcho.PropertyChanged += new PropertyChangedEventHandler(DoctorEcho_PropertyChanged);
            //    if (DoctorEKG.trek_doc_result_thai != null && DoctorEKG.trek_doc_result_thai != string.Empty && DoctorEKG.trek_doc_result_thai != "")
            //    {
            //        DoctorEKG.trek_doc_result_thai += "\r\n" + cbmConclusionEKG.Text;
            //    }
            //    else { DoctorEKG.trek_doc_result_thai += cbmConclusionEKG.Text; }
            //    this.cbmConclusionEKG.SelectedValue = "";
            //    cbmConclusionEKG.Focus();
            //}
            //else { cbmConclusionEKG.Focus(); }
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
