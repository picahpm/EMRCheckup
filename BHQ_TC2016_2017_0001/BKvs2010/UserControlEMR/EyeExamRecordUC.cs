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
    public partial class EyeExamRecordUC : UserControl
    {
        public EyeExamRecordUC()
        {
            InitializeComponent();
            List<mst_conclusion_favorite_dtl> mst = new FavoriteCls().getFavorite(favoriteTextBox1.FavoriteOrder, favoriteTextBox1.FavoriteType);
            favoriteTextBox1.AutoCompleteListThList = mst.Select(x => x.mcfd_description).ToList();
           
            dgvDiagnosis.AutoGenerateColumns = false;
            setMainTopicCombo();
            setMapField();
            setEventCombo();
        }
        private class fieldDestinationCls
        {
            public string fieldCheck { get; set; }
            public string fieldDestination { get; set; }
            public string fieldSide { get; set; }
            public string fieldDesciption1 { get; set; }
            public string fieldDesciption2 { get; set; }
            public ComboBox comboBox { get; set; }
        }
        List<fieldDestinationCls> mapField = new List<fieldDestinationCls>();
        private void setMainTopicCombo()
        {
            using (InhCheckupDataContext cdc = new InhCheckupDataContext())
            {
                var Maintopic = (from tbl in cdc.mst_eye_hdrs
                                 where tbl.meh_status == 'A' && new List<int?> { 1, 2, 4 }.Contains(tbl.meh_id) //tbl.meh_id (tbl.meh_id >= 1 && tbl.meh_id <= 2)
                                 select new
                                 {
                                     meh_ename = tbl.meh_ename,
                                     meh_id = tbl.meh_id
                                 }).ToList();
                cmbMainTopic.DataSource = Maintopic.Select((item, index) => new
                {
                    item.meh_ename,
                    item.meh_id
                }).ToList();
                cmbMainTopic.DisplayMember = "meh_ename";
                cmbMainTopic.ValueMember = "meh_id";
                cmbMainTopic_SelectedIndexChanged(null, null);

                var result = cdc.mst_eye_dtls.Where(x => x.med_status == 'A' && x.meh_id == 3).Select(x => new 
                {
                    id = x.med_ename,
                    name = x.med_ename
                }).ToList();
                result.Insert(0, new  { id = "", name = "" });
                //cbadvise_plan.DataSource = result;
                //cbadvise_plan.DisplayMember = "name";
                //cbadvise_plan.ValueMember = "id";
            }
        }
        private void setMapField()
        {
            mapField = new List<fieldDestinationCls>
            {
                new fieldDestinationCls { fieldCheck = "teh_eyehid_re", 
                                            fieldDestination = "teh_eyehid_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Eye Lid : RE",
                                            fieldDesciption2 = "Eye Lid(H00-H06)",
                                            comboBox = cmbHidSpecifyRE
                },

                new fieldDestinationCls { fieldCheck = "teh_eyehid_le", 
                                            fieldDestination = "teh_eyehid_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Eye Lid : LE",
                                            fieldDesciption2 = "Eye Lid(H00-H06)",
                                            comboBox = cmbHidSpecifyLE
                },

                new fieldDestinationCls { fieldCheck = "teh_orbit_re", 
                                            fieldDestination = "teh_orbit_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Orbit : RE",
                                            fieldDesciption2 = "Orbit",
                                            comboBox = cmbOrbitSpecifyRE
                },

                new fieldDestinationCls { fieldCheck = "teh_orbit_le", 
                                            fieldDestination = "teh_orbit_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Orbit : LE",
                                            fieldDesciption2 = "Orbit",
                                            comboBox = cmbOrbitSpecifyLE
                },

                new fieldDestinationCls { fieldCheck = "teh_conj_re", 
                                            fieldDestination = "teh_conj_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "",
                                            fieldDesciption2 = "Conjunctiva",
                                            comboBox = cmbConjRspecify
                },

                new fieldDestinationCls { fieldCheck = "teh_conj_le", 
                                            fieldDestination = "teh_conj_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Conjunctiva : LE",
                                            fieldDesciption2 = "Conjunctiva",
                                            comboBox = cmbConjLspecify
                },

                new fieldDestinationCls { fieldCheck = "teh_corn_re", 
                                            fieldDestination = "teh_corn_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Cornea : RE",
                                            fieldDesciption2 = "Cornea",
                                            comboBox = cmbCornSpecifyR
                },

                new fieldDestinationCls { fieldCheck = "teh_corn_le", 
                                            fieldDestination = "teh_corn_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Cornea : LE",
                                            fieldDesciption2 = "Cornea",
                                            comboBox = cmbCornSpecifyL
                },

                new fieldDestinationCls { fieldCheck = "teh_iris_re", 
                                            fieldDestination = "teh_iris_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Iris : RE",
                                            fieldDesciption2 = "Iris",
                                            comboBox = cmbIrisReSpecify
                },

                new fieldDestinationCls { fieldCheck = "teh_iris_le", 
                                            fieldDestination = "teh_iris_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Iris : LE",
                                            fieldDesciption2 = "Iris",
                                            comboBox = cmbIrisLeSpecify
                },

                new fieldDestinationCls { fieldCheck = "teh_lens_re", 
                                            fieldDestination = "teh_lens_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Lens : RE",
                                            fieldDesciption2 = "Lens",
                                            comboBox = cmbLensRSpecify
                },

                new fieldDestinationCls { fieldCheck = "teh_lens_le", 
                                            fieldDestination = "teh_lens_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Lens : LE",
                                            fieldDesciption2 = "Lens",
                                            comboBox = cmbLensLSpecify
                },

                new fieldDestinationCls { fieldCheck = "teh_cular_re", 
                                            fieldDestination = "teh_cular_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Extraocular movement : RE",
                                            fieldDesciption2 = "Extraocular movement",
                                            comboBox = cmbCularSpecifyRE
                },

                new fieldDestinationCls { fieldCheck = "teh_cular_le", 
                                            fieldDestination = "teh_cular_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Extraocular movement : LE",
                                            fieldDesciption2 = "Extraocular movement",
                                            comboBox = cmbCularSpecifyLE
                },

                new fieldDestinationCls { fieldCheck = "teh_fund_re",
                                            fieldDestination = "teh_fund_rmac_spec",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Extraocular movement : RE",
                                            fieldDesciption2 = "Fundus",
                                            comboBox = cmbFundRESpecify
                },

                new fieldDestinationCls { fieldCheck = "teh_fund_le",
                                            fieldDestination = "teh_fund_lmac_spec",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Extraocular movement : LE",
                                            fieldDesciption2 = "Fundus",
                                            comboBox = cmbFundLESpecify
                }
            };
        }
        private void setEventCombo()
        {
            cmbVisionOutRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionOutpinRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionLenRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionOutLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionOutpinLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionLenLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbTnRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbTnLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbHidSpecifyRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbHidSpecifyLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbOrbitSpecifyRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbOrbitSpecifyLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbConjRspecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbConjLspecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbCornSpecifyR.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbCornSpecifyL.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbIrisReSpecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbIrisLeSpecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbLensRSpecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbLensLSpecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbCularSpecifyRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbCularSpecifyLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbFundRESpecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbFundLESpecify.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbMainTopic.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbSubTopic.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
           // cbadvise_plan.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbConsultEye.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);

            cmbVisionOutRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionOutpinRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionLenRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionOutLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionOutpinLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionLenLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbTnRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbTnLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbHidSpecifyRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbHidSpecifyLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbOrbitSpecifyRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbOrbitSpecifyLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbConjRspecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbConjLspecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbCornSpecifyR.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbCornSpecifyL.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbIrisReSpecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbIrisLeSpecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbLensRSpecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbLensLSpecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbCularSpecifyRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbCularSpecifyLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbFundRESpecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbFundLESpecify.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbMainTopic.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbSubTopic.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
           // cbadvise_plan.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbConsultEye.DropDownClosed += new EventHandler(tooltip_DropDownClosed);

            cmbVisionOutRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionOutpinRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionLenRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionOutLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionOutpinLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionLenLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbTnRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbTnLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbCularSpecifyLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbHidSpecifyRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbHidSpecifyLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbOrbitSpecifyRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbOrbitSpecifyLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbConjRspecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbConjLspecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbCornSpecifyR.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbCornSpecifyL.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbIrisReSpecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbIrisLeSpecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbLensRSpecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbLensLSpecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbCularSpecifyRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbCularSpecifyLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbFundRESpecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbFundLESpecify.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbMainTopic.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbSubTopic.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            //cbadvise_plan.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbConsultEye.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
          //  cbadvise_plan.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbConsultEye.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
        }

        private string username;
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
                        username = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
                        trn_eye_exam_hdr patientEyes = value.trn_eye_exam_hdrs.FirstOrDefault();
                        if (patientEyes == null)
                        {
                            patientEyes = new trn_eye_exam_hdr();
                            patientEyes.teh_create_by = username;
                            value.trn_eye_exam_hdrs.Add(patientEyes);
                        }
                        
                        patientEyes.teh_update_by = username;

                        ChEyeDropper.Checked = patientEyes.teh_eyedropper == true ? true : false;
                        chk_adpFU.Checked = patientEyes.teh_advp_fu == 'Y' ? true : false;

                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(patientEyes))
                        {
                            eye_hdr_PropertyChanged(patientEyes, new PropertyChangedEventArgs(prop.Name));
                        }

                        if (patientEyes.trn_eye_diagnosis.Count() > 0)
                        {
                            List<trn_eye_diagnosi> eye_diag = patientEyes.trn_eye_diagnosis.Where(x => x.ted_topic != "Left" && x.ted_topic != "Right" && x.ted_topic != null && x.ted_topic != "").ToList();
                            eye_diag.ForEach(x =>
                            {
                                sourceDiag.Add(new clsSourceDiagnosis
                                {
                                    fieldName = x.ted_topic + ":" + x.ted_detail,
                                    name = x.ted_topic,
                                    name_desc = x.ted_topic,
                                    side = x.ted_topic,
                                    val = x.ted_detail
                                });
                            });
                        }

                        dgvDiagnosis.DataSource = sourceDiag;

                        patientEyes.PropertyChanged -= new PropertyChangedEventHandler(eye_hdr_PropertyChanged);
                        patientEyes.PropertyChanged += new PropertyChangedEventHandler(eye_hdr_PropertyChanged);
                        favoriteTextBox1.Text = patientEyes.teh_advp_others;
                        bsPatientRegis.DataSource = value;
                        _PatientRegis = value;
                        this.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        Clear();
                        Program.MessageError(this.Name, "setProp tpr_patient_regis", ex, false);
                    }
                }
            }
        }

        private class clsSourceDiagnosis
        {
            public string fieldName { get; set; }
            public string side { get; set; }
            public string name { get; set; }
            public string name_desc { get; set; }
            public string val { get; set; }
        }
        private BindingList<clsSourceDiagnosis> sourceDiag = new BindingList<clsSourceDiagnosis>();
        private void eye_hdr_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            fieldDestinationCls chkField = mapField.Where(x => x.fieldCheck == e.PropertyName).FirstOrDefault();
            if (chkField != null)
            {
                var val = TypeDescriptor.GetProperties(sender)[chkField.fieldCheck].GetValue(sender);
                if ((char?)val != null)
                {
                    if ((char?)val == 'N')
                    {
                        chkField.comboBox.SelectedIndex = -1;
                        chkField.comboBox.Enabled = false;
                    }
                    else if ((char?)val == 'A')
                    {
                        chkField.comboBox.SelectedIndex = 1;
                        chkField.comboBox.Enabled = true;
                    }
                }
                else
                {
                    chkField.comboBox.SelectedIndex = -1;
                    chkField.comboBox.Enabled = false;
                }
            }
            else
            {
                fieldDestinationCls desField = mapField.Where(x => x.fieldDestination == e.PropertyName).FirstOrDefault();
                if (desField != null)
                {
                    var val = TypeDescriptor.GetProperties(sender)[desField.fieldDestination].GetValue(sender);
                    var valflag = TypeDescriptor.GetProperties(sender)[desField.fieldCheck].GetValue(sender);
                    clsSourceDiagnosis result = sourceDiag.OfType<clsSourceDiagnosis>().Where(x => x.fieldName == desField.fieldDestination).FirstOrDefault();
                    if (valflag != null)
                    {
                        if (valflag.GetType() == typeof(string) || valflag.GetType() == typeof(char))
                        {
                            if (string.IsNullOrEmpty(valflag.ToString()) || valflag.ToString() == "N")
                            {
                                if (result != null)
                                {
                                    sourceDiag.Remove(result);
                                }
                            }
                            else
                            {
                                if (result != null)
                                {
                                    result.val = val.ToString();
                                }
                                else
                                {
                                    sourceDiag.Add(new clsSourceDiagnosis
                                    {
                                        fieldName = desField.fieldDestination,
                                        side = desField.fieldSide,
                                        name = desField.fieldDesciption1,
                                        name_desc = desField.fieldDesciption2,
                                        val = val.ToString()
                                    });
                                }
                            }
                        }
                    }
                    else
                    {
                        if (result != null)
                        {
                            sourceDiag.Remove(result);
                        }
                    }
                    dgvDiagnosis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgvDiagnosis.Refresh();
                }
            }
        }
        ToolTip tooltip = new ToolTip();
        private void tooltip_DrawItem(object sender, DrawItemEventArgs e)
        {
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 500;
            tooltip.ReshowDelay = 100;
            tooltip.AutomaticDelay = 500;
            tooltip.ShowAlways = false;
            string DropDownText;
            ComboBox combo = (ComboBox)sender;
            if (e.Index == -1)
            {
                DropDownText = string.Empty;
            }
            else
            {
                DropDownText = combo.GetItemText(combo.Items[e.Index]);
            }
            e.DrawBackground();
            using (SolidBrush br = new SolidBrush(e.ForeColor))
            {
                e.Graphics.DrawString(DropDownText, e.Font, br, e.Bounds);
            }
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                tooltip.Show(DropDownText, combo, e.Bounds.Right, e.Bounds.Bottom);
            }
        }
        private void tooltip_DropDownClosed(object sender, EventArgs e)
        {
            tooltip.Hide((ComboBox)sender);
        }
        private void combo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = (ComboBox)sender;
            foreach (Binding binding in combo.DataBindings)
            {
                binding.WriteValue();
            }
        }

        private void btnDefault_Click(object sender, EventArgs e)
        {
            trn_eye_exam_hdr patient_eyes = bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault();
            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(patient_eyes))
            {
                if (prop.Name != "teh_color_vision")
                {
                    if (mapField.Select(x => x.fieldCheck).Contains(prop.Name) || prop.Name == "teh_advp" || prop.Name == "teh_color_vision")
                    {
                        if (prop.PropertyType == typeof(System.Nullable<char>))
                        {
                            prop.SetValue(patient_eyes, 'N');
                        }
                        else if (prop.PropertyType == typeof(string))
                        {
                            prop.SetValue(patient_eyes, "N");
                        }
                    }
                }
            }
        }

        private void dgvDiagnosis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            if (gv.Columns[e.ColumnIndex].Name == "Del")
            {
                clsSourceDiagnosis result = (clsSourceDiagnosis)gv.Rows[e.RowIndex].DataBoundItem;
                sourceDiag.Remove(result);
                fieldDestinationCls fieldDesti = mapField.Where(x => x.fieldDestination == result.fieldName).FirstOrDefault();
                if (fieldDesti != null)
                {
                    trn_eye_exam_hdr patient_eyes = bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault();
                    TypeDescriptor.GetProperties(patient_eyes)[fieldDesti.fieldCheck].SetValue(patient_eyes, null);
                }
            }
        }
        private void cmbMainTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                using (InhCheckupDataContext cdc = new InhCheckupDataContext())
                {
                    int idx = (int)cmbMainTopic.SelectedValue;
                    var Subtopic = (from tbl in cdc.mst_eye_dtls
                                    where tbl.meh_id == idx
                                    && tbl.med_status == 'A'
                                    select new
                                    {
                                        med_ename = tbl.med_ename,
                                        med_id = tbl.med_id
                                    }).ToList();
                    cmbSubTopic.DataSource = Subtopic.Select((item, index) => new
                    {
                        item.med_ename,
                        item.med_id
                    }).ToList();

                    cmbSubTopic.DisplayMember = "med_ename";
                    cmbSubTopic.ValueMember = "med_id";
                }
            }
            catch
            {

            }
        }
        private void btnreset_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = sourceDiag.Count - 1; i >= 0; i--)
                {
                    fieldDestinationCls fieldDesti = mapField.Where(x => x.fieldDestination == sourceDiag[i].fieldName).FirstOrDefault();
                    if (fieldDesti != null)
                    {
                        trn_eye_exam_hdr patient_eyes = bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault();
                        TypeDescriptor.GetProperties(patient_eyes)[fieldDesti.fieldCheck].SetValue(patient_eyes, null);
                    }
                }
                sourceDiag.Clear();
            }
            catch
            {

            }
        }
        private void btnAddDiagnosis_Click(object sender, EventArgs e)
        {
            try
            {
                clsSourceDiagnosis result = sourceDiag.Where(x => x.fieldName == cmbMainTopic.Text.ToString() + ":" + cmbSubTopic.Text.ToString()).FirstOrDefault();
                if (result == null)
                {
                    sourceDiag.Add(new clsSourceDiagnosis
                    {
                        fieldName = cmbMainTopic.Text.ToString() + ":" + cmbSubTopic.Text.ToString(),
                        side = cmbMainTopic.Text.ToString(),
                        name = cmbMainTopic.Text.ToString(),
                        name_desc = cmbMainTopic.Text.ToString(),
                        val = cmbSubTopic.Text.ToString()
                    });
                }
            }
            catch
            {

            }
        }
        private void chk_adpFU_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chk = (CheckBox)sender;
                if (chk.Checked == true)
                {
                    bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault().teh_advp_fu = 'Y';
                }
                else
                {
                    bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault().teh_advp_fu = 'N';
                }
            }
            catch
            {

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
                trn_eye_exam_hdr patient_eyes = bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault();
                if (patient_eyes.teh_create_by == null)
                {
                    patient_eyes.teh_create_date = dateNow;
                }
                patient_eyes.teh_update_date = dateNow;
                patient_eyes.teh_advp_others = favoriteTextBox1.Text;
                patient_eyes.teh_advp_others = favoriteTextBox1.Text;
                patient_eyes.trn_eye_diagnosis.Clear();
                patient_eyes.trn_eye_diagnosis.AddRange(sourceDiag.Select(x => new trn_eye_diagnosi
                {
                    ted_topic = x.side,
                    ted_main = x.name_desc,
                    ted_detail = x.val,
                    ted_create_by = username,
                    ted_create_date = dateNow,
                    ted_update_by = username,
                    ted_update_date = dateNow
                   
                }));
            }
            catch
            {

            }
        }

        private void chk_Fun_Normal_RE_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_Fun_Normal_RE.Checked == true)
            {
                this.txtfund_rcup.Enabled = true;
                txtfund_rcup.Focus();
            }
            else
            {
                this.txtfund_rcup.Enabled = false;
                bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault().teh_fund_rcup = null;
            }
        }
        private void chk_Fun_Normal_LE_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chk_Fun_Normal_LE.Checked == true)
            {
                this.txtfund_lcup.Enabled = true;
                txtfund_lcup.Focus();
            }
            else
            {
                this.txtfund_lcup.Enabled = false;
                bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault().teh_fund_lcup = null;
            }
        }

        private void btnAdd_AdvisePlan_Click(object sender, EventArgs e)
        {
            //if (cbadvise_plan.Text != null && cbadvise_plan.Text != string.Empty && cbadvise_plan.Text != "")
            //{
            //    trn_eye_exam_hdr eye = _PatientRegis.trn_eye_exam_hdrs.FirstOrDefault();
            //    if (eye.teh_advp_others != null && eye.teh_advp_others != string.Empty && eye.teh_advp_others != "")
            //    {
            //        eye.teh_advp_others += ", " + cbadvise_plan.Text;
            //    }
            //    else { eye.teh_advp_others += cbadvise_plan.Text; }
            //    this.cbadvise_plan.SelectedValue = "";
            //    cbadvise_plan.Focus();
            //}
            //else { cbadvise_plan.Focus(); }
        }

        private void favoriteTextBox1_btnFavoriteClick(object sender, EventArgs e)
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
        private void favoriteTextBox1_DeleteFavorite(object sender, string e)
        {
            FavoriteTextBox txtBox = sender as FavoriteTextBox;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, username);
                txtBox.AutoCompleteListThList.Remove(e);
            }
        }
    }
}
