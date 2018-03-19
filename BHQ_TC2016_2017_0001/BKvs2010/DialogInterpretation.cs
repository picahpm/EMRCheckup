using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBCheckup;
using System.IO;
using System.Diagnostics;
using BKvs2010.EmrClass;

namespace BKvs2010
{
    public partial class DialogInterpretation : Form
    {
        public DialogInterpretation()
        {
            InitializeComponent();
        }

        public trn_patient_ass_dtl dtl;
        public string oldStatus;


        private void DialogInterpretation_Load(object sender, EventArgs e)
        {
           
            using (CheckupDataContext cdc = new CheckupDataContext())
            {
                // Select lab group name for display on header
                var labGroup = cdc.trn_patient_ass_dtls.Where(x => x.tped_lab_code.Equals(dtl.tped_lab_code)).FirstOrDefault();
                txtLabGroup.Text = labGroup.tped_lab_name;
                // Select default interpretation
                var ptlGroup = cdc.mst_lab_recoms.Where(x => x.mlr_id.Equals(dtl.mlr_id)).FirstOrDefault();
                txtDefIntEN.Text = string.Empty;
                txtDefIntTH.Text = string.Empty;
                txtDefIntJP.Text = string.Empty;
                if (ptlGroup != null)
                {
                    txtDefIntEN.Text = string.IsNullOrEmpty(ptlGroup.mlr_en_name) ? "" : ptlGroup.mlr_en_name;
                    txtDefIntTH.Text = string.IsNullOrEmpty(ptlGroup.mlr_th_name) ? "" : ptlGroup.mlr_th_name;
                    txtDefIntJP.Text = string.IsNullOrEmpty(ptlGroup.mlr_jp_name) ? "" : ptlGroup.mlr_jp_name;
                }
            }

            txtNewIntEN.Text = dtl.tped_lab_result_eng;
            txtNewIntTH.Text = dtl.tped_lab_result_thai;
            txtNewIntJP.Text = dtl.tped_lab_result_jp;
            txtItemCode.Text = dtl.tped_lab_code;
            txtItemName.Text = dtl.tped_lab_name;

            txtNormalRange.Text = dtl.tped_lab_nrange;
            txtUnit.Text = dtl.tped_lab_unit;
            txtResult.Text = dtl.tped_lab_value;
            txtSummary.Text = dtl.tped_summary.ToString();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dtl.tped_lab_result_status = oldStatus;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dtl.tped_lab_result_eng = txtNewIntEN.Text;
            dtl.tped_lab_result_thai = txtNewIntTH.Text;
            dtl.tped_lab_result_jp = txtNewIntJP.Text;
            dtl.tped_lab_result_status = "EI";
            dtl.tped_update_by = Program.CurrentUser.mut_username;

            this.Close();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtNewIntEN.Text = txtDefIntEN.Text;
            txtNewIntTH.Text = txtDefIntTH.Text;
            txtNewIntJP.Text = txtDefIntJP.Text;

        }
    }
}
