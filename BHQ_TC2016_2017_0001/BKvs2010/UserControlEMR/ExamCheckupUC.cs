using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BKvs2010.Usercontrols;
using DBCheckup;

namespace BKvs2010.UserControlEMR
{
    public partial class ExamCheckupUC : UserControl
    {
        public ExamCheckupUC()
        {
            InitializeComponent();
            GAResult.AutoCompleteType = "GA";
            AbdomenResult.AutoCompleteType = "AB";
            ChestResult.AutoCompleteType = "XR";
            CVSResult.AutoCompleteType = "CV";
            EARSResult.AutoCompleteType = "HS";
            ExtremitiesResult.AutoCompleteType = "EX";
            EYESResult.AutoCompleteType = "EM";
            HEADResult.AutoCompleteType = "HD";
            HEENTResult.AutoCompleteType = "HE";
            NeuroResult.AutoCompleteType = "NE";
            NOSEResult.AutoCompleteType = "NO";
            OtherResult.AutoCompleteType = "OT";
            THROATResult.AutoCompleteType = "TH";

            GAResult.RightClickDropDown += UI_DeleteFavorite;
            AbdomenResult.RightClickDropDown += UI_DeleteFavorite;
            ChestResult.RightClickDropDown += UI_DeleteFavorite;
            CVSResult.RightClickDropDown += UI_DeleteFavorite;
            EARSResult.RightClickDropDown += UI_DeleteFavorite;
            ExtremitiesResult.RightClickDropDown += UI_DeleteFavorite;
            EYESResult.RightClickDropDown += UI_DeleteFavorite;
            HEADResult.RightClickDropDown += UI_DeleteFavorite;
            HEENTResult.RightClickDropDown += UI_DeleteFavorite;
            NeuroResult.RightClickDropDown += UI_DeleteFavorite;
            NOSEResult.RightClickDropDown += UI_DeleteFavorite;
            OtherResult.RightClickDropDown += UI_DeleteFavorite;
            THROATResult.RightClickDropDown += UI_DeleteFavorite;

            GAResult.BtnFavoriteClick += UI_AddFavorite;
            AbdomenResult.BtnFavoriteClick += UI_AddFavorite;
            ChestResult.BtnFavoriteClick += UI_AddFavorite;
            CVSResult.BtnFavoriteClick += UI_AddFavorite;
            EARSResult.BtnFavoriteClick += UI_AddFavorite;
            ExtremitiesResult.BtnFavoriteClick += UI_AddFavorite;
            EYESResult.BtnFavoriteClick += UI_AddFavorite;
            HEADResult.BtnFavoriteClick += UI_AddFavorite;
            HEENTResult.BtnFavoriteClick += UI_AddFavorite;
            NeuroResult.BtnFavoriteClick += UI_AddFavorite;
            NOSEResult.BtnFavoriteClick += UI_AddFavorite;
            OtherResult.BtnFavoriteClick += UI_AddFavorite;
            THROATResult.BtnFavoriteClick += UI_AddFavorite;
        }
        private void UI_AddFavorite(object sender, string e)
        {
            try
            {
                DateTime dateNow = Program.GetServerDateTime();
                ExamResultUI ui = sender as ExamResultUI;
                if (ui != null)
                {
                    using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                    {
                        int mut_id = cdc.mst_user_types.Where(x => x.mut_username == _username)
                            .Select(x => x.mut_id)
                            .FirstOrDefault();

                        mst_autocomplete_physical_exam mst = cdc.mst_autocomplete_physical_exams
                            .Where(x => x.mape_type == ui.AutoCompleteType &&
                                        x.mut_id == mut_id &&
                                        x.mape_description == e)
                            .FirstOrDefault();
                        if (mst == null)
                        {
                            mst = new mst_autocomplete_physical_exam();
                            mst.mape_type = ui.AutoCompleteType;
                            mst.mut_id = mut_id;
                            mst.mape_active = true;
                            mst.mape_description = e;
                            mst.mape_create_date = dateNow;
                            cdc.mst_autocomplete_physical_exams.InsertOnSubmit(mst);
                            cdc.SubmitChanges();
                        }
                        List<string> tmp = ui.AutoCompleteListThList;
                        tmp.Add(e);
                        ui.AutoCompleteListThList = tmp;
                        MessageBox.Show("Add '" + ui.ResultTH + "' to favorite Complete.");
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ExamCheckupUC", "UI_DeleteText", ex, false);
            }
        }
        private void UI_DeleteFavorite(object sender, string e)
        {
            if (MessageBox.Show("Do you want to Delete '" + e + "'?", e, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    ExamResultUI ui = sender as ExamResultUI;
                    if (ui != null)
                    {
                        using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                        {
                            mst_autocomplete_physical_exam mst = cdc.mst_autocomplete_physical_exams
                                .Where(x => x.mape_type == ui.AutoCompleteType &&
                                            x.mst_user_type.mut_username == _username &&
                                            x.mape_description == e)
                                .FirstOrDefault();
                            if (mst != null)
                            {
                                cdc.mst_autocomplete_physical_exams.DeleteOnSubmit(mst);
                                cdc.SubmitChanges();
                            }
                            List<string> tmp = ui.AutoCompleteListThList;
                            tmp.Remove(e);
                            ui.AutoCompleteListThList = tmp;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Program.MessageError("ExamCheckupUC", "UI_DeleteText", ex, false);
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
                    this.GAResult.Language = value;
                    this.AbdomenResult.Language = value;
                    this.ChestResult.Language = value;
                    this.CVSResult.Language = value;
                    this.EARSResult.Language = value;
                    this.ExtremitiesResult.Language = value;
                    this.EYESResult.Language = value;
                    this.HEADResult.Language = value;
                    this.HEENTResult.Language = value;
                    //    this.MusculoskeletonResult.Language = value;
                    this.NeuroResult.Language = value;
                    this.NOSEResult.Language = value;
                    this.OtherResult.Language = value;
                    this.THROATResult.Language = value;
                    _Language = value;
                }
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (value != _username)
                {
                    GAResult.AutoCompleteListThList = GetListAutoComplete(GAResult.AutoCompleteType, value);
                    AbdomenResult.AutoCompleteListThList = GetListAutoComplete(AbdomenResult.AutoCompleteType, value);
                    ChestResult.AutoCompleteListThList = GetListAutoComplete(ChestResult.AutoCompleteType, value);
                    CVSResult.AutoCompleteListThList = GetListAutoComplete(CVSResult.AutoCompleteType, value);
                    EARSResult.AutoCompleteListThList = GetListAutoComplete(EARSResult.AutoCompleteType, value);
                    ExtremitiesResult.AutoCompleteListThList = GetListAutoComplete(ExtremitiesResult.AutoCompleteType, value);
                    EYESResult.AutoCompleteListThList = GetListAutoComplete(EYESResult.AutoCompleteType, value);
                    HEADResult.AutoCompleteListThList = GetListAutoComplete(HEADResult.AutoCompleteType, value);
                    HEENTResult.AutoCompleteListThList = GetListAutoComplete(HEENTResult.AutoCompleteType, value);
                    NeuroResult.AutoCompleteListThList = GetListAutoComplete(NeuroResult.AutoCompleteType, value);
                    NOSEResult.AutoCompleteListThList = GetListAutoComplete(NOSEResult.AutoCompleteType, value);
                    OtherResult.AutoCompleteListThList = GetListAutoComplete(OtherResult.AutoCompleteType, value);
                    THROATResult.AutoCompleteListThList = GetListAutoComplete(THROATResult.AutoCompleteType, value);
                    _username = value;
                }
            }
        }

        private List<string> GetListAutoComplete(string AutoCompleteType, string user)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    return cdc.mst_autocomplete_physical_exams
                        .Where(x => x.mst_user_type.mut_username == user &&
                                    x.mape_active == true &&
                                    x.mape_type == AutoCompleteType)
                        .Select(x => x.mape_description)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError("ExamCheckupUC", "GetListAutoComplete", ex, false);
                return new List<string>();
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
                        trn_doctor_hdr doctor_hdr = value.trn_doctor_hdrs.FirstOrDefault();
                        if (doctor_hdr == null)
                        {
                            doctor_hdr = new trn_doctor_hdr();
                            value.trn_doctor_hdrs.Add(doctor_hdr);
                        }
                        doctor_hdr.PropertyChanged -= new PropertyChangedEventHandler(doctor_hdr_PropertyChanged);
                        doctor_hdr.PropertyChanged += new PropertyChangedEventHandler(doctor_hdr_PropertyChanged);
                        trn_doctor_checkup doctor_checkup = doctor_hdr.trn_doctor_checkups.FirstOrDefault();
                        if (doctor_checkup == null)
                        {
                            doctor_checkup = new trn_doctor_checkup();
                            doctor_hdr.trn_doctor_checkups.Add(doctor_checkup);
                        }
                        doctor_checkup.PropertyChanged -= new PropertyChangedEventHandler(doctor_checkup_PropertyChanged);
                        doctor_checkup.PropertyChanged += new PropertyChangedEventHandler(doctor_checkup_PropertyChanged);
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

        private void doctor_hdr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        }

        class DestinationField
        {
            public string fieldFlag { get; set; }
            public string fieldRemark { get; set; }
            public string fieldRemarkEN { get; set; }
        }

        List<DestinationField> desFieldExam = new List<DestinationField>
        {
            new DestinationField
            {
                fieldFlag = "trcp_ga",
                fieldRemark = "trcp_ga_remark",
                fieldRemarkEN = "trcp_ga_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_chest",
                fieldRemark = "trcp_chest_remark",
                fieldRemarkEN = "trcp_chest_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_cvs",
                fieldRemark = "trcp_cvs_remark",
                fieldRemarkEN = "trcp_cvs_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_abdomen",
                fieldRemark = "trcp_abdomen_remark",
                fieldRemarkEN = "trcp_abdomen_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_extremities",
                fieldRemark = "trcp_ext_remark",
                fieldRemarkEN = "trcp_ext_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_neuro",
                fieldRemark = "trcp_neuro_remark",
                fieldRemarkEN = "trcp_neuro_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_musculos",
                fieldRemark = "trcp_musculos_remark",
                fieldRemarkEN = "trcp_musculos_remark_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_other",
                fieldRemark = "trcp_other_remark",
                fieldRemarkEN = "trcp_other_remark_en"
            }
        };

        List<DestinationField> desFieldHeent = new List<DestinationField>
        {
            new DestinationField
            {
                fieldFlag = "trcp_h_head",
                fieldRemark = "trcp_heent_head",
                fieldRemarkEN = "trcp_heent_head_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_h_eyes",
                fieldRemark = "trcp_heent_eyes",
                fieldRemarkEN = "trcp_heent_eyes_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_h_ears",
                fieldRemark = "trcp_heent_ears",
                fieldRemarkEN = "trcp_heent_ears_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_h_nose",
                fieldRemark = "trcp_heent_nose",
                fieldRemarkEN = "trcp_heent_nose_en"
            },
            new DestinationField
            {
                fieldFlag = "trcp_h_throat",
                fieldRemark = "trcp_heent_throat",
                fieldRemarkEN = "trcp_heent_throat_en"
            }
        };

        private void doctor_checkup_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                //---- ข้อมูลด้านซ้าย
                if (desFieldExam.Any(x => x.fieldFlag == e.PropertyName))
                {
                    var field = desFieldExam.Where(x => x.fieldFlag == e.PropertyName).Select(x => new { th = x.fieldRemark, en = x.fieldRemarkEN }).FirstOrDefault();
                    char? val = (char?)TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (val == null || val == 'A')
                    {
                        TypeDescriptor.GetProperties(sender)[field.th].SetValue(sender, "");
                        TypeDescriptor.GetProperties(sender)[field.en].SetValue(sender, "");
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)[field.th].SetValue(sender, "อยู่ในเกณฑ์ปกติ");
                        TypeDescriptor.GetProperties(sender)[field.en].SetValue(sender, "With in normal limit.");
                    }
                }
                //---- ข้อมูลด้านขวา Heent                
                else if (desFieldHeent.Any(x => x.fieldFlag == e.PropertyName))
                {
                    var field = desFieldHeent.Where(x => x.fieldFlag == e.PropertyName).Select(x => new { th = x.fieldRemark, en = x.fieldRemarkEN }).FirstOrDefault();
                    char? val = (char?)TypeDescriptor.GetProperties(sender)[e.PropertyName].GetValue(sender);
                    if (val == null || val == 'A')
                    {
                        TypeDescriptor.GetProperties(sender)[field.th].SetValue(sender, "");
                        TypeDescriptor.GetProperties(sender)[field.en].SetValue(sender, "");
                    }
                    else
                    {
                        TypeDescriptor.GetProperties(sender)[field.th].SetValue(sender, "อยู่ในเกณฑ์ปกติ");
                        TypeDescriptor.GetProperties(sender)[field.en].SetValue(sender, "With in normal limit.");
                    }

                    if (desFieldHeent.All(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == null))
                    {
                        TypeDescriptor.GetProperties(sender)["trcp_heent"].SetValue(sender, null);
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark"].SetValue(sender, "");
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark_en"].SetValue(sender, "");
                    }
                    else if (desFieldHeent.All(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'N' ||
                             (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == null))
                    {
                        TypeDescriptor.GetProperties(sender)["trcp_heent"].SetValue(sender, 'N');
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark"].SetValue(sender, "อยู่ในเกณฑ์ปกติ");
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark_en"].SetValue(sender, "With in normal limit.");
                    }
                    else if (desFieldHeent.Any(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A'))
                    {
                        TypeDescriptor.GetProperties(sender)["trcp_heent"].SetValue(sender, 'A');
                        List<string> fieldAb = desFieldHeent.Where(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A')
                                               .Select(x => x.fieldRemark)
                                               .ToList();
                        List<string> remarkAb = new List<string>();
                        fieldAb.ForEach(x => remarkAb.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark"].SetValue(sender, string.Join(", ", remarkAb));
                        List<string> fieldAbEN = desFieldHeent.Where(x => (char?)TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A')
                                                 .Select(x => x.fieldRemarkEN)
                                                 .ToList();
                        List<string> remarkAbEN = new List<string>();
                        fieldAbEN.ForEach(x => remarkAbEN.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark_en"].SetValue(sender, string.Join(", ", remarkAbEN));
                    }
                }
                else if (desFieldHeent.Any(x => x.fieldRemark == e.PropertyName || x.fieldRemarkEN == e.PropertyName))
                {
                    string field = desFieldHeent.Where(x => x.fieldRemark == e.PropertyName || x.fieldRemarkEN == e.PropertyName)
                                   .Select(x => x.fieldFlag)
                                   .FirstOrDefault();
                    var val = (char?) TypeDescriptor.GetProperties(sender)[field].GetValue(sender);
                    if (val == 'A')
                    {
                        List<string> fieldAb =
                            desFieldHeent.Where(
                                    x => (char?) TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A')
                                .Select(x => x.fieldRemark)
                                .ToList();
                        List<string> remarkAb = new List<string>();
                        fieldAb.ForEach(
                            x => remarkAb.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark"].SetValue(sender,
                            string.Join(", ", remarkAb));
                        List<string> fieldAbEN =
                            desFieldHeent.Where(
                                    x => (char?) TypeDescriptor.GetProperties(sender)[x.fieldFlag].GetValue(sender) == 'A')
                                .Select(x => x.fieldRemarkEN)
                                .ToList();
                        List<string> remarkAbEN = new List<string>();
                        fieldAbEN.ForEach(
                            x => remarkAbEN.Add(TypeDescriptor.GetProperties(sender)[x].GetValue(sender).ToString()));
                        TypeDescriptor.GetProperties(sender)["trcp_heent_remark_en"].SetValue(sender,
                            string.Join(", ", remarkAbEN));
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "doctor_checkup_PropertyChanged", ex, false);
            }
        }

        /*    private bool _ShowMusculoskeleton = true;
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
        } */

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

            }
            catch (Exception ex)
            {
                Program.MessageError("ExamResultUI", "EndEdit", ex, false);
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            trn_doctor_hdr doctorHdr = _PatientRegis.trn_doctor_hdrs.FirstOrDefault();
            trn_doctor_checkup doctorCheckup = doctorHdr.trn_doctor_checkups.FirstOrDefault();

            doctorCheckup.PropertyChanged -= doctor_checkup_PropertyChanged;
            if (doctorCheckup != null)
            {
                doctorCheckup.trcp_ga = 'N';
                doctorCheckup.trcp_ga_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_ga_remark_en = "With in normal limit.";
                
                doctorCheckup.trcp_extremities = 'N';
                doctorCheckup.trcp_ext_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_ext_remark_en = "With in normal limit.";

                doctorCheckup.trcp_chest = 'N';
                doctorCheckup.trcp_chest_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_chest_remark_en = "With in normal limit.";

                doctorCheckup.trcp_cvs = 'N';
                doctorCheckup.trcp_cvs_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_cvs_remark_en = "With in normal limit.";

                doctorCheckup.trcp_abdomen = 'N';
                doctorCheckup.trcp_abdomen_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_abdomen_remark_en = "With in normal limit.";

                doctorCheckup.trcp_neuro = 'N';
                doctorCheckup.trcp_neuro_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_neuro_remark_en = "With in normal limit.";

                doctorCheckup.trcp_other = 'N';
                doctorCheckup.trcp_other_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_other_remark_en = "With in normal limit.";

                doctorCheckup.trcp_heent = 'N';
                doctorCheckup.trcp_heent_remark = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_heent_remark_en = "With in normal limit.";

                doctorCheckup.trcp_h_head = 'N';
                doctorCheckup.trcp_heent_head = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_heent_head_en = "With in normal limit.";

                doctorCheckup.trcp_h_eyes = 'N';
                doctorCheckup.trcp_heent_eyes = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_heent_eyes_en = "With in normal limit.";

                doctorCheckup.trcp_h_ears = 'N';
                doctorCheckup.trcp_heent_ears = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_heent_ears_en = "With in normal limit.";

                doctorCheckup.trcp_h_nose = 'N';
                doctorCheckup.trcp_heent_nose = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_heent_nose_en = "With in normal limit.";

                doctorCheckup.trcp_h_throat = 'N';
                doctorCheckup.trcp_heent_throat = "อยู่ในเกณฑ์ปกติ";
                doctorCheckup.trcp_heent_throat_en = "With in normal limit.";
            }
            doctorCheckup.PropertyChanged += doctor_checkup_PropertyChanged;
        }
    }
}