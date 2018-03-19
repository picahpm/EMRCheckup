using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
namespace CheckupBO
{
    public partial class frmViewEyes : Form
    {
        InhCheckupDataContext dbc = new InhCheckupDataContext();
        DataTable dt = new DataTable();
        DataTable Tmptable = new DataTable("items");
        public int TprID;
        public frmViewEyes()
        {
            InitializeComponent();
        }

        private void frmPE_DialogEyes_Load(object sender, EventArgs e)
        {
            ComboboxMainTopic();
            cmbMainTopic.SelectedIndex = 0;
            timer1.Enabled = false;
            this.LoadTransaction();
            this.LoadChkRadio(sender, e);
        }

        private void ComboboxMainTopic()
        {
            try
            {
                var Maintopic = (from tbl in dbc.mst_eye_hdrs
                                 where tbl.meh_status == 'A'
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
            }
            catch (Exception e)
            {
                MessageBox.Show("catch : " + e.ToString());
            }
        }

        private void LoadTransaction()
        { 
            var objeye_exam = (from teh in dbc.trn_eye_exam_hdrs where teh.tpr_id == TprID select teh).FirstOrDefault();
            if (objeye_exam != null)
            {
                //Tab 1
                bindingSourcetrn_eye_exam_hdr.DataSource = objeye_exam;
                trn_eye_exam_hdr objCurrentEyeExam = (trn_eye_exam_hdr)bindingSourcetrn_eye_exam_hdr.Current;

                trn_eye_aircrew_thai objthai = dbc.trn_eye_aircrew_thais.Where(c => c.teh_id == objCurrentEyeExam.teh_id).FirstOrDefault();
                trn_eye_aircrew_faa objfa = dbc.trn_eye_aircrew_faas.Where(c => c.teh_id == objCurrentEyeExam.teh_id).FirstOrDefault();
                trn_eye_aircrew_can objcan = dbc.trn_eye_aircrew_cans.Where(c => c.teh_id == objCurrentEyeExam.teh_id).FirstOrDefault();
                trn_eye_aircrew_aus objaus = dbc.trn_eye_aircrew_aus.Where(c => c.teh_id == objCurrentEyeExam.teh_id).FirstOrDefault();
                trn_eye_diagnosi objdiagnosis = dbc.trn_eye_diagnosis.Where(c => c.teh_id == objCurrentEyeExam.teh_id).FirstOrDefault();

                Program.SetValueRadioGroup(pnlcolor, objCurrentEyeExam.teh_color_vision);

                Program.SetValueRadioGroup(pnlEyeHidRE, objCurrentEyeExam.teh_eyehid_le);
                Program.SetValueRadioGroup(pnlEyeHidLE, objCurrentEyeExam.teh_eyehid_re);
                cmbHidSpecifyRE.SelectedText = objCurrentEyeExam.teh_eyehid_rspecify;
                cmbHidSpecifyRE.SelectedText = objCurrentEyeExam.teh_eyehid_lspecify;

                Program.SetValueRadioGroup(pnlOrbitRE, objCurrentEyeExam.teh_orbit_re);
                Program.SetValueRadioGroup(pnlOrbitLE, objCurrentEyeExam.teh_orbit_le);

                Program.SetValueRadioGroup(pnlConjRE, objCurrentEyeExam.teh_conj_re);
                Program.SetValueRadioGroup(pnlConjLE, objCurrentEyeExam.teh_conj_le);

                Program.SetValueRadioGroup(pnlCornRE, objCurrentEyeExam.teh_corn_re);
                Program.SetValueRadioGroup(pnlCornRE, objCurrentEyeExam.teh_corn_le);

                Program.SetValueRadioGroup(pnlIrisLE, objCurrentEyeExam.teh_iris_re);
                Program.SetValueRadioGroup(pnlIrisRE, objCurrentEyeExam.teh_iris_le);

                Program.SetValueRadioGroup(pnlAC_depth, objCurrentEyeExam.teh_acdep);

                Program.SetValueRadioGroup(pnlLensRE, objCurrentEyeExam.teh_lens_re);
                Program.SetValueRadioGroup(pnllensLE, objCurrentEyeExam.teh_lens_le);

                Program.SetValueRadioGroup(pnlRetinaRE, objCurrentEyeExam.teh_retina_re);
                Program.SetValueRadioGroup(pnlRetinaLE, objCurrentEyeExam.teh_retina_le);

                Program.SetValueRadioGroup(pnlCularRE, objCurrentEyeExam.teh_cular_re);
                Program.SetValueRadioGroup(pnlCularLE, objCurrentEyeExam.teh_cular_le);

                Program.SetValueRadioGroup(pnlFundusMacRE, objCurrentEyeExam.teh_fund_rmacula);
                Program.SetValueRadioGroup(pnlFundusMacLE, objCurrentEyeExam.teh_fund_lmacula);

                Program.SetValueRadioGroup(pnlNormalcupdiscRE, objCurrentEyeExam.teh_fund_rcup);
                Program.SetValueRadioGroup(pnlNormalcupdiscLE, objCurrentEyeExam.teh_fund_lcup);

                if (objCurrentEyeExam.teh_advp_disease == 'Y')
                    chk_adpDisease.Checked = true;
                if (objCurrentEyeExam.teh_advp_med == 'Y')
                    chk_adpMed.Checked = true;
                if (objCurrentEyeExam.teh_advp_care == 'Y')
                    chk_adpCare.Checked = true;
                if (objCurrentEyeExam.teh_advp_fu == 'Y')
                    chk_adpFU.Checked = true;
                if (objCurrentEyeExam.teh_advp_medical == 'Y')
                    chk_adpMedical.Checked = true;

                var master = (from a in dbc.trn_eye_diagnosis
                              join b in dbc.trn_eye_exam_hdrs on a.teh_id equals b.teh_id
                              where a.teh_id == b.teh_id && b.tpr_id == TprID && a.ted_main == "Refractive Error" || a.ted_main == "Visual Disturbance"
                              select new
                              {
                                  main = a.ted_main,
                                  detail = a.ted_detail
                              }).Count();

                if (master != 0)
                {
                    btnAddDiagnosis_Click(null, null);
                }

                //tab 2
                if (objthai != null)
                {
                    bindingSourcetrn_eye_aircrew_thai.DataSource = objthai;
                    if (TprID==0)
                    {
                        bindingSourcetrn_eye_aircrew_thai.DataSource = dbc.trn_eye_aircrew_thais;
                        bindingSourcetrn_eye_aircrew_thai.AddNew();
                    }
                    else
                    {
                        trn_eye_aircrew_thai trneyethai = (trn_eye_aircrew_thai)bindingSourcetrn_eye_aircrew_thai.Current;
                        Program.SetValueRadioGroup(pnlcolorvision, trneyethai.tea_color_vis.ToString());
                        Program.SetValueRadioGroup(pnlvisionfailed, trneyethai.tea_vis_field.ToString());
                    }
                }
                else if (objthai == null)
                {
                    bindingSourcetrn_eye_aircrew_thai.DataSource = dbc.trn_eye_aircrew_thais;
                    bindingSourcetrn_eye_aircrew_thai.AddNew();
                }

               //tab 3
                if (objfa != null)
                {
                    bindingSourcetrn_eye_aircrew_faa.DataSource = objfa;
                    if (TprID==0)
                    {
                        bindingSourcetrn_eye_aircrew_faa.DataSource = dbc.trn_eye_aircrew_faas;
                        bindingSourcetrn_eye_aircrew_faa.AddNew();
                    }
                    else
                    {
                        trn_eye_aircrew_faa trneyefa = (trn_eye_aircrew_faa)bindingSourcetrn_eye_aircrew_faa.Current;
                        Program.SetValueRadioGroup(pnlcolorvisionFa, trneyefa.taf_color_vis.ToString());
                        Program.SetValueRadioGroup(pnlvisionfailedFa, trneyefa.taf_vis_field.ToString());
                    }
                }
                else if (objfa == null)
                {
                    bindingSourcetrn_eye_aircrew_faa.DataSource = dbc.trn_eye_aircrew_faas;
                    bindingSourcetrn_eye_aircrew_faa.AddNew();
                }

                //tab 4
                if (objcan != null)
                {
                    bindingSourcetrn_eye_aircrew_can.DataSource = objcan;
                    if (TprID==0)
                    {
                        bindingSourcetrn_eye_aircrew_can.DataSource = dbc.trn_eye_aircrew_cans;
                        bindingSourcetrn_eye_aircrew_can.AddNew();
                    }
                    else
                    {
                        trn_eye_aircrew_can trneyecan = (trn_eye_aircrew_can)bindingSourcetrn_eye_aircrew_can.Current;
                        Program.SetValueRadioGroup(pnlcolorcan, trneyecan.tac_color_vis.ToString());
                        Program.SetValueRadioGroup(pnlvisionfailedcan, trneyecan.tac_vis_field.ToString());
                    }
                }
                else if (objcan == null)
                {
                    bindingSourcetrn_eye_aircrew_can.DataSource = dbc.trn_eye_aircrew_cans;
                    bindingSourcetrn_eye_aircrew_can.AddNew();
                }

                //tab 5
                if (objaus != null)
                {
                    bindingSourcetrn_eye_aircrew_aus.DataSource = objaus;
                    if (TprID==0)
                    {
                        bindingSourcetrn_eye_aircrew_aus.DataSource = dbc.trn_eye_aircrew_aus;
                        bindingSourcetrn_eye_aircrew_aus.AddNew();
                    }
                    else
                    {
                        trn_eye_aircrew_aus trneyeaus = (trn_eye_aircrew_aus)bindingSourcetrn_eye_aircrew_aus.Current;
                        Program.SetValueRadioGroup(pnlcolorAus, trneyeaus.taa_color_vis.ToString());
                        Program.SetValueRadioGroup(pnlvisionfailedAus, trneyeaus.taa_vis_field.ToString());
                    }
                }
                else if (objaus == null)
                {
                    bindingSourcetrn_eye_aircrew_aus.DataSource = dbc.trn_eye_aircrew_aus;
                    bindingSourcetrn_eye_aircrew_aus.AddNew();
                }


               trn_patient_regi objcurrentRegis = (from t1 in dbc.trn_patient_regis where t1.tpr_id == TprID select t1).FirstOrDefault();
               if (objcurrentRegis != null)
                {
                    string strHn = objcurrentRegis.trn_patient.tpt_hn_no;
                    string strEN = objcurrentRegis.tpr_en_no;
                    string strUser = Program.CurrentUser.mut_username;
                    Service.WS_TrakcareCls ws = new Service.WS_TrakcareCls();
                    DataTable dtservice = ws.GetMedicineByDoctor(strHn, strEN, strUser);
                    //DataTable dtservice = ws.GetMedicineByDoctor("01-92-006363", "O01-12-876565", "001915668");
                    DataSet ds = new DataSet("dss");
                    Tmptable.Columns.Add("Item");
                    Tmptable.Columns.Add("OrderName");
                    Tmptable.Columns.Add("Instruction");
                    Tmptable.Columns.Add("Dosage");
                    Tmptable.Columns.Add("UOM");
                    Tmptable.Columns.Add("Freg");
                    ds.Tables.Add(Tmptable);
                    if (dtservice.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dtservice.Rows.Count - 1; i++)
                        {
                            DataRow newRow = Tmptable.NewRow();
                            Tmptable.Rows.Add(newRow);
                            newRow["Item"] = i + 1;
                            newRow["OrderName"] = dtservice.Rows[i][2].ToString();
                            newRow["Instruction"] = dtservice.Rows[i][10].ToString();
                            newRow["Dosage"] = dtservice.Rows[i][17].ToString();
                            newRow["UOM"] = dtservice.Rows[i][18].ToString();
                            newRow["Freg"] = dtservice.Rows[i][20].ToString();
                            dgvMedical.DataSource = Tmptable;
                            dgvMedical.Columns[0].Width = 50;
                        }
                    }
                }
            }
            else
            {

                bindingSourcetrn_eye_exam_hdr.DataSource = dbc.trn_eye_exam_hdrs;
                bindingSourcetrn_eye_exam_hdr.AddNew();
                bindingSourcetrn_eye_aircrew_thai.DataSource = dbc.trn_eye_aircrew_thais;
                bindingSourcetrn_eye_aircrew_thai.AddNew();
                bindingSourcetrn_eye_aircrew_faa.DataSource = dbc.trn_eye_aircrew_faas;
                bindingSourcetrn_eye_aircrew_faa.AddNew();
                bindingSourcetrn_eye_aircrew_can.DataSource = dbc.trn_eye_aircrew_cans;
                bindingSourcetrn_eye_aircrew_can.AddNew();
                bindingSourcetrn_eye_aircrew_aus.DataSource = dbc.trn_eye_aircrew_aus;
                bindingSourcetrn_eye_aircrew_aus.AddNew();
            }
            
        }

        private void LoadUIPanel(RadioButton RB,Control CT)
        {
            if (RB.Checked == true && RB.Tag == "N")
            {
                CT.Enabled = false;
            }
            else
            {
                CT.Enabled = true;
            }
        }

        //============
        private void rdNormalRE_Hid_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdNormalRE_Hid, cmbHidSpecifyRE);
        }

        private void rdNormalLE_Hid_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdNormalLE_Hid, cmbHidSpecifyLE);
        }
        //=============
        private void rbOrbitRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rbOrbitRE_Normal, cmbOrbitSpecifyRE);
        }

        private void rbOrbitLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rbOrbitLE_Normal, cmbOrbitSpecifyLE);
        }
        //============
      
        private void rdCorneaRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdCorneaRE_Normal, cmbCornSpecifyR);
        }
        //============
        private void rdIrisLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdIrisLE_Normal, cmbIrisLeSpecify);
        }
        private void rdIrisRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdIrisRE_Normal, cmbIrisReSpecify);
        }
        //===========

        private void rdLensRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdLensRE_Normal, cmbLensRSpecify);
        }

        private void rdLensLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdLensLE_Normal, cmbLensLSpecify);
        }
        //===========
        private void rdRetinaRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdRetinaRE_Normal, cmbRetinaSpecifyRE);
        }

        private void rdRetinaLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdRetinaLE_Normal, cmbRetinaSpecifyLE);
        }
        //============
        private void rdExtraRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdExtraRE_Normal, cmbCularSpecifyRE);
        }

        private void rdExtraLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdExtraLE_Normal, cmbCularSpecifyLE);
        }
        //============
        private void rdFundusRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdFundusRE_Normal, cmbFundRESpecify);
        }

        private void rdFundusLE_Normal_CheckedChanged(object sender, EventArgs e)
        {

            this.LoadUIPanel(rdFundusLE_Normal, cmbFundLESpecify);
        }

        private void btnAddDiagnosis_Click(object sender, EventArgs e)
        {
            //count++;
            if (dgvDiagnosis.Rows.Count > 0)
            {
                for (int i = 0; i < dgvDiagnosis.Rows.Count; i++)
                {
                    try
                    {
                        if (dgvDiagnosis.Rows[i].Cells[1].Value.ToString() == "Refractive Error" || dgvDiagnosis.Rows[i].Cells[1].Value.ToString() == "Visual Disturbance")
                        {
                            dgvDiagnosis.Rows.RemoveAt(i);
                            i--;
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
            dgvDiagnosis.Rows.Add(cmbMainTopic.Text.ToString(), cmbMainTopic.Text.ToString(), cmbMainTopic.Text.ToString(), cmbSubTopic.Text.ToString());
        }

        private void dgvDiagnosis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                RadioButton abnormal = new RadioButton();
                abnormal.Name = dgvDiagnosis.CurrentRow.Cells[1].Value.ToString();
                int gvcount = 0;
                gvcount = dgvDiagnosis.Rows.Count;
                if (gvcount == 1)
                {
                    dgvDiagnosis.Rows.RemoveAt(0);
                }
                else
                {
                    dgvDiagnosis.Rows.RemoveAt(dgvDiagnosis.CurrentRow.Index);
                }
                switch (abnormal.Name)
                {
                    case "Eye Hid(H00-H06)  :" + "RE":
                        radioButton47.Checked = false;
                        rdNormalRE_Hid.Checked = true;
                        cmbHidSpecifyRE.SelectedIndex = -1;
                        cmbHidSpecifyLE.SelectedIndex = -1;
                        break;
                    case "Eye Hid(H00-H06)  :" + "LE":
                        radioButton19.Checked = false;
                        rdNormalLE_Hid.Checked = true;
                        break;
                    case "Orbit :" + "RE":
                        rdABOrbitRE.Checked = false;
                        rbOrbitRE_Normal.Checked = true;
                        break;
                    case "Orbit :" + "LE":
                        rdABOrbitLE.Checked = false;
                        rbOrbitLE_Normal.Checked = true;
                        break;
                    case "Conjunctiva :" + "RE":
                        rbConjABRE.Checked = false;
                        rdConjRE_Normal.Checked = true;
                        break;
                    case "Conjunctiva :" + "LE":
                        rbConjABLE.Checked = false;
                        rdConjLE_Normal.Checked = true;
                        break;
                    case "Cornea :" + "RE":
                        rbCornABRE.Checked = false;
                        rdCorneaRE_Normal.Checked = true;
                        break;
                    case "Cornea :" + "LE":
                        rbCornABLE.Checked = false;
                        rdCorneaLE_Normal.Checked = true;
                        break;
                    case "Iris :" + "RE":
                        rbIrisABRE.Checked = false;
                        rdIrisRE_Normal.Checked = true;
                        break;
                    case "Iris :" + "LE":
                        rbIrisABLE.Checked = false;
                        rdIrisLE_Normal.Checked = true;
                        break;
                    case "lens :" + "RE":
                        rbLenABRE.Checked = false;
                        rdLensRE_Normal.Checked = true;
                        break;
                    case "lens :" + "LE":
                        rbLenABLE.Checked = false;
                        rdLensLE_Normal.Checked = true;
                        break;
                    case "Retina :" + "RE":
                        rdRetinaABRE.Checked = false;
                        rdRetinaRE_Normal.Checked = true;
                        break;
                    case "Retina :" + "LE":
                        rdRetinaABLE.Checked = false;
                        rdRetinaLE_Normal.Checked = true;
                        break;
                    case "Extraocular movement  :" + "RE":
                        rdCularABRE.Checked = false;
                        rdExtraRE_Normal.Checked = true;
                        break;
                    case "Extraocular movement  :" + "LE":
                        rdCularABLE.Checked = false;
                        rdExtraLE_Normal.Checked = true;
                        break;
                    case "Fundus :" + "RE":
                        rdMacABRE.Checked = false;
                        rdFundusRE_Normal.Checked = true;
                        break;
                    case "Fundus :" + "LE":
                        rdMacABLE.Checked = false;
                        rdFundusLE_Normal.Checked = true;
                        break;
                }
            }

        }

        public string cmbIndex(ComboBox cmb, string str)
        {
            if (cmb.SelectedIndex == -1)
                str = null;
            else
                str = cmb.SelectedItem.ToString();
            return str;
        }

        private void cmbMainTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idx = cmbMainTopic.SelectedIndex + 1;
                //int selecteVal = Convert.ToInt32(cmbMainTopic.SelectedValue);
                var Subtopic = (from tbl in dbc.mst_eye_dtls
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
            catch (Exception ee)
            {
                MessageBox.Show("catch : " + ee.ToString());
            }
        }

        private void Save()
        {
            Boolean saveIsCompleted = false;
            DateTime datenowvalue = Program.GetServerDateTime();
            int teh_id = 0;
            if (tabControl1.SelectedTab == t1)
            {
                var EyeExam = (trn_eye_exam_hdr)this.bindingSourcetrn_eye_exam_hdr.Current;
                EyeExam.tpr_id = TprID;
                EyeExam.mdr_id = Program.CurrentUser.mut_id;
                EyeExam.teh_type = EyeExam.teh_type;
                EyeExam.teh_vision_out_re = cmbIndex(cmbVisionOutRE, "");
                EyeExam.teh_vision_out_le = cmbIndex(cmbVisionOutLE, "");
                EyeExam.teh_vision_out_rpin = cmbIndex(cmbVisionOutpinRE, "");
                EyeExam.teh_vision_out_lpin = cmbIndex(cmbVisionOutpinLE, "");
                EyeExam.teh_vision_out_rlens = cmbIndex(cmbVisionLenRE, "");
                EyeExam.teh_vision_out_llens = cmbIndex(cmbVisionLenLE, "");
                EyeExam.teh_vision_re = cmbIndex(cmbVisionRE, "");
                EyeExam.teh_vision_le = cmbIndex(cmbVisionLE, "");
                EyeExam.teh_color_vision = Program.GetValueRadioTochar(pnlcolor);
                EyeExam.teh_tn_re = cmbIndex(cmbTnRE, "");
                EyeExam.teh_tn_le = cmbIndex(cmbTnLE, "");
                EyeExam.teh_eyehid_re = Program.GetValueRadioTochar(pnlEyeHidRE);
                EyeExam.teh_eyehid_rspecify = cmbIndex(cmbHidSpecifyRE, "");
                EyeExam.teh_eyehid_le = Program.GetValueRadioTochar(pnlEyeHidLE);
                EyeExam.teh_eyehid_lspecify = cmbIndex(cmbHidSpecifyLE, "");
                EyeExam.teh_orbit_re = Program.GetValueRadioTochar(pnlOrbitRE);
                EyeExam.teh_orbit_rspecify = cmbIndex(cmbOrbitSpecifyRE, "");
                EyeExam.teh_orbit_le = Program.GetValueRadioTochar(pnlOrbitLE);
                EyeExam.teh_orbit_lspecify = cmbIndex(cmbOrbitSpecifyLE, "");
                EyeExam.teh_conj_re = Program.GetValueRadioTochar(pnlConjRE);
                EyeExam.teh_conj_rspecify = cmbIndex(cmbConjRspecify, "");
                EyeExam.teh_conj_le = Program.GetValueRadioTochar(pnlConjLE);
                EyeExam.teh_conj_lspecify = cmbIndex(cmbConjLspecify, "");
                EyeExam.teh_corn_re = Program.GetValueRadioTochar(pnlCornRE);
                EyeExam.teh_corn_rspecify = cmbIndex(cmbCornSpecifyR, "");
                EyeExam.teh_corn_le = Program.GetValueRadioTochar(pnlCornLE);
                EyeExam.teh_corn_lspecify = cmbIndex(cmbCornSpecifyL, "");
                EyeExam.teh_iris_re = Program.GetValueRadioTochar(pnlIrisRE);
                EyeExam.teh_iris_rspecify = cmbIndex(cmbIrisReSpecify, "");
                EyeExam.teh_iris_le = Program.GetValueRadioTochar(pnlIrisLE);
                EyeExam.teh_iris_lspecify = cmbIndex(cmbIrisLeSpecify, "");
                EyeExam.teh_iris_rapd = cmbIndex(cmbIrisRapd, "");
                EyeExam.teh_acdep = Program.GetValueRadioTochar(pnlAC_depth);
                EyeExam.teh_lens_re = Program.GetValueRadioTochar(pnlLensRE);
                EyeExam.teh_lens_rspecify = cmbIndex(cmbLensRSpecify, "");
                EyeExam.teh_lens_le = Program.GetValueRadioTochar(pnllensLE);
                EyeExam.teh_lens_lspecify = cmbIndex(cmbLensLSpecify, "");
                EyeExam.teh_retina_re = Program.GetValueRadioTochar(pnlRetinaRE);
                EyeExam.teh_retina_rspecify = cmbIndex(cmbRetinaSpecifyRE, "");
                EyeExam.teh_retina_le = Program.GetValueRadioTochar(pnlRetinaLE);
                EyeExam.teh_retina_lspecify = cmbIndex(cmbRetinaSpecifyLE, "");
                EyeExam.teh_cular_re = Program.GetValueRadioTochar(pnlCularRE);
                EyeExam.teh_cular_rspecify = cmbIndex(cmbCularSpecifyRE, "");
                EyeExam.teh_cular_le = Program.GetValueRadioTochar(pnlCularLE);
                EyeExam.teh_cular_lspecify = cmbIndex(cmbCularSpecifyLE, "");
                EyeExam.teh_fund_re = Program.GetValueRadioTochar(pnlNormalcupdiscRE);
                EyeExam.teh_fund_rmacula = Program.GetValueRadioTochar(pnlFundusMacRE);
                EyeExam.teh_fund_rmac_spec = cmbIndex(cmbFundRESpecify, "");
                EyeExam.teh_fund_le = Program.GetValueRadioTochar(pnlNormalcupdiscLE);
                EyeExam.teh_fund_lmacula = Program.GetValueRadioTochar(pnlFundusMacLE);
                EyeExam.teh_fund_lmac_spec = cmbIndex(cmbFundLESpecify, "");
                EyeExam.teh_advp_disease = (chk_adpDisease.Checked) ? 'Y' : 'N';
                EyeExam.teh_advp_med = (chk_adpMed.Checked) ? 'Y' : 'N';
                EyeExam.teh_advp_care = (chk_adpCare.Checked) ? 'Y' : 'N';
                EyeExam.teh_advp_fu = (chk_adpFU.Checked) ? 'Y' : 'N';
                EyeExam.teh_advp_medical = (chk_adpMedical.Checked) ? 'Y' : 'N';
                EyeExam.teh_advp_consult = cmbIndex(cmbConsultEye, "");
                EyeExam.teh_update_by = Program.CurrentUser.mut_username;
                EyeExam.teh_update_date = EyeExam.teh_create_date;
                bindingSourcetrn_eye_exam_hdr.EndEdit();
                dbc.SubmitChanges();
                saveIsCompleted = true;
                teh_id = EyeExam.teh_id;
                if (saveIsCompleted == true)
                {
                    int tpr_id = TprID;
                    var countRecord = (from a in dbc.trn_eye_diagnosis
                                       join b in dbc.trn_eye_exam_hdrs on a.teh_id equals b.teh_id
                                       where a.teh_id == b.teh_id && b.tpr_id == tpr_id
                                       select new
                                       {
                                           main = a.ted_main,
                                           detail = a.ted_detail
                                       }).Count();
                    if (countRecord != 0)
                    {
                        var del2 = (from a in dbc.trn_eye_diagnosis
                                    where a.teh_id == teh_id
                                    select a);
                        foreach (var detail in del2)
                        {
                            dbc.trn_eye_diagnosis.DeleteOnSubmit(detail);
                        }
                    }
                    var tehidMax = teh_id;
                    for (int i = 0; i <= dgvDiagnosis.Rows.Count - 1; i++)
                    {
                        string main = dgvDiagnosis.Rows[i].Cells[2].Value.ToString();
                        string detail = dgvDiagnosis.Rows[i].Cells[3].Value.ToString();
                        trn_eye_diagnosi eyediagnosis = new trn_eye_diagnosi();
                        trn_eye_exam_hdr objhdr = new trn_eye_exam_hdr();
                        eyediagnosis.teh_id = tehidMax;
                        eyediagnosis.ted_main = main;
                        eyediagnosis.ted_detail = detail;
                        eyediagnosis.ted_create_by = Program.CurrentUser.mut_username;
                        eyediagnosis.ted_create_date = datenowvalue;
                        eyediagnosis.ted_update_by = Program.CurrentUser.mut_username;
                        eyediagnosis.ted_update_date = EyeExam.teh_create_date;
                        dbc.trn_eye_diagnosis.InsertOnSubmit(eyediagnosis);
                    }
                }
            }
            if (tabControl1.SelectedTab == t2)////eye thai
            {
                trn_eye_exam_hdr obj = dbc.trn_eye_exam_hdrs.Where(c => c.tpr_id == TprID).FirstOrDefault();
                if (obj != null)
                {
                    var EyeAircrewThai = (trn_eye_aircrew_thai)this.bindingSourcetrn_eye_aircrew_thai.Current;
                    EyeAircrewThai.teh_id = teh_id;
                    EyeAircrewThai.tea_type = obj.teh_type;
                    EyeAircrewThai.tea_color_vis = Program.GetValueRadioTochar(pnlcolorvision);
                    EyeAircrewThai.tea_fw_lantern = Program.GetValueRadioTochar(pnlFWThai);
                    EyeAircrewThai.tea_vis_field = Program.GetValueRadioTochar(pnlvisionfailed);
                    EyeAircrewThai.tea_create_by = Program.CurrentUser.mut_username;
                    EyeAircrewThai.tea_create_date = datenowvalue;
                    EyeAircrewThai.tea_update_by = Program.CurrentUser.mut_username;
                    EyeAircrewThai.tea_update_date = EyeAircrewThai.tea_create_date;
                    bindingSourcetrn_eye_aircrew_thai.EndEdit();
                    saveIsCompleted = true;
                }
            }
            if (tabControl1.SelectedTab == t3)//// eye Faa
            {
                trn_eye_exam_hdr obj2 = dbc.trn_eye_exam_hdrs.Where(c => c.tpr_id == TprID).FirstOrDefault();
                if (obj2 != null)
                {
                    var EyeAircrewFAA = (trn_eye_aircrew_faa)this.bindingSourcetrn_eye_aircrew_faa.Current;
                    EyeAircrewFAA.teh_id = teh_id;
                    EyeAircrewFAA.taf_type = obj2.teh_type;
                    EyeAircrewFAA.taf_color_vis = Program.GetValueRadioTochar(pnlcolorvisionFa);
                    EyeAircrewFAA.taf_fw_lantern = Program.GetValueRadioTochar(pnl_FW_FA);
                    EyeAircrewFAA.taf_vis_field = Program.GetValueRadioTochar(pnlvisionfailedFa);
                    EyeAircrewFAA.taf_create_by = Program.CurrentUser.mut_username;
                    EyeAircrewFAA.taf_create_date = datenowvalue;
                    EyeAircrewFAA.taf_update_by = Program.CurrentUser.mut_username;
                    EyeAircrewFAA.taf_update_date = EyeAircrewFAA.taf_create_date;
                    bindingSourcetrn_eye_aircrew_faa.EndEdit();

                    saveIsCompleted = true;
                }
            }
            if (tabControl1.SelectedTab == t4)////eye can
            {
                trn_eye_exam_hdr obj3 = dbc.trn_eye_exam_hdrs.Where(c => c.tpr_id == TprID).FirstOrDefault();
                if (obj3 != null)
                {
                    var EyeAircrewCan = (trn_eye_aircrew_can)this.bindingSourcetrn_eye_aircrew_can.Current;
                    EyeAircrewCan.teh_id = teh_id;
                    EyeAircrewCan.tac_type = obj3.teh_type;
                    EyeAircrewCan.tac_color_vis = Program.GetValueRadioTochar(pnlcolorcan);
                    EyeAircrewCan.tac_fw_lantern = Program.GetValueRadioTochar(pnl_FW_CN);
                    EyeAircrewCan.tac_vis_field = Program.GetValueRadioTochar(pnlvisionfailedcan);
                    EyeAircrewCan.tac_create_by = Program.CurrentUser.mut_username;
                    EyeAircrewCan.tac_create_date = datenowvalue;
                    EyeAircrewCan.tac_update_by = Program.CurrentUser.mut_username;
                    EyeAircrewCan.tac_update_date = EyeAircrewCan.tac_create_date;
                    bindingSourcetrn_eye_aircrew_can.EndEdit();
                    saveIsCompleted = true;
                   
                }
            }
            if (tabControl1.SelectedTab == t5)////eye aus
            {
                trn_eye_exam_hdr obj4 = dbc.trn_eye_exam_hdrs.Where(c => c.tpr_id == TprID).FirstOrDefault();
                var EyeExamAus = (trn_eye_aircrew_aus)this.bindingSourcetrn_eye_aircrew_aus.Current;//  this.bindingSourcetrn_eye_exam_hdr.Current;  
                if (obj4 != null)
                {
                    var EyeAircrewAus = (trn_eye_aircrew_aus)this.bindingSourcetrn_eye_aircrew_aus.Current;
                    EyeAircrewAus.teh_id = teh_id;
                    EyeAircrewAus.taa_type = obj4.teh_type;
                    EyeAircrewAus.taa_color_vis = Program.GetValueRadioTochar(pnlcolorAus);
                    EyeAircrewAus.taa_fw_lantern = Program.GetValueRadioTochar(pnl_FW_AUS);
                    EyeAircrewAus.taa_vis_field = Program.GetValueRadioTochar(pnlvisionfailedAus);
                    EyeAircrewAus.taa_create_by = Program.CurrentUser.mut_username;
                    EyeAircrewAus.taa_create_date = datenowvalue;
                    EyeAircrewAus.taa_update_by = Program.CurrentUser.mut_username;
                    EyeAircrewAus.taa_update_date = EyeAircrewAus.taa_create_date;
                    bindingSourcetrn_eye_aircrew_aus.EndEdit();
                   saveIsCompleted = true;
                }
                
            }

            if (saveIsCompleted == true)
            {
                dbc.SubmitChanges();
                lblMsg.Visible = true;
                timer1.Enabled = true;
                lblMsg.Text = "Save Completed.";
            }
            else
            {
                lblMsg.Text = "Not Save Completed.";
            }
        }

        private void LoadChkRadio(object sender,EventArgs e)
        { 
            rdFundusLE_Normal_CheckedChanged(sender, e);
            rdFundusRE_Normal_CheckedChanged(sender,e);

            rdExtraLE_Normal_CheckedChanged(sender,e);
            rdExtraRE_Normal_CheckedChanged(sender,e);

            rdRetinaLE_Normal_CheckedChanged(sender,e);
            rdRetinaRE_Normal_CheckedChanged(sender,e);

            rdLensLE_Normal_CheckedChanged(sender,e);
            rdLensRE_Normal_CheckedChanged(sender,e);

            rdIrisRE_Normal_CheckedChanged(sender,e);
            rdIrisLE_Normal_CheckedChanged(sender,e);

            rdConjLE_Normal_CheckedChanged(sender,e);
            rdConjRE_Normal_CheckedChanged(sender, e);

            rdCorneaLE_Normal_CheckedChanged(sender, e);
            rdCorneaRE_Normal_CheckedChanged(sender, e);

            rbOrbitLE_Normal_CheckedChanged(sender,e);
            rbOrbitRE_Normal_CheckedChanged(sender,e);

            rdNormalLE_Hid_CheckedChanged(sender,e);
            rdNormalRE_Hid_CheckedChanged(sender,e);
        }

        public void clrpanel(Panel pnl)
        {
            foreach (Control ctl in pnl.Controls)
            {
                RadioButton rb;
                if (ctl is RadioButton)
                {
                    rb = (RadioButton)ctl;
                    rb.Checked = false;
                }
            }
        }

        private void btnreset_Click(object sender, EventArgs e)
        {
            //    dt.Clear();

            clrpanel(pnlRetinaLE);
            clrpanel(pnlRetinaRE);
            clrpanel(pnlEyeHidLE);
            clrpanel(pnlEyeHidRE);
            clrpanel(pnlOrbitLE);
            clrpanel(pnlOrbitRE);
            clrpanel(pnlCularLE);
            clrpanel(pnlCularRE);
            clrpanel(pnlConjRE);
            clrpanel(pnlConjLE);
            cmbHidSpecifyRE.SelectedIndex = -1;

            //    dt.Clear();
            dgvDiagnosis.Rows.Clear();
            dgvDiagnosis.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i <= 10; i++)
            {
                if (i == 10)
                {
                    lblMsg.Visible = false;
                    timer1.Enabled = false;
                }
            }
        }

        private void rdConjRE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdConjRE_Normal, cmbConjRspecify);
        }

        private void rdConjLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdConjLE_Normal, cmbConjLspecify);
        }

        private void rdCorneaLE_Normal_CheckedChanged(object sender, EventArgs e)
        {
            this.LoadUIPanel(rdCorneaLE_Normal, cmbCornSpecifyL);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
    }
}
