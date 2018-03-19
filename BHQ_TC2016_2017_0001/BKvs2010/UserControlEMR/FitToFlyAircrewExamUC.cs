using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using BKvs2010.Usercontrols;

namespace BKvs2010.UserControlEMR
{
    public partial class FitToFlyAircrewExamUC : UserControl
    {
        public FitToFlyAircrewExamUC()
        {
            InitializeComponent();
            SetDoctorData();
        }
        private void autoCompleteDocName_SelectedValueChanged(object sender, object e)
        {
            try
            {
                trn_avia_med_cert mc = bsAviationCert.OfType<trn_avia_med_cert>().FirstOrDefault();
                if (mc != null)
                {
                    if (e == null)
                    {
                        mc.tamc_doctor_code = null;
                        mc.tamc_doctor_name = null;
                        txtDoctorPosition.Text = null;
                    }
                    else
                    {
                        mc.tamc_doctor_code = ((Doctor)e).username;
                        mc.tamc_doctor_name = ((Doctor)e).fullname;
                        DoctorPosition dp = GetDoctorPosition(((Doctor)e).mut_id);
                        txtDoctorPosition.Text = dp.DP_Description;
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "autoCompleteDocName_SelectedValueChanged", ex, false);
            }
        }
        class Doctor
        {
            public string fullname { get; set; }
            public string username { get; set; }
            public int mut_id { get; set; }
        }  
        class DoctorPosition
        {
            public int DP_ID { get; set; }
            public string DP_Description { get; set; }
            public DateTime DP_CreateDate { get; set; }
            public string DP_CreateBy { get; set; }
        }
        public mst_user_type user { get; set; }

        private void SetDoctorData()
        {
            List<string> DocName = new List<string>();
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    var result = (from mua in dbc.mst_user_aviations
                                  join mut in dbc.mst_user_types on mua.mut_id equals mut.mut_id
                                  where mua.mua_active == true
                                  select new Doctor
                                  {
                                      username = mut.mut_username,
                                      fullname = mua.mua_en_name,
                                      mut_id = mua.mut_id
                                  }).ToList();
                    result.Insert(0, new Doctor { username = "", fullname = "", mut_id = 0 });

                    autoCompleteDocName.DataSource = result;
                    autoCompleteDocName.DisplayMember = "fullname";
                    autoCompleteDocName.ValueMember = "username";
                    autoCompleteDocName.SelectedValueChanged += new UserControlLibrary.TextBoxAutoComplete.OnSelectedValueChanged(autoCompleteDocName_SelectedValueChanged);
                }
            }
            catch
            {  }            
        }
        private DoctorPosition GetDoctorPosition(int id)
        {
            DoctorPosition doc = new DoctorPosition();
            try
            {
                using (InhCheckupDataContext dbc = new InhCheckupDataContext())
                {
                    DoctorPosition lstDoc = dbc.mst_user_aviations.Where(x => x.mua_active == true && x.mut_id == id)
                        .Select(x => new DoctorPosition
                        {
                            DP_ID = x.mua_id,
                            DP_Description = (x.mua_position).Replace(char.ConvertFromUtf32(10),Environment.NewLine),
                            DP_CreateDate = (DateTime)x.mua_create_date,
                            DP_CreateBy = x.mua_create_by
                        }).FirstOrDefault();
                    doc = lstDoc;
                }
                return doc;
            }
            catch
            { return doc; }
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
                        trn_avia_med_cert avia_med_cert = value.trn_avia_med_certs.FirstOrDefault();
                        if (avia_med_cert == null)
                        {
                            avia_med_cert = new trn_avia_med_cert();
                            value.trn_avia_med_certs.Add(avia_med_cert);
                            avia_med_cert.tamc_create_by = user == null ? null : user.mut_username;
                        }
                        avia_med_cert.tamc_update_by = user == null ? null : user.mut_username;
                        avia_med_cert_PropertyChanged(avia_med_cert, new PropertyChangedEventArgs("tamc_cardio_vascular_flag"));
                        avia_med_cert.PropertyChanged -= new PropertyChangedEventHandler(avia_med_cert_PropertyChanged);
                        avia_med_cert.PropertyChanged += new PropertyChangedEventHandler(avia_med_cert_PropertyChanged);

                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;

                        autoCompleteDocName.SelectedValue = avia_med_cert.tamc_doctor_code;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "PatientRegis", ex, false);
                    }
                }
            }
        }
        private void avia_med_cert_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            //pnlFamily.Enabled = false;
            //pnlCholest.Enabled = false;
            //pnlDiabetic.Enabled = false;
            //pnlObe.Enabled = false;
            //pnlSmok.Enabled = false;
            //pnlLifeS.Enabled = false;        

            try
            {
                if (e.PropertyName == "tamc_cardio_vascular_flag")
                {
                    var value = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (value == null)
                    {
                        TypeDescriptor.GetProperties(sender)["tamc_family_hist"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tamc_cholesterol"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tamc_diabetic"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tamc_obesity"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tamc_smoking"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tamc_life_style"].SetValue(sender, null);
                        pnlCardioVascular.Enabled = false;
                    }
                    else
                    {
                        bool flag = (bool)value;
                        if (!flag)
                        {
                            TypeDescriptor.GetProperties(sender)["tamc_family_hist"].SetValue(sender, null);
                            TypeDescriptor.GetProperties(sender)["tamc_cholesterol"].SetValue(sender, null);
                            TypeDescriptor.GetProperties(sender)["tamc_diabetic"].SetValue(sender, null);
                            TypeDescriptor.GetProperties(sender)["tamc_obesity"].SetValue(sender, null);
                            TypeDescriptor.GetProperties(sender)["tamc_smoking"].SetValue(sender, null);
                            TypeDescriptor.GetProperties(sender)["tamc_life_style"].SetValue(sender, null);
                            pnlCardioVascular.Enabled = false;
                        }
                        else
                        {
                            pnlCardioVascular.Enabled = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "avia_med_cert_PropertyChanged(object sender, PropertyChangedEventArgs e)", ex, false);
            }

        }

        public void Clear()
        {
            this.Enabled = false;
            _PatientRegis = null;
            bsPatientRegis.DataSource = new trn_patient_regi();
        }
        public void EndEdit()
        {
            //trn_avia_med_cert avia_med_cert = _PatientRegis.trn_avia_med_certs.FirstOrDefault();
            //if (autoCompleteDocName.SelectedItem != null)
            //{
            //    AutoCompleteDoctor obj = new AutoCompleteDoctor();
            //    doctor dc = (doctor)autoCompleteDocName.SelectedItem;
            //    avia_med_cert.tamc_doctor_code = dc.SSUSR_Initials;
            //    avia_med_cert.tamc_doctor_name = obj.GetDoctorName(dc.CTPCP_Desc).NameEN;
            //}
            //else
            //{
            //    avia_med_cert.tamc_doctor_code = null;
            //    avia_med_cert.tamc_doctor_name = null;
            //} 
        }
    }
}
