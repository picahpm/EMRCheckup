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
    public partial class EchoUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public EchoUC()
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
                trn_doctor_echo ec = bsDoctorEcho.OfType<trn_doctor_echo>().FirstOrDefault();
                if (ec != null)
                {
                    if (e == null)
                    {
                        ec.trec_cardiologist_code = null;
                        ec.trec_cardiologist_license = null;
                        ec.trec_cardiologist_name_en = null;
                        ec.trec_cardiologist_name_th = null;
                    }
                    else
                    {
                        ec.trec_cardiologist_code = ((DoctorProfile)e).SSUSR_Initials;
                        ec.trec_cardiologist_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        ec.trec_cardiologist_name_en = dn.NameEN;
                        ec.trec_cardiologist_name_th = dn.NameTH;
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
                        trn_doctor_echo DoctorEcho = DoctorHdr.trn_doctor_echos.FirstOrDefault();
                        if (DoctorEcho == null)
                        {
                            DoctorEcho = new trn_doctor_echo();
                            DoctorEcho.trec_create_by = username;
                            DoctorEcho.trec_create_date = DateTime.Now;
                            DoctorHdr.trn_doctor_echos.Add(DoctorEcho);
                        }
                        DoctorEcho.trec_update_by = username;
                        DoctorEcho.trec_update_date = DateTime.Now;
                        linkPDF = value.trn_patient.trn_patient_history_echos.Where(x => x.tphc_en_no == value.tpr_en_no).Select(x => x.tphc_link).FirstOrDefault();
                        favoriteTextBox1.Text = DoctorEcho.trec_doc_result_thai;
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        autoCompleteUC1.SelectedValue = DoctorEcho.trec_cardiologist_code;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
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
                    //switch (this._Language)
                    //{
                    //    case Language.TH:
                    //        cbmConclusionECHO.DisplayMember = "langThai";
                    //        txtremark.DataBindings.Add(new Binding("Text", bsDoctorEcho, "trec_doc_result_thai"));
                    //        break;
                    //    case Language.EN:
                    //        cbmConclusionECHO.DisplayMember = "langEng";
                    //        txtremark.DataBindings.Add(new Binding("Text", bsDoctorEcho, "trec_doc_result_eng"));
                    //        break;
                    //}
                    //_Language = value;
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
                trn_doctor_echo DoctorEcho = DoctorHdr.trn_doctor_echos.FirstOrDefault();
                if (DoctorEcho == null)
                {
                    DoctorEcho = new trn_doctor_echo();
                    DoctorHdr.trn_doctor_echos.Add(DoctorEcho);
                }
                //DoctorEcho.PropertyChanged += new PropertyChangedEventHandler(DoctorEcho_PropertyChanged);

                DoctorEcho.trec_doc_result_thai = favoriteTextBox1.Text;
                DoctorEcho.trec_doc_result_eng = favoriteTextBox1.Text;
                
               
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
            //try
            //{
            //    using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            //    {
            //        trn_doctor_echo DoctorEcho = bsDoctorEcho.OfType<trn_doctor_echo>().FirstOrDefault();
            //        if (DoctorEcho != null)
            //        {
            //            var objevent = (from t1 in dbc.mst_events
            //                            where t1.mvt_code == "EC"
            //                            select t1).FirstOrDefault();

            //            var objcurrentpatientplan = (from t1 in dbc.trn_patient_plans
            //                                         where t1.tpr_id == this._PatientRegis.tpr_id
            //                                         && t1.mvt_id == objevent.mvt_id
            //                                         && t1.tpl_use_pac == true
            //                                         select t1).FirstOrDefault();
            //            if (objcurrentpatientplan != null)
            //            {
            //                string filename = objcurrentpatientplan.tpl_txt_others;
            //                int mhs_id = objcurrentpatientplan.trn_patient_regi.mhs_id;
            //                var datenow = Program.GetServerDateTime().Date;
            //                var objpathodata = (from t1 in dbc.mst_config_dtls
            //                                    where t1.mst_config_hdr.mhs_id == mhs_id
            //                                    && t1.mst_config_hdr.mfh_code == "PAO"
            //                                    && (t1.mst_config_hdr.mfh_status == 'A'
            //                                        && (t1.mst_config_hdr.mfh_effective_date == null ||
            //                                                (t1.mst_config_hdr.mfh_effective_date.Value.Date <= datenow
            //                                                && (t1.mst_config_hdr.mfh_expire_date == null ||
            //                                                    t1.mst_config_hdr.mfh_expire_date.Value.Date >= datenow))
            //                                            )
            //                                        )
            //                                    && (t1.mfd_status == 'A'
            //                                        && (t1.mfd_effective_date == null ||
            //                                                (t1.mfd_effective_date.Value.Date <= datenow
            //                                                && (t1.mfd_expire_date == null ||
            //                                                    t1.mfd_expire_date.Value.Date >= datenow)
            //                                                )
            //                                            )
            //                                            )
            //                                    select t1).FirstOrDefault();
            //                if (objpathodata != null)
            //                {
            //                    string serverPath = objpathodata.mfd_text;
            //                    //--new 08/09/2015 edit by M
            //                    string a = (serverPath + filename).Replace(@"\", @"/");
            //                    System.Diagnostics.Process.Start(a);
            //                    //--old 08/09/2015 edit by M
            //                    //ViewPDF frmview = new ViewPDF();
            //                    //frmview.PathFile = serverPath + filename;
            //                    //frmview.ShowDialog();
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Program.MessageError(this.Name, "ShowPDF", ex, false);
            //}
        }

        private void btnAddConclusionECHO_Click(object sender, EventArgs e)
        {
            //if (cbmConclusionECHO.Text != null && cbmConclusionECHO.Text != string.Empty && cbmConclusionECHO.Text != "")
            //{
            //    trn_doctor_hdr DoctorHdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            //    if (DoctorHdr == null)
            //    {
            //        DoctorHdr = new trn_doctor_hdr();
            //        _PatientRegis.trn_doctor_hdrs.Add(DoctorHdr);
            //    }
            //    trn_doctor_echo DoctorEcho = DoctorHdr.trn_doctor_echos.FirstOrDefault();
            //    if (DoctorEcho == null)
            //    {
            //        DoctorEcho = new trn_doctor_echo();
            //        DoctorHdr.trn_doctor_echos.Add(DoctorEcho);
            //    }
            //    //DoctorEcho.PropertyChanged += new PropertyChangedEventHandler(DoctorEcho_PropertyChanged);
            //    if (DoctorEcho.trec_doc_result_thai != null && DoctorEcho.trec_doc_result_thai != string.Empty && DoctorEcho.trec_doc_result_thai != "")
            //    {
            //        DoctorEcho.trec_doc_result_thai += "\r\n" + cbmConclusionECHO.Text;
            //    }
            //    else { DoctorEcho.trec_doc_result_thai += cbmConclusionECHO.Text; }
            //    this.cbmConclusionECHO.SelectedValue = "";
            //    cbmConclusionECHO.Focus();
            //}
            //else { cbmConclusionECHO.Focus(); }
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
