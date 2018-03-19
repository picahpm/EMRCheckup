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
    public partial class ExamOccMedUC : UserControl
    {
        public ExamOccMedUC()
        {
            InitializeComponent();
        }

        private Language _Language = Language.TH;
        public Language Language
        {
            get { return _Language; }
            set
            {
                if (value != _Language)
                {
                    this.GAResult.Language = value;
                    this.AbdomenResult.Language = value;
                    this.ChestResult.Language = value;
                    this.CVSResult.Language = value;
                    this.EARSResult.Language = value;
                  //   this.ExtremitiesResult.Language = value;
                   this.EYESResult.Language = value;
                    this.HEADResult.Language = value;
                    this.HEENTResult.Language = value;
                    this.MusculoskeletonResult.Language = value;
                    this.NeuroResult.Language = value;
                    this.NOSEResult.Language = value;
                    this.OtherResult.Language = value;
                    this.THROATResult.Language = value;
                    _Language = value;
                }
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
                }
                else
                {
                    try
                    {
                        trn_doctor_occ_med doctor_occ = value.trn_doctor_occ_meds.FirstOrDefault();
                        if (doctor_occ == null)
                        {
                            doctor_occ = new trn_doctor_occ_med();
                            value.trn_doctor_occ_meds.Add(doctor_occ);
                        }
                        doctor_occ.PropertyChanged -= new PropertyChangedEventHandler(doctor_occ_PropertyChanged);
                        doctor_occ.PropertyChanged += new PropertyChangedEventHandler(doctor_occ_PropertyChanged);
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "patientRegis", ex, false);
                    }
                }
            }
        }
   
        class DestinationField
        {
            public string fieldFlag { get; set; }
            public string fieldRemark { get; set; }
            public string fieldRemarkEN { get; set; }
        }
        List<DestinationField> desFieldExam = new List<DestinationField>
        {
            new DestinationField { fieldFlag = "tdom_ga", fieldRemark = "tdom_ga_remark", fieldRemarkEN = "tdom_ga_remark_en" },
            new DestinationField { fieldFlag = "tdom_chest", fieldRemark = "tdom_chest_remark", fieldRemarkEN = "tdom_chest_remark_en" },
            new DestinationField { fieldFlag = "tdom_cvs", fieldRemark = "tdom_cvs_remark", fieldRemarkEN = "tdom_cvs_remark_en" },
            new DestinationField { fieldFlag = "tdom_abdomen", fieldRemark = "tdom_abdomen_remark", fieldRemarkEN = "tdom_abdomen_remark_en" },
            new DestinationField { fieldFlag = "tdom_extremities", fieldRemark = "tdom_ext_remark", fieldRemarkEN = "tdom_ext_remark_en" },
            new DestinationField { fieldFlag = "tdom_neuro", fieldRemark = "tdom_neuro_remark", fieldRemarkEN = "tdom_neuro_remark_en" },
            new DestinationField { fieldFlag = "tdom_musculos", fieldRemark = "tdom_musculos_remark", fieldRemarkEN = "tdom_musculos_remark_en" },
            new DestinationField { fieldFlag = "tdom_other", fieldRemark = "tdom_other_remark", fieldRemarkEN = "tdom_other_remark_en" }
        };
        List<DestinationField> desFieldHeent = new List<DestinationField>
        {
            new DestinationField { fieldFlag = "tdom_h_head", fieldRemark = "tdom_heent_head", fieldRemarkEN = "tdom_heent_head_en" },
            new DestinationField { fieldFlag = "tdom_h_eyes", fieldRemark = "tdom_heent_eyes", fieldRemarkEN = "tdom_heent_eyes_en" },
            new DestinationField { fieldFlag = "tdom_h_ears", fieldRemark = "tdom_heent_ears", fieldRemarkEN = "tdom_heent_ears_en" },
            new DestinationField { fieldFlag = "tdom_h_nose", fieldRemark = "tdom_heent_nose", fieldRemarkEN = "tdom_heent_nose_en" },
            new DestinationField { fieldFlag = "tdom_h_throat", fieldRemark = "tdom_heent_throat", fieldRemarkEN = "tdom_heent_throat_en" }
        };
        private void doctor_occ_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //---- ข้อมูลด้านซ้าย
                if (desFieldExam.Any(x => x.fieldFlag == e.PropertyName))
                {
                    var val = TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if ((char?)val == 'N')
                    {
                        string field = desFieldExam.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemark).FirstOrDefault();
                        string remark = TypeDescriptor.GetProperties(sender)[field].GetValue(sender).ToString();
                        if (string.IsNullOrEmpty(remark))
                        {
                            TypeDescriptor.GetProperties(sender)[field].SetValue(sender, "อยู่ในเกณฑ์ปกติ");
                        }
                        string fieldEN = desFieldExam.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemarkEN).FirstOrDefault();
                        string remarkEN = TypeDescriptor.GetProperties(sender)[fieldEN].GetValue(sender).ToString();
                        if (string.IsNullOrEmpty(remarkEN))
                        {
                            TypeDescriptor.GetProperties(sender)[fieldEN].SetValue(sender, "Within normal limit.");
                        }
                    }
                    // ---- Away change to Abnormal clear text // Modify by M 8/5/2015   
                    else if ((char?)val == 'A')
                    {
                        string field = desFieldExam.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemark).FirstOrDefault();
                        string remark = TypeDescriptor.GetProperties(sender)[field].GetValue(sender).ToString();
                        TypeDescriptor.GetProperties(sender)[field].SetValue(sender, ""); 
                        string fieldEN = desFieldExam.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemarkEN).FirstOrDefault();
                        string remarkEN = TypeDescriptor.GetProperties(sender)[fieldEN].GetValue(sender).ToString();
                        TypeDescriptor.GetProperties(sender)[fieldEN].SetValue(sender, "");
                       
                    }
                }
                //---- ข้อมูลด้านขวา Heent                
                else if (desFieldHeent.Any(x => x.fieldFlag == e.PropertyName))
                {
                    var val = (char?)TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (val == 'N')
                    {
                        string field = desFieldHeent.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemark).FirstOrDefault();
                        string remark = TypeDescriptor.GetProperties(sender)[field].GetValue(sender).ToString();
                        if (string.IsNullOrEmpty(remark))
                        {
                            TypeDescriptor.GetProperties(sender)[field].SetValue(sender, "อยู่ในเกณฑ์ปกติ");
                        }
                        string fieldEN = desFieldHeent.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemarkEN).FirstOrDefault();
                        string remarkEN = TypeDescriptor.GetProperties(sender)[fieldEN].GetValue(sender).ToString();
                        if (string.IsNullOrEmpty(remarkEN))
                        {
                            TypeDescriptor.GetProperties(sender)[fieldEN].SetValue(sender, "Within normal limit.");
                        }
                    } // ---- Away change to Abnormal clear text // Modify by M 8/5/2015   
                    else if ((char?)val == 'A')
                    {
                        string field = desFieldHeent.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemark).FirstOrDefault();
                        string remark = TypeDescriptor.GetProperties(sender)[field].GetValue(sender).ToString();
                        TypeDescriptor.GetProperties(sender)[field].SetValue(sender, "");
                        string fieldEN = desFieldHeent.Where(x => x.fieldFlag == e.PropertyName).Select(x => x.fieldRemarkEN).FirstOrDefault();
                        string remarkEN = TypeDescriptor.GetProperties(sender)[fieldEN].GetValue(sender).ToString();
                        TypeDescriptor.GetProperties(sender)[fieldEN].SetValue(sender, "");
                      
                    }
                  
                    if (desFieldHeent.All(x => TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == null))
                    {
                        TypeDescriptor.GetProperties(sender)["tdom_heent"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark_en"].SetValue(sender, null);
                    }
                    else if (desFieldHeent.All(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'N'))
                    {
                        TypeDescriptor.GetProperties(sender)["tdom_heent"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark"].SetValue(sender, "อยู่ในเกณฑ์ปกติ");
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark_en"].SetValue(sender, "Within normal limit.");
                    }
                    else if (desFieldHeent.Any(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A'))
                    {
                        TypeDescriptor.GetProperties(sender)["tdom_heent"].SetValue(sender, 'A');
                        List<string> fieldAb = desFieldHeent.Where(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A').Select(x => x.fieldRemark).ToList();
                        List<string> remarkAb = new List<string>();
                        fieldAb.ForEach(x => remarkAb.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark"].SetValue(sender, string.Join(", ", remarkAb));
                        List<string> fieldAbEN = desFieldHeent.Where(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A').Select(x => x.fieldRemarkEN).ToList();
                        List<string> remarkAbEN = new List<string>();
                        fieldAbEN.ForEach(x => remarkAbEN.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark_en"].SetValue(sender, string.Join(", ", remarkAbEN));
                    }
                }
                else if (desFieldHeent.Any(x => x.fieldRemark == e.PropertyName || x.fieldRemarkEN == e.PropertyName))
                {
                    string field = desFieldHeent.Where(x => x.fieldRemark == e.PropertyName || x.fieldRemarkEN == e.PropertyName).Select(x => x.fieldFlag).FirstOrDefault();
                    var val = (char?)TypeDescriptor.GetProperties(sender)[field].GetValue(sender);
                    if (val == 'A')
                    {
                        List<string> fieldAb = desFieldHeent.Where(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A').Select(x => x.fieldRemark).ToList();
                        List<string> remarkAb = new List<string>();
                        fieldAb.ForEach(x => remarkAb.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark"].SetValue(sender, string.Join(", ", remarkAb));
                        List<string> fieldAbEN = desFieldHeent.Where(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A').Select(x => x.fieldRemarkEN).ToList();
                        List<string> remarkAbEN = new List<string>();
                        fieldAbEN.ForEach(x => remarkAbEN.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["tdom_heent_remark_en"].SetValue(sender, string.Join(", ", remarkAbEN));
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "doctor_occ_med_PropertyChanged", ex, false);
            }
        }

        private bool _ShowMusculoskeleton = true;
        public bool ShowMusculoskeleton
        {
            get { return _ShowMusculoskeleton; }
            set
            {
                if (value != _ShowMusculoskeleton)
                {
                    if (value)
                    {
                        panalMusculos.Height = 70;
                    }
                    else
                    {
                        panalMusculos.Height = 0;
                    }
                    _ShowMusculoskeleton = value;
                }
            }
        }

        public void Clear()
        {
            this.Enabled = false;
            bsPatientRegis.DataSource = new trn_patient_regi();
            _PatientRegis = null;
        }
        public void EndEdit()
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                string user_name = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;

                trn_doctor_occ_med DoctorOccMed = bsDoctorOccMed.OfType<trn_doctor_occ_med>().FirstOrDefault();
                if (DoctorOccMed.tdom_create_by == null)
                {
                    DoctorOccMed.tdom_create_by = user_name;
                    DoctorOccMed.tdom_create_date = dateNow;
                }
                DoctorOccMed.tdom_update_by = user_name;
                DoctorOccMed.tdom_update_date = dateNow;
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "EndEdit", ex, false);
            }
        }
    }
}