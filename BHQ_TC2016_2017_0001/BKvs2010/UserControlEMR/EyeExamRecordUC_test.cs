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
    public partial class EyeExamRecordUC_test : UserControl
    {
        public EyeExamRecordUC_test()
        {
            InitializeComponent();
            
            favRecom.RightClickDropDown += fav_DeleteFavrotie;
            favLid_L.RightClickDropDown += fav_DeleteFavrotie;
            favLid_R.RightClickDropDown += fav_DeleteFavrotie;
            favConjunctiveL.RightClickDropDown += fav_DeleteFavrotie;
            favConjunctiveR.RightClickDropDown += fav_DeleteFavrotie;
            favCornL.RightClickDropDown += fav_DeleteFavrotie;
            favCornR.RightClickDropDown += fav_DeleteFavrotie;
            favIrisL.RightClickDropDown += fav_DeleteFavrotie;
            favIrisR.RightClickDropDown += fav_DeleteFavrotie;
            favLensL.RightClickDropDown += fav_DeleteFavrotie;
            favLensR.RightClickDropDown += fav_DeleteFavrotie;
            favFundLESpecify.RightClickDropDown += fav_DeleteFavrotie;
            favFundRESpecify.RightClickDropDown += fav_DeleteFavrotie;
            favConsultEye.RightClickDropDown += fav_DeleteFavrotie;



            string username1;
            string admin_user = "admin";
            username1 = Program.CurrentUser == null ? null : Program.CurrentUser.mut_username;
            List<mst_conclusion_favorite_dtl> mst = new FavoriteCls().getFavorite(favRecom.FavoriteOrder, favRecom.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favColor = new FavoriteCls().getFavorite(favColor.FavoriteOrder, favColor.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favLid_L = new FavoriteCls().getFavorite(favLid_L.FavoriteOrder, favLid_L.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favLid_R = new FavoriteCls().getFavorite(favLid_R.FavoriteOrder, favLid_R.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favConjunctiveL = new FavoriteCls().getFavorite(favConjunctiveL.FavoriteOrder, favConjunctiveL.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favConjunctiveR = new FavoriteCls().getFavorite(favConjunctiveR.FavoriteOrder, favConjunctiveR.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favConrL = new FavoriteCls().getFavorite(favCornL.FavoriteOrder, favCornL.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favConrR = new FavoriteCls().getFavorite(favCornR.FavoriteOrder, favCornR.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favIrisL = new FavoriteCls().getFavorite(favIrisL.FavoriteOrder, favIrisL.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favIrisR = new FavoriteCls().getFavorite(favIrisR.FavoriteOrder, favIrisR.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favLensL = new FavoriteCls().getFavorite(favLensL.FavoriteOrder, favLensL.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favLensR = new FavoriteCls().getFavorite(favLensR.FavoriteOrder, favLensR.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favFundLESpecify = new FavoriteCls().getFavorite(favFundLESpecify.FavoriteOrder, favFundLESpecify.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favFundRESpecify = new FavoriteCls().getFavorite(favFundRESpecify.FavoriteOrder, favFundRESpecify.FavoriteType);
            List<mst_conclusion_favorite_dtl> mst_favConsultEye = new FavoriteCls().getFavorite(favConsultEye.FavoriteOrder, favConsultEye.FavoriteType);
            favRecom.AutoCompleteListThList = mst.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList(); //where username & admin
            favColor.AutoCompleteListThList = mst_favColor.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favLid_L.AutoCompleteListThList = mst_favLid_L.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favLid_R.AutoCompleteListThList = mst_favLid_R.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favConjunctiveL.AutoCompleteListThList = mst_favConjunctiveL.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favConjunctiveR.AutoCompleteListThList = mst_favConjunctiveR.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favCornL.AutoCompleteListThList = mst_favConrL.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favCornR.AutoCompleteListThList = mst_favConrR.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favIrisL.AutoCompleteListThList = mst_favIrisL.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favIrisR.AutoCompleteListThList = mst_favIrisR.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favLensL.AutoCompleteListThList = mst_favLensL.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favLensR.AutoCompleteListThList = mst_favLensR.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favFundLESpecify.AutoCompleteListThList = mst_favFundLESpecify.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favFundRESpecify.AutoCompleteListThList = mst_favFundRESpecify.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
            favConsultEye.AutoCompleteListThList = mst_favConsultEye.Where(x => x.mcfd_create_by == username1 || x.mcfd_create_by == admin_user).Select(x => x.mcfd_description).ToList();
           

            dgvDiagnosis.AutoGenerateColumns = false;
            dgvDiagnosis.ClearSelection();
            dgvRecom.AutoGenerateColumns = false;
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
            public FavoriteTextBoxForEyeExam FTBEyeExam { get; set; }
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
                result.Insert(0, new { id = "", name = "" });
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
                                            FTBEyeExam = favLid_R
                },
                

                new fieldDestinationCls { fieldCheck = "teh_eyehid_le", 
                                            fieldDestination = "teh_eyehid_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Eye Lid : LE",
                                            fieldDesciption2 = "Eye Lid(H00-H06)",
                                            FTBEyeExam = favLid_L
                                            
                },
               

                new fieldDestinationCls { fieldCheck = "teh_conj_re", 
                                            fieldDestination = "teh_conj_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "",
                                            fieldDesciption2 = "Conjunctiva",
                                            FTBEyeExam = favConjunctiveR
                },


                
                new fieldDestinationCls { fieldCheck = "teh_conj_le", 
                                            fieldDestination = "teh_conj_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Conjunctiva : LE",
                                            fieldDesciption2 = "Conjunctiva",
                                            FTBEyeExam = favConjunctiveL
                },


               new fieldDestinationCls { fieldCheck = "teh_corn_re", 
                                            fieldDestination = "teh_corn_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Cornea : RE",
                                            fieldDesciption2 = "Cornea",
                                            FTBEyeExam = favCornR
                },


                new fieldDestinationCls { fieldCheck = "teh_corn_le", 
                                            fieldDestination = "teh_corn_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Cornea : LE",
                                            fieldDesciption2 = "Cornea",
                                            FTBEyeExam = favCornL
                },

              

                new fieldDestinationCls { fieldCheck = "teh_iris_re", 
                                            fieldDestination = "teh_iris_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Iris : RE",
                                            fieldDesciption2 = "Iris",
                                            FTBEyeExam = favIrisR
                },

                
                 new fieldDestinationCls { fieldCheck = "teh_iris_le", 
                                            fieldDestination = "teh_iris_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Iris : LE",
                                            fieldDesciption2 = "Iris",
                                            FTBEyeExam = favIrisL
                },

                

                new fieldDestinationCls { fieldCheck = "teh_lens_re", 
                                            fieldDestination = "teh_lens_rspecify",
                                            fieldSide = "Right",
                                            fieldDesciption1 = "Lens : RE",
                                            fieldDesciption2 = "Lens",
                                            FTBEyeExam = favLensR
                },


                new fieldDestinationCls { fieldCheck = "teh_lens_le", 
                                            fieldDestination = "teh_lens_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Lens : LE",
                                            fieldDesciption2 = "Lens",
                                            FTBEyeExam = favLensL
                },

               
                new fieldDestinationCls { fieldCheck = "teh_cular_le", 
                                            fieldDestination = "teh_cular_lspecify",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Extraocular movement : LE",
                                            fieldDesciption2 = "Extraocular movement",
                                           FTBEyeExam = favFundRESpecify
                },

                new fieldDestinationCls { fieldCheck = "teh_fund_le",
                                            fieldDestination = "teh_fund_lmac_spec",
                                            fieldSide = "Left",
                                            fieldDesciption1 = "Extraocular movement : LE",
                                            fieldDesciption2 = "Fundus",
                                            FTBEyeExam = favFundLESpecify
                }
            };
        }

        private void setEventCombo()
        {

            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            ////////////////////        DrawItem       ///////////////////////
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////


            //Uncorrected Vision
            cmbVisionOutRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionOutLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);           
            //Glasses 
            cmbVisionRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);  
            cmbVisionLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            //Contact Lens
            cmbVisionLenRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionLenLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            //Pin hole
            cmbVisionOutpinRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbVisionOutpinLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            //Ocular Pressure
            cmbTnRE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbTnLE.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            //Main and sub
            cmbMainTopic.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            cmbSubTopic.DrawItem += new DrawItemEventHandler(tooltip_DrawItem);
            

            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            ////////////////////     DropDownClosed    ///////////////////////
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////


            //Uncorrected Vision
            cmbVisionOutRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionOutLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            //Glasses
            cmbVisionRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            //Contact Lens 
            cmbVisionLenRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionLenLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            //Pin hole
            cmbVisionOutpinRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbVisionOutpinLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            //Ocular Pressure
            cmbTnRE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbTnLE.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            //Main and sub
            cmbMainTopic.DropDownClosed += new EventHandler(tooltip_DropDownClosed);
            cmbSubTopic.DropDownClosed += new EventHandler(tooltip_DropDownClosed);


            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////
            ///////////////////   SelectedIndexChanged  //////////////////////
            //////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////


            //Uncorrected Vision
            cmbVisionOutRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionOutLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            //Glasses
            cmbVisionRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            //Contact Lens 
            cmbVisionLenRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionLenLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            //Pin hole
            cmbVisionOutpinRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbVisionOutpinLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            //Ocular Pressure
            cmbTnRE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbTnLE.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            //Main and sub
            cmbMainTopic.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            cmbSubTopic.SelectedIndexChanged += new EventHandler(combo_SelectedIndexChanged);
            
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
                        //chk_adpFU.Checked = patientEyes.teh_advp_fu == 'Y' ? true : false;

                        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(patientEyes))
                        {
                            eye_hdr_PropertyChanged(patientEyes, new PropertyChangedEventArgs(prop.Name));
                        }

                        if (patientEyes.trn_eye_diagnosis.Count() > 0)
                        {
                          //  List<trn_eye_diagnosi> eye_diag = patientEyes.trn_eye_diagnosis.Where(x => x.ted_topic != "Left" && x.ted_topic != "Right" && x.ted_topic != null && x.ted_topic != "").ToList();
                            List<trn_eye_diagnosi> eye_diag = patientEyes.trn_eye_diagnosis.Where(x => x.ted_topic != null && x.ted_topic != "").ToList();
                            eye_diag.ForEach(x =>
                            {
                                sourceDiag.Add(new clsSourceDiagnosis
                                {
                                    fieldName = x.ted_topic + ":" + x.ted_detail,
                                    name = x.ted_topic,
                                    name_desc = x.ted_main,
                                    side = x.ted_topic,
                                    val = x.ted_detail
                                });
                            });

                            //List<trn_eye_exam_hdr> eye_recom = patientEyes.
                            
                        }

                        sourceRecom.Add(new clsSourceRecom
                        {
                            detail = patientEyes.teh_advp_others

                        });

                        dgvDiagnosis.DataSource = sourceDiag;
                        dgvRecom.DataSource = sourceRecom;

                        patientEyes.PropertyChanged -= new PropertyChangedEventHandler(eye_hdr_PropertyChanged);
                        patientEyes.PropertyChanged += new PropertyChangedEventHandler(eye_hdr_PropertyChanged);
                       // favRecom.Text = patientEyes.teh_advp_others;
                        favColor.Text = patientEyes.teh_color_vis_other;
                        favLid_L.Text = patientEyes.teh_eyehid_lspecify;
                        favLid_R.Text = patientEyes.teh_eyehid_rspecify;
                        favConjunctiveL.Text = patientEyes.teh_conj_lspecify;
                        favConjunctiveR.Text = patientEyes.teh_conj_rspecify;
                        favCornL.Text = patientEyes.teh_corn_lspecify;
                        favCornR.Text = patientEyes.teh_corn_rspecify;
                        favIrisL.Text = patientEyes.teh_iris_lspecify;
                        favIrisR.Text = patientEyes.teh_iris_rspecify;
                        favLensL.Text = patientEyes.teh_lens_lspecify;
                        favLensR.Text = patientEyes.teh_lens_rspecify;
                        favFundLESpecify.Text = patientEyes.teh_fund_lmac_spec;
                        favFundRESpecify.Text = patientEyes.teh_fund_rmac_spec;
                        favConsultEye.Text = patientEyes.teh_advp_consult;
                        favConsultEye.Text = patientEyes.teh_advp_consult;

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
        private class clsSourceRecom
        {           
            public string no { get; set; }
            public string detail { get; set; }
            public string val { get; set; }
        }

        private BindingList<clsSourceDiagnosis> sourceDiag = new BindingList<clsSourceDiagnosis>();
        private BindingList<clsSourceRecom> sourceRecom = new BindingList<clsSourceRecom>();
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
                        chkField.FTBEyeExam.SelectedIndex = -1;
                        chkField.FTBEyeExam.Text = String.Empty;
                        chkField.FTBEyeExam.Enabled = false;
                        
                    }
                    else if ((char?)val == 'A')
                    {
                        chkField.FTBEyeExam.SelectedIndex = 1;
                        chkField.FTBEyeExam.Enabled = true;
                        //chkField.comboBox.SelectedIndex = 1;
                        //chkField.comboBox.Enabled = true;
                    }
                    
                }
                else
                {
                    chkField.FTBEyeExam.SelectedIndex = -1;
                    chkField.FTBEyeExam.Text = String.Empty;
                    chkField.FTBEyeExam.Enabled = false;
                    
                    //chkField.comboBox.SelectedIndex = -1;
                    //chkField.comboBox.Enabled = false;
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
                   // clsSourceRecom result_recom = sourceDiag.OfType<clsSourceRecom>().Where(x => x.detail == desField.fieldDestination).FirstOrDefault();
                    if (valflag != null)
                    {
                        if (valflag.GetType() == typeof(string) || valflag.GetType() == typeof(char))
                        {
                            if (string.IsNullOrEmpty(valflag.ToString()) || valflag.ToString() == "N")
                            {
                                if (result != null)
                                {
                                    sourceDiag.Remove(result);
                                    //sourceRecom.Remove(result_recom);
                                    
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
                        if (result != null )
                        {
                            sourceDiag.Remove(result);
                        }
                        //else if (result_recom != null)
                        //{
                        //    sourceRecom.Remove(result_recom);
                        //}
                    }
                    dgvDiagnosis.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgvRecom.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                    dgvDiagnosis.Refresh();
                    dgvRecom.Refresh();
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
               // if (prop.Name != "teh_color_vision")
                if (prop.Name != "teh_fund_le")
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

        private void dgvRecom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            if (gv.Columns[e.ColumnIndex].Name == "Delete")
            {
                clsSourceRecom result = (clsSourceRecom)gv.Rows[e.RowIndex].DataBoundItem;
                sourceRecom.Remove(result);
                fieldDestinationCls fieldDesti = mapField.Where(x => x.fieldDestination == result.detail).FirstOrDefault();
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
                   var whichside="";
                   if (both.Checked  )
                   {
                       whichside = both.Text.ToString();
                   }
                   else if (Right.Checked)
                   {
                       whichside = Right.Text.ToString();
                   }
                   else
                   {
                       whichside = Left.Text.ToString();
                   }


                    sourceDiag.Add(new clsSourceDiagnosis
                    {
                        fieldName = cmbMainTopic.Text.ToString() + ":" + cmbSubTopic.Text.ToString(),
                        //side = cmbMainTopic.Text.ToString(),
                        //side = radioButton25.Checked.ToString(),
                        side = whichside,
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


        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                
                clsSourceRecom result = sourceRecom.Where(x => x.detail == favRecom.Text.ToString()).FirstOrDefault();
                if (result == null)
                {
                    sourceRecom.Add(new clsSourceRecom
                    {
                       
                       // no = favRecom.Text.ToString(),
                        no =  ((dgvRecom.RowCount) + 1).ToString(),
                        detail = favRecom.Text.ToString(),
                        val = favRecom.Text.ToString()
                    });
                    favRecom.Text = String.Empty; 
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
               // patient_eyes.teh_advp_others = dgvRecom.Columns[1].ToString();               
                //patient_eyes.teh_advp_others = dgvRecom.Columns[1].ToString();
                patient_eyes.teh_color_vis_other = favColor.Text;
                patient_eyes.teh_color_vis_other = favColor.Text;
                patient_eyes.teh_eyehid_lspecify = favLid_L.Text;
                patient_eyes.teh_eyehid_lspecify = favLid_L.Text;
                patient_eyes.teh_eyehid_rspecify = favLid_R.Text;
                patient_eyes.teh_eyehid_rspecify = favLid_R.Text;
                patient_eyes.teh_conj_lspecify = favConjunctiveL.Text;
                patient_eyes.teh_conj_lspecify = favConjunctiveL.Text;
                patient_eyes.teh_conj_rspecify = favConjunctiveR.Text;
                patient_eyes.teh_conj_rspecify = favConjunctiveR.Text;
                patient_eyes.teh_corn_lspecify = favCornL.Text;
                patient_eyes.teh_corn_lspecify = favCornL.Text;
                patient_eyes.teh_corn_rspecify = favCornR.Text;
                patient_eyes.teh_corn_rspecify = favCornR.Text;
                patient_eyes.teh_iris_lspecify = favIrisL.Text;
                patient_eyes.teh_iris_lspecify = favIrisL.Text;
                patient_eyes.teh_iris_rspecify = favIrisR.Text;
                patient_eyes.teh_iris_rspecify = favIrisR.Text;
                patient_eyes.teh_lens_lspecify = favLensL.Text;
                patient_eyes.teh_lens_lspecify = favLensL.Text;
                patient_eyes.teh_lens_rspecify = favLensR.Text;
                patient_eyes.teh_lens_rspecify = favLensR.Text;
                patient_eyes.teh_fund_lmac_spec = favFundLESpecify.Text;
                patient_eyes.teh_fund_lmac_spec = favFundLESpecify.Text;
                patient_eyes.teh_fund_rmac_spec = favFundRESpecify.Text;
                patient_eyes.teh_fund_rmac_spec = favFundRESpecify.Text;
                patient_eyes.teh_advp_consult = favConsultEye.Text;
                patient_eyes.teh_advp_consult = favConsultEye.Text;
                
                patient_eyes.teh_advp_others = null;
                for (int rows = 0; rows < dgvRecom.RowCount; rows++)
                {
                    if (dgvRecom.DataSource != null )
                    {
                        patient_eyes.teh_advp_others += (string)dgvRecom[1, rows].Value + System.Environment.NewLine;
                    }
                    else
                    {
                        patient_eyes.teh_advp_others = null;
                    }

                } 

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

        //private void chk_Fun_Normal_RE_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chk_Fun_Normal_RE.Checked == true)
        //    {
        //        this.txtfund_rcup.Enabled = true;
        //        txtfund_rcup.Focus();
        //    }
        //    else
        //    {
        //        this.txtfund_rcup.Enabled = false;
        //        bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault().teh_fund_rcup = null;
        //    }
        //}
        //private void chk_Fun_Normal_LE_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.chk_Fun_Normal_LE.Checked == true)
        //    {
        //        this.txtfund_lcup.Enabled = true;
        //        txtfund_lcup.Focus();
        //    }
        //    else
        //    {
        //        this.txtfund_lcup.Enabled = false;
        //        bsPatientEyes.OfType<trn_eye_exam_hdr>().FirstOrDefault().teh_fund_lcup = null;
        //    }
        //}

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

        #region btnFavoriteClick

        public void fav_btnFavoriteClick(object sender, EventArgs e)
        {
            FavoriteTextBoxForEyeExam txtBox = sender as FavoriteTextBoxForEyeExam;
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

        
        #endregion

        public void fav_DeleteFavrotie(object sender, string e)
        {
            FavoriteTextBoxForEyeExam txtBox = sender as FavoriteTextBoxForEyeExam;
            if (txtBox != null && MessageBox.Show("Do you want to delete '" + e + "'?", "Delete Favorite.", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                new FavoriteCls().removeFavorite(txtBox.FavoriteOrder, txtBox.FavoriteType, e, username);
                txtBox.AutoCompleteListThList.Remove(e);
            }
        }

       

        
        

       

        
       

        

       
        
    }
}
