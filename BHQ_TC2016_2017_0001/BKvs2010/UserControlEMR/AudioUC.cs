using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.EmrClass;
using BKvs2010.Usercontrols;

namespace BKvs2010.UserControlEMR
{
    public partial class AudioUC : UserControl
    {
        AutoCompleteDoctor obj = new AutoCompleteDoctor();
        public AudioUC()
        {
            InitializeComponent();
            List<clsSourceAutoCompleteDoctor> sourceDoctor = new EmrClass.FunctionDataCls().getSourceDoctor();
            autoCompleteUC1.DataSource = sourceDoctor;
            autoCompleteUC1.DisplayMember = "dis";
            autoCompleteUC1.ValueMember = "val";

            autoCompleteUC1.DataSource = obj.GetDoctorData();
            autoCompleteUC1.ValueMember = "SSUSR_Initials";
            autoCompleteUC1.DisplayMember = "CTPCP_Desc";
            autoCompleteUC1.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteUC1_SelectedValueChanged);


            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                favhearingr.AutoCompleteListThList = cdc.mst_conclusion_favorite_dtls
                                                             .Where(x => x.mcfd_active == true && x.mst_conclusion_favorite_hdr.mcfh_order == favhearingr.FavoriteOrder
                                                             && x.mst_conclusion_favorite_hdr.mcfh_type == favhearingr.FavoriteType)
                                                             .Select(x => x.mcfd_description).ToList();
                favhearingl.AutoCompleteListThList = cdc.mst_conclusion_favorite_dtls
                                                             .Where(x => x.mcfd_active == true && x.mst_conclusion_favorite_hdr.mcfh_order == favhearingl.FavoriteOrder
                                                             && x.mst_conclusion_favorite_hdr.mcfh_type == favhearingl.FavoriteType)
                                                             .Select(x => x.mcfd_description).ToList();
            }
            favhearingl.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
            favhearingr.RightClickDropDown += favoriteTextBox1_DeleteFavorite;
        }
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, user_name);
                txtBox.AutoCompleteListThList.Remove(e);
            }
        }
        private void autoCompleteUC1_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_audiometric_hdr patientAudio = bsPatientAudio.OfType<trn_audiometric_hdr>().FirstOrDefault();
                if (patientAudio != null)
                {
                    if (e == null)
                    {
                        txtDoctorCode.Text = "";
                        patientAudio.tdh_doctor_code = null;
                        patientAudio.tdh_doctor_license = null;
                        patientAudio.tdh_doctor_name_en = null;
                        patientAudio.tdh_doctor_name_th = null;
                    }
                    else
                    {
                        txtDoctorCode.Text = ((DoctorProfile)e).SSUSR_Initials;
                        patientAudio.tdh_doctor_code = ((DoctorProfile)e).SSUSR_Initials;
                        patientAudio.tdh_doctor_license = ((DoctorProfile)e).CTPCP_SMCNo;
                        DoctorName dn = obj.GetDoctorName(((DoctorProfile)e).CTPCP_Desc);
                        patientAudio.tdh_doctor_name_en = dn.NameEN;
                        patientAudio.tdh_doctor_name_th = dn.NameTH;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteUC1_SelectedValueChanged", ex, false);
            }
        }
        private class clsSourceMaster
        {
            public int val { get; set; }
            public string dis { get; set; }
        }
        List<string> rightAir = new List<string> {
            "tdh_right_level_500",
            "tdh_right_level_1000",
            "tdh_right_level_2000"
        };
        List<string> leftAir = new List<string> {
            "tdh_left_level_500",
            "tdh_left_level_1000",
            "tdh_left_level_2000"
        };
        List<string> rightHigh = new List<string> {
            "tdh_right_level_3000",
            "tdh_right_level_4000",
            "tdh_right_level_6000"
        };
        List<string> leftHigh = new List<string> {
            "tdh_left_level_3000",
            "tdh_left_level_4000",
            "tdh_left_level_6000"
        };

        private bool _isDoctorRoom = false;
        public bool isDoctorRoom
        {
            get { return _isDoctorRoom; }
            set { _isDoctorRoom = value; }
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
                }
                else
                {
                    try
                    {
                        trn_audiometric_hdr patientAudio = value.trn_audiometric_hdrs.FirstOrDefault();
                        if (patientAudio == null)
                        {
                            patientAudio = new trn_audiometric_hdr();
                            value.trn_audiometric_hdrs.Add(patientAudio);
                        }

                        patientAudio.PropertyChanged -= new PropertyChangedEventHandler(patientAudio_PropertyChanged);
                        patientAudio.PropertyChanged += new PropertyChangedEventHandler(patientAudio_PropertyChanged);
                        favhearingl.Text = patientAudio.tdh_doc_result_left_thai;
                        favhearingr.Text = patientAudio.tdh_doc_result_right_thai;
                        bsPatientRegis.DataSource = value;
                        btnGraph_Click(null, null);
                        _PatientRegis = value;
                        this.Enabled = true;

                        if (_isDoctorRoom == true)
                        {
                            autoCompleteUC1.SelectedValue = Program.CurrentUser.mut_username;
                            autoCompleteUC1.Enabled = false;
                            txtDoctorCode.Enabled = false;
                        }
                        else
                        {
                            autoCompleteUC1.SelectedValue = patientAudio.tdh_doctor_code;
                        }
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                    
                }
            }
        }
        private void patientAudio_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (rightAir.Contains(e.PropertyName))
            {
                trn_patient_regi patient_regis = (trn_patient_regi)bsPatientRegis.Current;
                trn_audiometric_hdr patient_audio = patient_regis.trn_audiometric_hdrs.FirstOrDefault();
                if (patient_audio != null)
                {
                    int Rt500 = patient_audio.tdh_right_level_500 == null ? 0 : (int)patient_audio.tdh_right_level_500;
                    int Rt1000 = patient_audio.tdh_right_level_1000 == null ? 0 : (int)patient_audio.tdh_right_level_1000;
                    int Rt2000 = patient_audio.tdh_right_level_2000 == null ? 0 : (int)patient_audio.tdh_right_level_2000;

                    if (Rt500 > 0 || Rt1000 > 0 || Rt2000 > 0)
                    {
                        double result = Math.Round((double)((Rt500 + Rt1000 + Rt2000) / 3.0));
                        patient_audio.tdh_right_air_pt_ave = Convert.ToDecimal(result);
                    }
                    else
                    {
                        patient_audio.tdh_right_air_pt_ave = null;
                    }
                }
            }
            else if (leftAir.Contains(e.PropertyName))
            {
                trn_patient_regi patient_regis = (trn_patient_regi)bsPatientRegis.Current;
                trn_audiometric_hdr patient_audio = patient_regis.trn_audiometric_hdrs.FirstOrDefault();
                if (patient_audio != null)
                {
                    int Lt500 = patient_audio.tdh_left_level_500 == null ? 0 : (int)patient_audio.tdh_left_level_500;
                    int Lt1000 = patient_audio.tdh_left_level_1000 == null ? 0 : (int)patient_audio.tdh_left_level_1000;
                    int Lt2000 = patient_audio.tdh_left_level_2000 == null ? 0 : (int)patient_audio.tdh_left_level_2000;

                    if (Lt500 > 0 || Lt1000 > 0 || Lt2000 > 0)
                    {
                        double result = Math.Round((double)((Lt500 + Lt1000 + Lt2000) / 3.0));
                        patient_audio.tdh_left_air_pt_ave = Convert.ToDecimal(result);
                    }
                    else
                    {
                        patient_audio.tdh_left_air_pt_ave = null;
                    }
                }
            }
            else if (rightHigh.Contains(e.PropertyName))
            {
                trn_patient_regi patient_regis = (trn_patient_regi)bsPatientRegis.Current;
                trn_audiometric_hdr patient_audio = patient_regis.trn_audiometric_hdrs.FirstOrDefault();
                if (patient_audio != null)
                {
                    int Rt3000 = patient_audio.tdh_right_level_3000 == null ? 0 : (int)patient_audio.tdh_right_level_3000;
                    int Rt5000 = patient_audio.tdh_right_level_4000 == null ? 0 : (int)patient_audio.tdh_right_level_4000;
                    int Rt6000 = patient_audio.tdh_right_level_6000 == null ? 0 : (int)patient_audio.tdh_right_level_6000;

                    if (Rt3000 > 0 || Rt5000 > 0 || Rt6000 > 0)
                    {
                        double result = Math.Round((double)((Rt3000 + Rt5000 + Rt6000) / 3.0));
                        patient_audio.tdh_avg_high_rt = result;
                    }
                    else
                    {
                        patient_audio.tdh_avg_high_rt = null;
                    }
                }
            }
            else if (leftHigh.Contains(e.PropertyName))
            {
                trn_patient_regi patient_regis = (trn_patient_regi)bsPatientRegis.Current;
                trn_audiometric_hdr patient_audio = patient_regis.trn_audiometric_hdrs.FirstOrDefault();
                if (patient_audio != null)
                {
                    int Lt3000 = patient_audio.tdh_left_level_3000 == null ? 0 : (int)patient_audio.tdh_left_level_3000;
                    int Lt5000 = patient_audio.tdh_left_level_4000 == null ? 0 : (int)patient_audio.tdh_left_level_4000;
                    int Lt6000 = patient_audio.tdh_left_level_6000 == null ? 0 : (int)patient_audio.tdh_left_level_6000;

                    if (Lt3000 > 0 || Lt5000 > 0 || Lt6000 > 0)
                    {
                        double result = Math.Round((double)((Lt3000 + Lt5000 + Lt6000) / 3.0));
                        patient_audio.tdh_avg_high_lt = result;
                    }
                    else
                    {
                        patient_audio.tdh_avg_high_lt = null;
                    }
                }
            }
        }

        //private void autoCompleteDoctor_currentChangeHandler(object sender, UIcontrol.completeArgs e)
        //{
        //    txtDoctorCode.Text = e.valueData != null ? e.valueData.ToString() : "";
        //    trn_audiometric_hdr patientAudio = bsPatientAudio.OfType<trn_audiometric_hdr>().FirstOrDefault();
        //    if (patientAudio != null)
        //    {
        //        patientAudio.tdh_doctor_code = txtDoctorCode.Text;
        //    }
        //}
        private void btnGraph_Click(object sender, EventArgs e)
        {
            chart1.Series["Rt"].Points.Clear();
            chart1.Series["Lt"].Points.Clear();
            //trn_patient_regi patient_regis = (trn_patient_regi)bsPatientRegis.Current;
            //trn_audiometric_hdr patient_audio = patient_regis.trn_audiometric_hdrs.FirstOrDefault();
            trn_audiometric_hdr patient_audio = bsPatientAudio.OfType<trn_audiometric_hdr>().FirstOrDefault();
            if (patient_audio != null)
            {
                if (patient_audio.tdh_right_level_250 != null) chart1.Series["Rt"].Points.AddXY(1, patient_audio.tdh_right_level_250);
                if (patient_audio.tdh_right_level_500 != null) chart1.Series["Rt"].Points.AddXY(2, patient_audio.tdh_right_level_500);
                if (patient_audio.tdh_right_level_750 != null) chart1.Series["Rt"].Points.AddXY(3, patient_audio.tdh_right_level_750);
                if (patient_audio.tdh_right_level_1000 != null) chart1.Series["Rt"].Points.AddXY(4, patient_audio.tdh_right_level_1000);
                if (patient_audio.tdh_right_level_1500 != null) chart1.Series["Rt"].Points.AddXY(5, patient_audio.tdh_right_level_1500);
                if (patient_audio.tdh_right_level_2000 != null) chart1.Series["Rt"].Points.AddXY(6, patient_audio.tdh_right_level_2000);
                if (patient_audio.tdh_right_level_3000 != null) chart1.Series["Rt"].Points.AddXY(7, patient_audio.tdh_right_level_3000);
                if (patient_audio.tdh_right_level_4000 != null) chart1.Series["Rt"].Points.AddXY(8, patient_audio.tdh_right_level_4000);
                if (patient_audio.tdh_right_level_6000 != null) chart1.Series["Rt"].Points.AddXY(9, patient_audio.tdh_right_level_6000);
                if (patient_audio.tdh_right_level_8000 != null) chart1.Series["Rt"].Points.AddXY(10, patient_audio.tdh_right_level_8000);

                if (patient_audio.tdh_left_level_250 != null) chart1.Series["Lt"].Points.AddXY(1, patient_audio.tdh_left_level_250);
                if (patient_audio.tdh_left_level_500 != null) chart1.Series["Lt"].Points.AddXY(2, patient_audio.tdh_left_level_500);
                if (patient_audio.tdh_left_level_750 != null) chart1.Series["Lt"].Points.AddXY(3, patient_audio.tdh_left_level_750);
                if (patient_audio.tdh_left_level_1000 != null) chart1.Series["Lt"].Points.AddXY(4, patient_audio.tdh_left_level_1000);
                if (patient_audio.tdh_left_level_1500 != null) chart1.Series["Lt"].Points.AddXY(5, patient_audio.tdh_left_level_1500);
                if (patient_audio.tdh_left_level_2000 != null) chart1.Series["Lt"].Points.AddXY(6, patient_audio.tdh_left_level_2000);
                if (patient_audio.tdh_left_level_3000 != null) chart1.Series["Lt"].Points.AddXY(7, patient_audio.tdh_left_level_3000);
                if (patient_audio.tdh_left_level_4000 != null) chart1.Series["Lt"].Points.AddXY(8, patient_audio.tdh_left_level_4000);
                if (patient_audio.tdh_left_level_6000 != null) chart1.Series["Lt"].Points.AddXY(9, patient_audio.tdh_left_level_6000);
                if (patient_audio.tdh_left_level_8000 != null) chart1.Series["Lt"].Points.AddXY(10, patient_audio.tdh_left_level_8000);
            }
        }
        private void btnCleargraph_Click(object sender, EventArgs e)
        {
            chart1.Series["Rt"].Points.Clear();
            chart1.Series["Lt"].Points.Clear();
            trn_patient_regi patient_regis = (trn_patient_regi)bsPatientRegis.Current;
            trn_audiometric_hdr patient_audio = patient_regis.trn_audiometric_hdrs.FirstOrDefault();
            if (patient_audio != null)
            {
                patient_audio.tdh_right_level_250 = null;
                patient_audio.tdh_right_level_500 = null;
                patient_audio.tdh_right_level_750 = null;
                patient_audio.tdh_right_level_1000 = null;
                patient_audio.tdh_right_level_1500 = null;
                patient_audio.tdh_right_level_2000 = null;
                patient_audio.tdh_right_level_3000 = null;
                patient_audio.tdh_right_level_4000 = null;
                patient_audio.tdh_right_level_6000 = null;
                patient_audio.tdh_right_level_8000 = null;

                patient_audio.tdh_left_level_250 = null;
                patient_audio.tdh_left_level_500 = null;
                patient_audio.tdh_left_level_750 = null;
                patient_audio.tdh_left_level_1000 = null;
                patient_audio.tdh_left_level_1500 = null;
                patient_audio.tdh_left_level_2000 = null;
                patient_audio.tdh_left_level_3000 = null;
                patient_audio.tdh_left_level_4000 = null;
                patient_audio.tdh_left_level_6000 = null;
                patient_audio.tdh_left_level_8000 = null;
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
            btnCleargraph_Click(null, null);
        }
        public void EndEdit()
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

                trn_audiometric_hdr patientAudio = bsPatientAudio.OfType<trn_audiometric_hdr>().FirstOrDefault();
                if (string.IsNullOrEmpty(patientAudio.tdh_create_by))
                {
                    patientAudio.tdh_create_by = user_name;
                    patientAudio.tdh_create_date = dateNow;
                }
                patientAudio.tdh_update_by = user_name;
                patientAudio.tdh_update_date = dateNow;
                //krit
                patientAudio.tdh_doc_result_left_eng = favhearingl.Text;
                patientAudio.tdh_doc_result_left_thai = favhearingl.Text;
                patientAudio.tdh_doc_result_right_eng = favhearingr.Text;
                patientAudio.tdh_doc_result_right_thai = favhearingr.Text;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }

        private void favorite_btnFavoriteClick(object sender, EventArgs e)
        {
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null)
            {
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                bool savefav = new FavoriteCls().saveFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, txtBox.Text, user_name);
                if (savefav)
                {
                    txtBox.AutoCompleteListThList.Add(txtBox.Text);
                    MessageBox.Show("Add '" + txtBox.Text + "' to favorite Complete.");
                }
            }
        }
    }
}

