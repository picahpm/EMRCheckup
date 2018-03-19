using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq.Mapping;
using System.Data.Common;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.Globalization;

namespace BKvs2010
{
    public partial class frmLabProfile : Form
    {
        public int ptpr_id { get; set; }
        public string LabNo { get; set; }

        private InhCheckupDataContext dbc = new InhCheckupDataContext();

        public frmLabProfile()
        {
            InitializeComponent();
        }

        private void frmLabProfile_Load(object sender, EventArgs e)
        {
            try
            {
                this.Text = Program.GetRoomName();
                uiProfileHorizontal1.Loaddata(ptpr_id,Program.CurrentSite.mhs_id);

                if (ptpr_id != 0) //(Program.CurrentRegis != null)
                {
                    BindDataZone2();
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "frmLabProfile_Load", ex, false);
            }
        }

        private void HidenButtonLab()
        {
            var objhdr = (from t1 in dbc.trn_patient_ass_hdrs
                          where t1.trn_patient_ass_grp.tpr_id == ptpr_id
                          select t1).ToList();
            foreach (var dr in objhdr)
            {
                switch(dr.tpeh_order_name)
                {
                    case "Haematology":
                        btnHeamatology.Enabled = true;
                        break;
                    case "Biochemistry":
                        btnBiochemistry.Enabled = true;
                        break;
                    case "Immunology":
                        btnImmunology.Enabled = true;
                        break;
                    case "Urine Examination":
                        btnUrineExamination.Enabled = true;
                        break;
                    case "Stool Examination" :
                        btnStoolExamination.Enabled = true;
                        break;
                    case "Other Lab":
                        btnother.Enabled = true;
                        break;

                };
            }
        }

        private void BindDataLab(string labgrpname, int tpr_id)
        {
            var objhdr = (from t1 in dbc.trn_patient_ass_hdrs
             where t1.trn_patient_ass_grp.tpr_id == ptpr_id && t1.tpeh_order_name == labgrpname
             select t1);
            if(objhdr != null){
                PatientAssHdrbindingSource1.DataSource = objhdr;
                trn_patient_ass_hdr currentdata = (trn_patient_ass_hdr)PatientAssHdrbindingSource1.Current;
                BindLabDataToGrid();
            }
        }

        private void BindLabDataToGrid()
        {
            trn_patient_ass_hdr currenthdr = (trn_patient_ass_hdr)PatientAssHdrbindingSource1.Current;


            lblHlab.Text = "Lab Profile " + currenthdr.tpeh_order_name;
            txtLabDate.Text = currenthdr.tpeh_collection_date.Value.ToString("dd/MM/yyyy");
            var objdtl = (from t1 in currenthdr.trn_patient_ass_dtls
                          join t2 in dbc.mst_labs on t1.tped_lab_code equals t2.mlb_code
                          select new gridAssHdr
                          {
                              No = 0,
                              ItemSet = t2.mlb_lab_setname,
                              ItemCode = t1.tped_lab_code,
                              ItemName = t1.tped_lab_name,
                              Value = t1.tped_lab_value,
                              NRange = t1.tped_lab_nrange,
                              Units = t1.tped_lab_unit,
                              LabResult = t1.tped_lab_result_thai,
                              Summary = t1.tped_summary
                          }).ToList();
            gvLabProfile.DataSource = new SortableBindingList<gridAssHdr>(objdtl);

            //Noina Coding ถ้าเจอ code , name ที่เหมือนกัน ไม่ต้องนำมาแสดงอีก
            string labcode = "";
            foreach (DataGridViewRow dr in gvLabProfile.Rows)
            {
                //if (dr.Cells["colTestItem"].Value == labcode)
                //{
                //dr.Cells["colTestItem"].Value = "";
                //dr.Cells["colItemSet"].Value = "";
                //}

                //labcode = (string)dr.Cells["colTestItem"].Value;
                if (dr.Cells[1].Value.ToString() == labcode)
                {
                    dr.Cells[1].Value = "";
                }
                else
                {
                    labcode = dr.Cells[1].Value.ToString();
                }
            }

            string setor = "";
            countAsummary = 0;
            LabResultSummay = "";
            try
            {
                int indexrow = 1;
                for (int i = 0; i < gvLabProfile.Rows.Count; i++)
                {
                    if (gvLabProfile.Rows[i].Cells["colSummary"].Value != null && gvLabProfile.Rows[i].Cells["colSummary"].Value.ToString() == "A")
                    {
                        gvLabProfile.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        gvLabProfile.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                        countAsummary = countAsummary + 1;
                        if (gvLabProfile.Rows[i].Cells["ColResult"].Value != null)
                        {
                            LabResultSummay += setor + gvLabProfile.Rows[i].Cells["ColResult"].Value.ToString();
                            setor = ",";
                        }
                    }
                    gvLabProfile.Rows[i].Cells[0].Value = indexrow;
                    indexrow = indexrow + 1;
                }


                if (currenthdr.tpeh_summary == 'N')
                {
                    rdN.Checked = true;
                    //string olddata = currenthdr.tpeh_pat_education;
                    //if (olddata != LabResultSummay)
                    //{
                    //    currenthdr.tpeh_pat_education = LabResultSummay;
                    //}
                }
                else
                {
                    rdAB.Checked = true;
                }

                this.gvLabProfile.ClearSelection();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "BindLabDataToGrid", ex, false);
            }
        }

        private void BindDataZone2()
        {

            //noina add
            HidenButtonLab();

            try
            {
                PatientAssHdrbindingSource1.DataSource = (from t1 in dbc.trn_patient_ass_hdrs
                                                          where t1.trn_patient_ass_grp.tpr_id==ptpr_id
                                                          select t1);
                if (LabNo!="")
                {
                    int icount = 0;
                    int total= PatientAssHdrbindingSource1.Count;
                ewewe: trn_patient_ass_hdr currentdata = (trn_patient_ass_hdr)PatientAssHdrbindingSource1.Current;
                    if (currentdata.tpeh_order_code != LabNo)
                    {
                        icount = icount + 1;
                        PatientAssHdrbindingSource1.MoveNext();
                        if (total == icount)
                        {
                        }
                        else
                        {
                            goto ewewe;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "BindDataZone2", ex, false);
            }
        }

        private void btnSaveAsDraft_Click(object sender, EventArgs e)
        {
            try
            {
                trn_patient_ass_hdr currenthdr = (trn_patient_ass_hdr)PatientAssHdrbindingSource1.Current;
                currenthdr.tpeh_summary = Program.GetValueRadioTochar(panelSummary);

                PatientAssHdrbindingSource1.EndEdit();
                dbc.SubmitChanges();
                lbAlertMsg.Text = "Save data completed.";
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "btnSaveAsDraft_Click", ex, false);
            }
        }

       private int countAsummary = 0;
       private string LabResultSummay = ""; 
    
        private void PatientAssHdrbindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            trn_patient_ass_hdr currenthdr = (trn_patient_ass_hdr)PatientAssHdrbindingSource1.Current;


            lblHlab.Text ="Lab Profile " + currenthdr.tpeh_order_name;
            txtLabDate.Text = currenthdr.tpeh_collection_date.Value.ToString("dd/MM/yyyy");
            var objdtl = (from t1 in currenthdr.trn_patient_ass_dtls
                          join t2 in dbc.mst_labs on t1.tped_lab_code equals t2.mlb_code
                         select new gridAssHdr
                         {
                             No = 0,
                             ItemSet = t2.mlb_lab_setname,
                             ItemCode = t1.tped_lab_code,
                             ItemName = t1.tped_lab_name,
                             Value = t1.tped_lab_value,
                             NRange = t1.tped_lab_nrange,
                             Units = t1.tped_lab_unit,
                             LabResult = t1.tped_lab_result_thai,
                             Summary = t1.tped_summary
                         }).ToList();
            gvLabProfile.DataSource = new SortableBindingList<gridAssHdr>(objdtl);

            //Noina Coding ถ้าเจอ code , name ที่เหมือนกัน ไม่ต้องนำมาแสดงอีก
            string labcode = "";
            foreach (DataGridViewRow dr in gvLabProfile.Rows)
            {
                //if (dr.Cells["colTestItem"].Value == labcode)
                //{
                    //dr.Cells["colTestItem"].Value = "";
                    //dr.Cells["colItemSet"].Value = "";
                //}

                //labcode = (string)dr.Cells["colTestItem"].Value;
                if (dr.Cells[1].Value.ToString() == labcode)
                {
                    dr.Cells[1].Value = "";
                }
                else
                {
                    labcode = dr.Cells[1].Value.ToString();
                }
            }

            string setor = "";
            countAsummary = 0;
            LabResultSummay = "";
            try
            {
                int indexrow = 1;
                for (int i = 0; i < gvLabProfile.Rows.Count; i++)
                {
                    if (gvLabProfile.Rows[i].Cells["colSummary"].Value != null && gvLabProfile.Rows[i].Cells["colSummary"].Value.ToString() == "A")
                    {
                        gvLabProfile.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                        gvLabProfile.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                        countAsummary = countAsummary + 1;
                        if (gvLabProfile.Rows[i].Cells["ColResult"].Value != null)
                        {
                            LabResultSummay += setor + gvLabProfile.Rows[i].Cells["ColResult"].Value.ToString();
                            setor = ",";
                        }
                    }
                    gvLabProfile.Rows[i].Cells[0].Value = indexrow;
                    indexrow = indexrow + 1;
                }


                if ( currenthdr.tpeh_summary == 'N')
                {
                    rdN.Checked = true;
                    //string olddata = currenthdr.tpeh_pat_education;
                    //if (olddata != LabResultSummay)
                    //{
                    //    currenthdr.tpeh_pat_education = LabResultSummay;
                    //}
                }
                else
                {
                    rdAB.Checked = true;
                }

                this.gvLabProfile.ClearSelection();
            }
            catch (Exception ex)
            {
                Program.MessageError(this.Name, "PatientAssHdrbindingSource1_CurrentChanged", ex, false);
            }
            
        }
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            int indexrow = 1;
            for (int i = 0; i < gvLabProfile.Rows.Count; i++)
            {
                if (gvLabProfile.Rows[i].Cells["colSummary"].Value != null && gvLabProfile.Rows[i].Cells["colSummary"].Value.ToString() == "A")
                {
                    gvLabProfile.Rows[i].DefaultCellStyle.ForeColor = Color.Red;
                    gvLabProfile.Rows[i].DefaultCellStyle.SelectionForeColor = Color.Red;
                }
                gvLabProfile.Rows[i].Cells[0].Value = indexrow;
                indexrow = indexrow + 1;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            lbAlertMsg.Text = "";
            panelSummary.Focus();
            if (dbc.GetChangeSet().Updates.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data?", "Confirm Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PatientAssHdrbindingSource1.EndEdit();
                    dbc.SubmitChanges();
                }
                else
                {
                    PatientAssHdrbindingSource1.CancelEdit();
                    dbc = null;
                    dbc = new InhCheckupDataContext();
                    PatientAssHdrbindingSource1.DataSource = (from t1 in dbc.trn_patient_ass_hdrs
                                                              where t1.trn_patient_ass_grp.tpr_id == ptpr_id
                                                              select t1);
                }
            }
            PatientAssHdrbindingSource1.MovePrevious();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            lbAlertMsg.Text = "";
            panelSummary.Focus();
            if (dbc.GetChangeSet().Updates.Count > 0)
            {
                if (MessageBox.Show("Do you want save change data?", "Confirm Save Data", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PatientAssHdrbindingSource1.EndEdit();
                    dbc.SubmitChanges();
                }
                else
                {
                    PatientAssHdrbindingSource1.CancelEdit();
                    dbc = null;
                    dbc = new InhCheckupDataContext();
                    PatientAssHdrbindingSource1.DataSource = (from t1 in dbc.trn_patient_ass_hdrs
                                                              where t1.trn_patient_ass_grp.tpr_id == ptpr_id
                                                              select t1);
                }
            }
            PatientAssHdrbindingSource1.MoveNext();
        }

        private void btnHeamatology_Click(object sender, EventArgs e)
        {
            BindDataLab(btnHeamatology.Text, ptpr_id);
        }

        private void btnBiochemistry_Click(object sender, EventArgs e)
        {
            BindDataLab(btnBiochemistry.Text, ptpr_id);
        }

        private void btnImmunology_Click(object sender, EventArgs e)
        {
            BindDataLab(btnImmunology.Text, ptpr_id);
        }

        private void btnUrineExamination_Click(object sender, EventArgs e)
        {
            BindDataLab(btnUrineExamination.Text, ptpr_id);
        }

        private void btnStoolExamination_Click(object sender, EventArgs e)
        {
            BindDataLab(btnStoolExamination.Text, ptpr_id);
        }

        private void btnother_Click(object sender, EventArgs e)
        {
            BindDataLab(btnother.Text, ptpr_id);
        }

    }
    class gridAssHdr
    {
        public int No { get; set; }
        public string ItemSet { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Value { get; set; }
        public string NRange { get; set; }
        public string Units { get; set; }
        public string LabResult { get; set; }
        public char? Summary { get; set; }
    }
}
