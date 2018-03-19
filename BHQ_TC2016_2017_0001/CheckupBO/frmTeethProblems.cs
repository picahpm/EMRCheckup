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
    public partial class frmTeethProblems : Form
    {
        public frmTeethProblems()
        {
            InitializeComponent();
        }

        private string strCode;
        public string GetDocResult
        {
            get { return strCode; }
            set { strCode = value; }
        }
        private void frmTeethProblems_Load(object sender, EventArgs e)
        {
            using (InhCheckupDataContext dbc = new InhCheckupDataContext())
            {
                var objproblems = (from t1 in dbc.mst_doc_results
                                   where t1.mst_doc_result_hdr.mrm_id == 23
                                   && t1.mst_doc_result_hdr.mrh_code == "TA"
                                   select new { ID = t1.mdr_id, Title = t1.mdr_tname + "\r\n(" + t1.mdr_ename + ")", Code = t1.mdr_code }).ToList();
                dataGridView2.DataSource = objproblems;
                if (GetDocResult != "")
                {
                    for (int i = 0; i < dataGridView2.Rows.Count; i++)
                    {
                        if (GetDocResult.Contains(dataGridView2[3, i].Value.ToString()))
                        {
                            dataGridView2[0, i].Value = true;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string strdata = "";
            string strx = "";
            for (int iRow = 0; iRow <= dataGridView2.Rows.Count - 1; iRow++)
            {
                if (dataGridView2[0, iRow].Value!=null && Convert.ToBoolean(dataGridView2[0, iRow].Value) == true)
                {
                    strdata += strx + dataGridView2["colID", iRow].Value.ToString() + "|" + dataGridView2["ColCode", iRow].Value.ToString(); 
                    strx = ",";
                   // MessageBox.Show(dataGridView2["ColCode", iRow].Value.ToString());
                }
            }
            GetDocResult = strdata;
            this.DialogResult = DialogResult.OK;
        }
    }
    
}
